using PrismOS.Libraries.Resource.Images;
using IL2CPU.API.Attribs;
using XSharp.Assembler;
using Cosmos.Core;
using Cosmos.HAL;
using System;

namespace PrismOS.Libraries.Graphics
{
    public unsafe class FrameBuffer : IDisposable
    {
        public FrameBuffer(uint Width, uint Height)
        {
            this.Width = Width;
            this.Height = Height;

            Cosmos.HAL.Global.PIT.RegisterTimer(new PIT.PITTimer(() => { FPS = Frames; Frames = 0; }, 1000000000, true));
            Internal = (uint*)GCImplementation.AllocNewObject(Size * 4);
        }

        // Get/Set Pixels
        public Color this[uint X, uint Y]
        {
            get
            {
                return this[(int)X, (int)Y];
            }
            set
            {
                this[(int)X, (int)Y] = value;
            }
        }
        public Color this[int X, int Y]
        {
            get
            {
                if (X > Width || Y > Height || X < 0 || Y < 0)
                {
                    return Color.Black;
                }
                return Color.FromARGB(Internal[Y * Width + X]);
            }
            set
            {
                if (value.A == 0 || X > Width || Y > Height || X < 0 || Y < 0)
                {
                    return;
                }
                if (value.A < 255)
                {
                    value = Color.AlphaBlend(this[X, Y], value);
                }
                Internal[Y * Width + X] = value.ARGB;
            }
        }
        public Color this[uint Index]
        {
            get
            {
                return this[(int)Index];
            }
            set
            {
                this[(int)Index] = value;
            }
        }
        public Color this[int Index]
        {
            get
            {
                if (Index > Size)
                {
                    return Color.Black;
                }
                if (Index < 0)
                {
                    return Color.Black;
                }

                return Color.FromARGB(Internal[Index]);
            }
            set
            {
                if (value.A == 0 || Index > Size)
                {
                    return;
                }
                if (value.A < 255)
                {
                    value = Color.AlphaBlend(this[Index], value);
                }

                Internal[Index] = value.ARGB;
            }
        }

        public uint* Internal { get; set; }
        private uint Frames { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public uint FPS { get; set; }
        public uint Size
        {
            get
            {
                return Width * Height;
            }
        }
        
        #region Line

        public void DrawLine(int X1, int Y1, int X2, int Y2, Color Color, bool AntiAlias = false)
        {
            int DX = Math.Abs(X2 - X1), SX = X1 < X2 ? 1 : -1;
            int DY = Math.Abs(Y2 - Y1), SY = Y1 < Y2 ? 1 : -1;
            int err = (DX > DY ? DX : -DY) / 2;

            while (X1 != X2 || Y1 != Y2)
            {
                this[X1, Y1] = Color;
                if (AntiAlias)
                {
                    if (X1 + X2 > Y1 + Y2)
                    {
                        this[X1 + 1, Y1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
                        this[X1 + 1, Y1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
                    }
                    else
                    {
                        this[X1, Y1 + 1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
                        this[X1, Y1 - 1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
                    }
                }
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

        #endregion

        #region Bezier Curves

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
                this[(int)XU, (int)YU] = Color;
            }
        }

        #endregion

        #region Rectangle

        public void DrawRectangle(int X, int Y, int Width, int Height, int Radius, Color Color)
        {
            if (Radius > 0)
            {
                DrawArc(Radius + X, Radius + Y, Radius, Color, 180, 270); // Top left
                DrawArc(X + Width - Radius, Y + Height - Radius, Radius, Color, 0, 90); // Bottom right
                DrawArc(Radius + X, Y + Height - Radius, Radius, Color, 90, 180); // Bottom left
                DrawArc(X + Width - Radius, Radius + Y, Radius, Color, 270, 360);
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
                    Fill(Internal + ((Y + IY) * this.Width + X), Color.ARGB, (uint)Width);
                }
                return;
            }
            if (Radius == 0)
            {
                for (int IX = X; IX < X + Width; IX++)
                {
                    for (int IY = Y; IY < Y + Height; IY++)
                    {
                        this[X, IY] = Color;
                    }
                }
            }
            else
            {
                DrawFilledCircle(X + Radius, Y + Radius, Radius, Color);
                DrawFilledCircle(X + Width - Radius - 1, Y + Radius, Radius, Color);

                DrawFilledCircle(X + Radius, Y + Height - Radius - 1, Radius, Color);
                DrawFilledCircle(X + Width - Radius - 1, Y + Height - Radius - 1, Radius, Color);

                DrawFilledRectangle(X + Radius, Y, Width - Radius * 2, Height, 0, Color);
                DrawFilledRectangle(X, Y + Radius, Width, Height - Radius * 2, 0, Color);
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

        #region Triangle

        public void DrawFilledTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            // 28.4 fixed-point coordinates
            Y1 = (int)Math.Round(16.0f * Y1);
            Y2 = (int)Math.Round(16.0f * Y2);
            Y3 = (int)Math.Round(16.0f * Y3);

            X1 = (int)Math.Round(16.0f * X1);
            X2 = (int)Math.Round(16.0f * X2);
            X3 = (int)Math.Round(16.0f * X3);

            // Deltas
            int DX12 = X1 - X2;
            int DX23 = X2 - X3;
            int DX31 = X3 - X1;

            int DY12 = Y1 - Y2;
            int DY23 = Y2 - Y3;
            int DY31 = Y3 - Y1;

            // Fixed-point deltas
            int FDX12 = DX12 << 4;
            int FDX23 = DX23 << 4;
            int FDX31 = DX31 << 4;

            int FDY12 = DY12 << 4;
            int FDY23 = DY23 << 4;
            int FDY31 = DY31 << 4;

            // Bounding rectangle
            int minx = (Math.Min(Math.Min(X1, X2), X3) + 0xF) >> 4;
            int maxx = (Math.Max(Math.Max(X1, X2), X3) + 0xF) >> 4;
            int miny = (Math.Min(Math.Min(Y1, Y2), Y3) + 0xF) >> 4;
            int maxy = (Math.Max(Math.Max(Y1, Y2), Y3) + 0xF) >> 4;

            // Half-edge constants
            int C1 = DY12 * X1 - DX12 * Y1;
            int C2 = DY23 * X2 - DX23 * Y2;
            int C3 = DY31 * X3 - DX31 * Y3;

            // Correct for fill convention
            if (DY12 < 0 || (DY12 == 0 && DX12 > 0)) C1++;
            if (DY23 < 0 || (DY23 == 0 && DX23 > 0)) C2++;
            if (DY31 < 0 || (DY31 == 0 && DX31 > 0)) C3++;

            int CY1 = C1 + DX12 * (miny << 4) - DY12 * (minx << 4);
            int CY2 = C2 + DX23 * (miny << 4) - DY23 * (minx << 4);
            int CY3 = C3 + DX31 * (miny << 4) - DY31 * (minx << 4);

            for (int Y = miny; Y < maxy; Y++)
            {
                int CX1 = CY1;
                int CX2 = CY2;
                int CX3 = CY3;

                for (int X = minx; X < maxx; X++)
                {
                    if (CX1 > 0 && CX2 > 0 && CX3 > 0)
                    {
                        this[X, Y] = Color;
                    }

                    CX1 -= FDY12;
                    CX2 -= FDY23;
                    CX3 -= FDY31;
                }

                CY1 += FDX12;
                CY2 += FDX23;
                CY3 += FDX31;
            }
        }

        public void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            DrawLine(X1, Y1, X2, Y2, Color);
            DrawLine(X1, Y1, X3, Y3, Color);
            DrawLine(X2, Y2, X3, Y3, Color);
        }

        #endregion

        #region Circle 

        public void DrawCircle(int X, int Y, int Radius, Color Color)
        {
            int IX = 0, IY = Radius, DP = 3 - 2 * Radius;

            while (IY >= IX)
            {
                this[X + IX, Y + IY] = Color;
                this[X - IX, Y + IY] = Color;
                this[X + IX, Y - IY] = Color;
                this[X - IX, Y - IY] = Color;
                this[X + IY, Y + IX] = Color;
                this[X - IY, Y + IX] = Color;
                this[X + IY, Y - IX] = Color;
                this[X - IY, Y - IX] = Color;

                IX++;
                if (DP > 0)
                {
                    IY--;
                    DP += 4 * (IX - IY) + 10;
                }
                else
                {
                    DP += 4 * IX + 6;
                }
            }
        }
        public void DrawFilledCircle(int X, int Y, int Radius, Color Color)
        {
            if (Radius == 0)
            {
                return;
            }
            if (Color.A == 255)
            {
                int R2 = Radius * Radius;
                for (int IY = -Radius; IY <= Radius; IY++)
                {
                    int IX = (int)(Math.Sqrt(R2 - IY * IY) + 0.5);

                    uint* Offset = Internal + (Width * (Y + IY)) + X - IX;

                    Fill(Offset, Color.ARGB, (uint)(IX * 2));
                }
            }
            else
            {
                for (int IX = -Radius; IX < Radius; IX++)
                {
                    int Height = (int)Math.Sqrt((Radius * Radius) - (IX * IX));

                    for (int IY = -Height; IY < Height; IY++)
                    {
                        this[IX + X, IY + Y] = Color;
                    }
                }
            }
        }

        #endregion

        #region Arc

        public void DrawArc(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
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
                this[X + IX, Y + IY] = Color;
            }
        }
        public void DrawFilledArc(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
            {
                return;
            }

            for (int I = 0; I < Radius; I++)
            {
                DrawArc(X, Y, I, Color, StartAngle, EndAngle);
            }
        }

        #endregion

        #region Memory

        [PlugMethod(Assembler = typeof(AsmCopyUint))]
        public static void Copy(uint* Destination, uint* Source, uint Length)
        {
            throw new NotImplementedException();
        }
        [PlugMethod(Assembler = typeof(AsmSetUint))]
        public static void Fill(uint* Destination, uint Value, uint Length)
        {
            throw new NotImplementedException();
        }
        public class AsmSetUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov eax, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep stosd");
            }
        }
        public class AsmCopyUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov esi, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep movsd");
            }
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
                Copy(Internal, Image.Internal, Size);
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
                    Copy(Internal + PadX + X + ((PadY + Y + IY) * Width), Image.Internal + PadX + ((PadY + IY) * Image.Width), TWidth);
                }
                return;
            }

            for (int IX = 0; IX < Image.Width; IX++)
            {
                for (int IY = 0; IY < Image.Height; IY++)
                {
                    this[X + IX, Y + IY] = Image[IX, IY];
                }
            }
        }

        #endregion

        #region Text

        public void DrawString(int X, int Y, string Text, Font Font, Color Color, bool Center = false)
        {
            if (Text == null || Text.Length == 0)
            {
                return;
            }
            string[] Lines = Text.Split('\n');

            // Loop Through Each Line Of Text
            for (int Line = 0; Line < Lines.Length; Line++)
            {
                // Advanced Calculations To Determine Position
                int IX = X - (Center ? ((int)Font.MeasureString(Text) / 2) : 0);
                int IY = (int)(Y + (Font.Size * Line) - (Center ? Font.Size * Lines.Length / 2 : 0));

                if (IY > Height)
                {
                    return;
                }

                // Loop Though Each Char In The Line
                for (int Char = 0; Char < Lines[Line].Length; Char++)
                {
                    if (IX > Width)
                    {
                        continue;
                    }
                    IX += (int)(DrawChar(IX, IY, Lines[Line][Char], Font, Color, Center) + 2);
                }
            }
        }

        public uint DrawChar(int X, int Y, char Char, Font Font, Color Color, bool Center)
        {
            uint Index = (uint)Font.Charset.IndexOf(Char);
            if (Char == ' ')
            {
                return Font.Size2;
            }
            if (Char == '\t')
            {
                return Font.Size2 * 4;
            }
            if (Index < 0)
            {
                return Font.Size2;
            }
            if (Center)
            {
                X -= (int)Font.Size16;
                Y -= (int)Font.Size8;
            }

            uint MaxX = 0;
            uint SizePerFont = Font.Size * Font.Size8 * Index;

            for (int h = 0; h < Font.Size; h++)
            {
                for (int aw = 0; aw < Font.Size8; aw++)
                {
                    for (int ww = 0; ww < 8; ww++)
                    {
                        if ((Font.Binary[SizePerFont + (h * Font.Size8) + aw] & (0x80 >> ww)) != 0)
                        {
                            int max = (aw * 8) + ww;

                            int x = X + max;
                            int y = Y + h;

                            this[x, y] = Color;

                            if (max > MaxX)
                            {
                                MaxX = (uint)max;
                            }
                        }
                    }
                }
            }
            return MaxX;
        }

        #endregion

        #region Misc

        public void Clear(Color Color)
        {
            Fill(Internal, Color.ARGB, Size);
        }
        public void Clear()
        {
            Clear(Color.Black);
        }

        public void CopyTo(uint* Destination)
        {
            Frames++;
            Copy(Destination, Internal, Size);
        }

        public FrameBuffer Resize(uint Width, uint Height)
        {
            if (Width <= 0 || Height <= 0 || Width == this.Width || Height == this.Height)
            {
                return this;
            }

            FrameBuffer FB = new(Width, Height);
            for (int IX = 0; IX < this.Width; IX++)
            {
                for (int IY = 0; IY < this.Height; IY++)
                {
                    long X = IX / (this.Width / Width);
                    long Y = IY / (this.Height / Height);
                    FB.Internal[(FB.Width * Y) + X] = Internal[(this.Width * IY) + IX];
                }
            }
            return FB;
        }
        public FrameBuffer Rotate(int Degrees)
        {
            FrameBuffer T = new(Width, Height);
            double Angle = Numerics.Math2.Degrees(Degrees);
            uint CenterX = Width / 2;
            uint CenterY = Height / 2;

            for (int X = 0; X < Width; X++)
            {
                for (int Y = 0; Y < Height; Y++)
                {
                    int XP = (int)((X - CenterX) * Math.Cos(Angle) - (Y - CenterY) * Math.Sin(Angle) + CenterX);
                    int YP = (int)((X - CenterX) * Math.Sin(Angle) + (Y - CenterY) * Math.Cos(Angle) + CenterY);
                    if (0 <= XP || XP < Width && 0 <= YP && YP < Height)
                    {
                        T[X, Y] = this[XP, YP];
                    }
                }
            }
            return T;
        }

        public static FrameBuffer FromImage(byte[] Binary)
        {
            if (PNG.Validate(Binary))
            {
                return new PNG(Binary);
            }
            if (BMP.Validate(Binary))
            {
                return new BMP(Binary);
            }
            else
            {
                return new TGA(Binary);
            }
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