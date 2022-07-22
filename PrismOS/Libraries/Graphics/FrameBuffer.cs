using PrismOS.Libraries.Numerics;
using Cosmos.Core;
using System.IO;
using System;

namespace PrismOS.Libraries.Graphics
{
    public unsafe class FrameBuffer : IDisposable
    {
        public FrameBuffer(uint Width, uint Height)
        {
            this.Width = Width;
            this.Height = Height;
            Internal = (uint*)GCImplementation.AllocNewObject(Size * 4);
        }

        public uint this[long Index]
        {
            get
            {
                return Internal[Index];
            }
            set
            {
                Internal[Index] = value;
                //MemoryOperations.Fill(Internal + Index, value, 1);
            }
        }

        public uint* Internal { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public uint Size
        {
            get
            {
                return Width * Height;
            }
        }

        #region Pixel

        public void SetPixel(int X, int Y, Color Color)
        {
            if (Color.A == 0 || X > Width || Y > Height || X < 0 || Y < 0)
            {
                return;
            }
            if (Color.A < 255)
            {
                Color.AlphaBlend(GetPixel(X, Y), Color);
            }
            this[Y * Width + X] = Color.ARGB;
        }
        public void SetPixel(int Index, Color Color)
        {
            if (Color.A == 0 || Index > Size)
            {
                return;
            }
            if (Color.A < 255)
            {
                Color.AlphaBlend(GetPixel(Index), Color);
            }
            this[Index] = Color.ARGB;
        }
        public Color GetPixel(int X, int Y)
        {
            if (X > Width || Y > Height || X < 0 || Y < 0)
            {
                return Color.Black;
            }
            return Color.FromARGB(this[Y * Width + X]);
        }
        public Color GetPixel(int Index)
        {
            if (Index > Size)
            {
                return Color.Black;
            }

            return Color.FromARGB(this[Index]);
        }

        #endregion

        #region Line

        public void DrawLine(int X1, int Y1, int X2, int Y2, Color Color)
        {
            int DX = Math.Abs(X2 - X1), SX = X1 < X2 ? 1 : -1;
            int DY = Math.Abs(Y2 - Y1), SY = Y1 < Y2 ? 1 : -1;
            int err = (DX > DY ? DX : -DY) / 2;

            while (X1 != X2 || Y1 != Y2)
            {
                SetPixel(X1, Y1, Color);
                int e2 = err;
                if (e2 > -DX) { err -= DY; X1 += SX; }
                if (e2 < DY) { err += DX; Y1 += SY; }
            }
        }
        public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
        {
            int IX = (int)(Radius * Math.Cos(Math.PI * Angle / 180));
            int IY = (int)(Radius * Math.Sin(Math.PI * Angle / 180));
            DrawLine(X, Y, X + IX, Y + IY, Color);
        }
        public void DrawQuadraticBezierLine(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, int N = 6)
        {
            // X2 and Y2 is where the curve should bend to.
            if (N > 0)
            {
                int X12 = (X1 + X2) / 2;
                int Y12 = (Y1 + Y2) / 2;
                int X23 = (X2 + X3) / 2;
                int Y23 = (Y2 + Y3) / 2;
                int X123 = (X12 + X23) / 2;
                int Y123 = (Y12 + Y23) / 2;

                DrawQuadraticBezierLine(X1, Y1, X12, Y12, X123, Y123, Color, N - 1);
                DrawQuadraticBezierLine(X123, Y123, X23, Y23, X3, Y3, Color, N - 1);
            }
            else
            {
                DrawLine(X1, Y1, X2, Y2, Color);
                DrawLine(X2, Y2, X3, Y3, Color);
            }
        }
        public void DrawCubicBezierLine(int X0, int Y0, int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            for (double U = 0.0; U <= 1.0; U += 0.0001)
            {
                double Power3V1 = (1 - U) * (1 - U) * (1 - U);
                double Power2V1 = (1 - U) * (1 - U);
                double Power3V2 = U * U * U;
                double Power2V2 = U * U;

                double XU = Power3V1 * X0 + 3 * U * Power2V1 * X1 + 3 * Power2V2 * (1 - U) * X2 + Power3V2 * X3;
                double YU = Power3V1 * Y0 + 3 * U * Power2V1 * Y1 + 3 * Power2V2 * (1 - U) * Y2 + Power3V2 * Y3;
                SetPixel((int)XU, (int)YU, Color);
            }
        }

        #endregion

        #region Rectangle

        public void DrawRectangle(int X, int Y, int Width, int Height, int Radius, Color Color)
        {
            if (Radius > 0)
            {
                DrawCircle(Radius + X, Radius + Y, Radius, Color, 180, 270); // Top left
                DrawCircle(X + Width - Radius, Y + Height - Radius, Radius, Color, 0, 90); // Bottom right
                DrawCircle(Radius + X, Y + Height - Radius, Radius, Color, 90, 180); // Bottom left
                DrawCircle(X + Width - Radius, Radius + Y, Radius, Color, 270, 360);
            }
            DrawLine(X + Radius, Y, X + Width - Radius, Y, Color); // Top Line
            DrawLine(X + Radius, Y + Height, X + Width - Radius, Height + Y, Color); // Bottom Line
            DrawLine(X, Y + Radius, X, Y + Height - Radius, Color); // Left Line
            DrawLine(X + Width, Y + Radius, Width + X, Y + Height - Radius, Color); // Right Line
        }
        public void DrawFilledRectangle(int X, int Y, int Width, int Height, int Radius, Color Color)
        {
            if (Radius == 0 && Color.A == 255)
            {
                if (X < 0)
                {
                    Width -= Math.Abs(X);
                    X = 0;
                }
                if (Y < 0)
                {
                    Height -= Math.Abs(Y);
                    Y = 0;
                }
                if (X + Width >= this.Width)
                {
                    Width -= X;
                }
                if (Y + Height >= this.Height)
                {
                    Height -= Y;
                }
                for (int IY = 0; IY < Height; IY++)
                {
                    MemoryOperations.Fill(Internal + ((Y + IY) * this.Width + X), Color.ARGB, Width);
                }
                return;
            }

            if (Radius == 0)
            {
                for (int IX = X; IX < X + Width; IX++)
                {
                    for (int IY = Y; IY < Y + Height; IY++)
                    {
                        SetPixel(IX, IY, Color);
                    }
                }
            }
            else
            {
                DrawFilledRectangle(X + Radius, Y, Width - Radius * 2, Height, 0, Color);
                DrawFilledRectangle(X, Y + Radius, Width, Height - Radius * 2, 0, Color);

                DrawFilledCircle(X + Radius, Y + Radius, Radius, Color);
                DrawFilledCircle(X + Width - Radius - 1, Y + Radius, Radius, Color);

                DrawFilledCircle(X + Radius, Y + Height - Radius - 1, Radius, Color);
                DrawFilledCircle(X + Width - Radius - 1, Y + Height - Radius - 1, Radius, Color);
            }
        }

        public void DrawRectangleGrid(int X, int Y, int BlockCountX, int BlockCountY, int BlockSize, Color BlockType1, Color BlockType2)
        {
            for (int IX = 0; IX < BlockCountX; IX++)
            {
                for (int IY = 0; IY < BlockCountY; IY++)
                {
                    if ((IX + IY) % 2 == 0)
                    {
                        DrawFilledRectangle(X + IX * BlockSize, Y + IY * BlockSize, BlockSize, BlockSize, 0, BlockType1);
                    }
                    else
                    {
                        DrawFilledRectangle(X + IX * BlockSize, Y + IY * BlockSize, BlockSize, BlockSize, 0, BlockType2);
                    }
                }
            }
        }

        #endregion

        #region Circle

        public void DrawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
            {
                return;
            }

            for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
            {
                double Angle1 = Math.PI * Angle / 180;
                int IX = (int)(Radius * Math.Cos(Angle1));
                int IY = (int)(Radius * Math.Sin(Angle1));
                SetPixel(X + IX, Y + IY, Color);
            }
        }
        public void DrawFilledCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
            {
                return;
            }

            for (int I = 0; I < Radius; I++)
            {
                DrawCircle(X, Y, I, Color, StartAngle, EndAngle);
            }
        }

        #endregion

        #region Triangle

        public void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            DrawLine(X1, Y1, X2, Y2, Color);
            DrawLine(X1, Y1, X3, Y3, Color);
            DrawLine(X2, Y2, X3, Y3, Color);
        }
        public void DrawTriangle(Triangle<float> Triangle, Color Color)
        {
            DrawTriangle((int)Triangle.P1.X, (int)Triangle.P1.Y, (int)Triangle.P2.X, (int)Triangle.P2.Y, (int)Triangle.P3.X, (int)Triangle.P3.Y, Color);
        }
        public void DrawTriangle(Triangle<int> Triangle, Color Color)
        {
            DrawTriangle(Triangle.P1.X, Triangle.P1.Y, Triangle.P2.X, Triangle.P2.Y, Triangle.P3.X, Triangle.P3.Y, Color);
        }

        #endregion

        #region Image

        public void DrawImage(int X, int Y, FrameBuffer Image, bool Alpha = true)
        {
            if (Image == null)
            {
                throw new Exception("Cannot draw a null image file.");
            }
            if (!Alpha && X == 0 && Y == 0 && Image.Width == Width && Image.Height == Height)
            {
                MemoryOperations.Copy(Internal, Image.Internal, (int)Size);
                return;
            }
            if (!Alpha)
            {
                uint TWidth = Image.Width;
                uint THeight = Image.Height;
                uint PadX = 0;
                uint PadY = 0;

                if (X < 0)
                {
                    PadX = (uint)Math.Abs(X);
                    TWidth -= PadX;
                }
                if (Y < 0)
                {
                    PadY = (uint)Math.Abs(Y);
                    THeight -= PadY;
                }
                if (X + Image.Width >= Width)
                {
                    TWidth = Width - (uint)X;
                }
                if (Y + Image.Height >= Height)
                {
                    THeight = Height - (uint)Y;
                }

                for (uint IY = 0; IY < THeight; IY++)
                {
                    MemoryOperations.Copy(Internal + PadX + X + ((PadY + Y + IY) * Width), Image.Internal + PadX + ((PadY + IY) * Image.Width), (int)TWidth);
                }
                return;
            }

            for (int IX = 0; IX < Image.Width; IX++)
            {
                for (int IY = 0; IY < Image.Height; IY++)
                {
                    SetPixel(X + IX, Y + IY, Image.GetPixel(IX, IY));
                }
            }
        }

        #endregion

        #region Text

        public class Font
        {
            public Font(string Charset, MemoryStream MS, int Width, int Height)
            {
                this.Charset = Charset;
                this.MS = MS;
                this.Width = Width;
                this.Height = Height;
            }

            public static string DefaultCharset = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
            public static Font Default = new(DefaultCharset, new(Assets.Font1B), 8, 16);

            public string Charset;
            public MemoryStream MS;
            public int Width;
            public int Height;
        }

        public void DrawString(int X, int Y, string Text, Font Font, Color Color, bool Center = false)
        {
            if (Text == null || Text.Length == 0)
            {
                return;
            }
            string[] Lines = Text.Split('\n');

            for (int Line = 0; Line < Lines.Length; Line++)
            {
                for (int Char = 0; Char < Lines[Line].Length; Char++)
                {
                    // draw character in the middle of x and y
                    int IX = X + (Font.Width * Char);
                    int IY = Y + (Font.Height * Line);

                    // If center, move ix and iy to the center
                    if (Center)
                    {
                        IX -= Font.Width * Lines[Line].Length / 2;
                        IY -= Font.Height * Lines.Length / 2;
                    }

                    DrawChar(IX, IY, Lines[Line][Char], Font, Color);
                }
            }
        }

        public int DrawChar(int X, int Y, char Char, Font Font, Color Color)
        {
            int Index = Font.Charset.IndexOf(Char);
            if (Index == -1) return Font.Height / 2;

            int MaxX = 0;

            bool LastPixelIsNotDrawn = false;

            int SizePerFont = Font.Height * (Font.Height / Font.Width);
            byte[] BFont = new byte[SizePerFont];
            Font.MS.Seek(SizePerFont * Index, SeekOrigin.Begin);
            Font.MS.Read(BFont, 0, BFont.Length);

            for (int h = 0; h < Font.Height; h++)
            {
                for (int aw = 0; aw < Font.Height / Font.Width; aw++)
                {

                    for (int ww = 0; ww < 8; ww++)
                    {
                        if ((BFont[(h * (Font.Height / 8)) + aw] & (0x80 >> ww)) != 0)
                        {
                            SetPixel(X + (aw * 8) + ww, Y + h, Color);

                            if ((aw * 8) + ww > MaxX)
                            {
                                MaxX = (aw * 8) + ww;
                            }

                            if (LastPixelIsNotDrawn)
                            {
                                SetPixel(X + (aw * 8) + ww - 1, Y + h, Color.FromARGB(255, (byte)(Color.R / 2), (byte)(Color.G / 2), (byte)(Color.B / 2)));
                                LastPixelIsNotDrawn = false;
                            }
                        }
                        else
                        {
                            LastPixelIsNotDrawn = true;
                        }
                    }
                }
            }

            return MaxX;
        }

        #endregion

        #region Misc

        public void Clear()
        {
            Clear(Color.Black);
        }
        public void Clear(Color Color)
        {
            MemoryOperations.Fill(Internal, Color.ARGB, (int)Size);
        }

        // Possibly doesn't work
        public static FrameBuffer FromTGA(byte[] Binary)
        {
            uint i, j, k, x, y, w = (uint)((Binary[13] << 8) + Binary[12]), h = (uint)((Binary[15] << 8) + Binary[14]), o = (uint)((Binary[11] << 8) + Binary[10]);
            uint m = (uint)((Binary[1] != 0 ? (Binary[7] >> 3) * Binary[5] : 0) + 18);

            if (w < 1 || h < 1)
            {
                return null;
            }

            uint[] Data = new uint[(w * h + 2) * 4];

            if (Data.Length == 0)
                return null;

            switch (Binary[2])
            {
                case 1:
                    if (Binary[6] != 0 || Binary[4] != 0 || Binary[3] != 0 || (Binary[7] != 24 && Binary[7] != 32))
                    {
                        GCImplementation.Free(Data);
                        return null;
                    }
                    for (y = i = 0; y < h; y++)
                    {
                        k = ((o != 0 ? h - y - 1 : y) * w);
                        for (x = 0; x < w; x++)
                        {
                            j = (uint)(Binary[m + k++] * (Binary[7] >> 3) + 18);
                            Data[2 + i++] = (uint)(((Binary[7] == 32 ? Binary[j + 3] : 0xFF) << 24) | (Binary[j + 2] << 16) | (Binary[j + 1] << 8) | Binary[j]);
                        }
                    }
                    break;
                case 2:
                    if (Binary[5] != 0 || Binary[6] != 0 || Binary[1] != 0 || (Binary[16] != 24 && Binary[16] != 32))
                    {
                        GCImplementation.Free(Data);
                        return null;
                    }
                    for (y = i = 0; y < h; y++)
                    {
                        j = ((uint)((o != 0 ? h - y - 1 : y) * w * (Binary[16] >> 3)));
                        for (x = 0; x < w; x++)
                        {
                            Data[2 + i++] = (uint)(((Binary[16] == 32 ? Binary[j + 3] : 0xFF) << 24) | (Binary[j + 2] << 16) | (Binary[j + 1] << 8) | Binary[j]);
                            j += (uint)(Binary[16] >> 3);
                        }
                    }
                    break;
                case 9:
                    if (Binary[6] != 0 || Binary[4] != 0 || Binary[3] != 0 || (Binary[7] != 24 && Binary[7] != 32))
                    {
                        GCImplementation.Free(Data);
                        return null;
                    }
                    y = i = 0;
                    for (x = 0; x < w * h && m < Binary.Length;)
                    {
                        k = Binary[m++];
                        if (k > 127)
                        {
                            k -= 127; x += k;
                            j = (uint)(Binary[m++] * (Binary[7] >> 3) + 18);
                            while (k-- != 0)
                            {
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                Data[2 + i++] = (uint)(((Binary[7] == 32 ? Binary[j + 3] : 0xFF) << 24) | (Binary[j + 2] << 16) | (Binary[j + 1] << 8) | Binary[j]);
                            }
                        }
                        else
                        {
                            k++; x += k;
                            while (k-- != 0)
                            {
                                j = (uint)(Binary[m++] * (Binary[7] >> 3) + 18);
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                Data[2 + i++] = (uint)(((Binary[7] == 32 ? Binary[j + 3] : 0xFF) << 24) | (Binary[j + 2] << 16) | (Binary[j + 1] << 8) | Binary[j]);
                            }
                        }
                    }
                    break;
                case 10:
                    if (Binary[5] != 0 || Binary[6] != 0 || Binary[1] != 0 || (Binary[16] != 24 && Binary[16] != 32))
                    {
                        GCImplementation.Free(Data);
                        return null;
                    }
                    y = i = 0;
                    for (x = 0; x < w * h && m < Binary.Length;)
                    {
                        k = Binary[m++];
                        if (k > 127)
                        {
                            k -= 127; x += k;
                            while (k-- != 0)
                            {
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                Data[2 + i++] = (uint)(((Binary[16] == 32 ? Binary[m + 3] : 0xFF) << 24) | (Binary[m + 2] << 16) | (Binary[m + 1] << 8) | Binary[m]);
                            }
                            m += (uint)Binary[16] >> 3;
                        }
                        else
                        {
                            k++; x += k;
                            while (k-- != 0)
                            {
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                Data[2 + i++] = (uint)(((Binary[16] == 32 ? Binary[m + 3] : 0xFF) << 24) | (Binary[m + 2] << 16) | (Binary[m + 1] << 8) | Binary[m]);
                                m += (uint)(Binary[16] >> 3);
                            }
                        }
                    }
                    break;
                default:
                    GCImplementation.Free(Data);
                    return null;
            }
            Data[0] = w;
            Data[1] = h;

            FrameBuffer TMP = new(Data[0], Data[1]);
            for (int I = 0; I < TMP.Size; I++)
            {
                TMP.SetPixel(I, Color.FromARGB(Data[I + 2]));
            }
            return TMP;
        }
        public static FrameBuffer FromBitmap(byte[] Binary)
        {
            Cosmos.System.Graphics.Bitmap BMP = new(Binary);
            fixed (uint* PTR = (uint[])(object)BMP.rawData)
            {
                return new FrameBuffer(BMP.Width, BMP.Height) { Internal = PTR };
            }
        }

        public FrameBuffer Resize(uint Width, uint Height)
        {
            if (Width <= 0 || Height <= 0 || Width == this.Width || Height == this.Height)
            {
                return new(1, 1);
            }

            FrameBuffer FB = new(Width, Height);
            for (int IX = 0; IX < this.Width; IX++)
            {
                for (int IY = 0; IY < this.Height; IY++)
                {
                    long X = IX / (this.Width / Width);
                    long Y = IY / (this.Height / Height);
                    FB[(FB.Width * Y) + X] = this[(this.Width * IY) + IX];
                }
            }
            return FB;
        }

        public void Dispose()
        {
            if (Size != 0)
            {
                Cosmos.Core.Memory.Heap.Free(Internal);
            }
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}