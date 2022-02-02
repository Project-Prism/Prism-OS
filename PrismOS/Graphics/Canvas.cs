using System;
using Color = System.Drawing.Color;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using Cosmos.System.Graphics.Fonts;

namespace PrismOS.Graphics
{
    public class Canvas
    {
        public Canvas(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new int[Width * Height];
            AABuffer = new int[Width * Height];
            VBE = new((ushort)Width, (ushort)Height, 32);
            GetCanvas = this;
            Clear(Color.Black);
        }

        #region Properties
        public static Canvas GetCanvas { get; set; }
        public VBEDriver VBE { get; set; }
        public int[] Buffer { get; set; }
        public int[] AABuffer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        #region Drawing

        #region Pixel
        public void SetPixel(int X, int Y, Color Color, bool Smooth = false)
        {
            if (X > Width || X < 0 || Y > Height || Y < 0 || Color.A == 0)
            {
                return;
            }
            if (Color.A < 255)
            {
                Color = AlphaBlend(GetPixel(X, Y), Color);
            }
            if (Smooth)
            {
                Color Color2 = Color.FromArgb(Color.A / 4, Color);
                // Draw antialiasing
                AABuffer[(Width * (Y - 1)) + X] = Color2.ToArgb();
                AABuffer[(Width * (Y + 1)) + X] = Color2.ToArgb();
                AABuffer[(Width * Y) + (X - 1)] = Color2.ToArgb();
                AABuffer[(Width * Y) + (X + 1)] = Color2.ToArgb();
            }

            // Draw main pixel
            Buffer[(Width * Y) + X] = Color.ToArgb();
        }
        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(Buffer[(Width * Y) + X]);
        }
        #endregion

        #region Line
        public void DrawLine(int X, int Y, int X2, int Y2, Color Color)
        {
            int dx = X2 - X;
            int dy = Y2 - Y;
            int p = (2 * dy) - dx;
            while (X <= X2)
            {
                if (p < 0)
                {
                    X++;
                    p += 2 * dy;
                }
                else
                {
                    X++;
                    Y++;
                    p += 2 * (dy - dx);
                }
                SetPixel(X, Y, Color, true);
            }
        }
        public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
        {
            DrawLine(X, Y, (int)(X + (Math.Cos(Angle) * Radius)), (int)(Y + (Math.Sin(Angle) * Radius)), Color);
            System.Threading.Thread.Sleep(1);
            return;

            int[] sine = new int[16] { 0, 27, 54, 79, 104, 128, 150, 171, 190, 201, 221, 233, 243, 250, 254, 255 };
            int xEnd, yEnd, quadrant, x_flip, y_flip;
            quadrant = Angle / 15;

            switch (quadrant)
            {
                case 0: x_flip = 1; y_flip = -1; break;
                case 1: Angle = Math.Abs(Angle - 30); x_flip = y_flip = 1; break;
                case 2: Angle -= 30; x_flip = -1; y_flip = 1; break;
                case 3: Angle = Math.Abs(Angle - 60); x_flip = y_flip = -1; break;
                default: x_flip = y_flip = 1; break;
            }

            xEnd = X;
            yEnd = Y;

            if (Angle > sine.Length) return;

            xEnd += x_flip * ((sine[Angle] * Radius) >> 8);
            yEnd += y_flip * ((sine[15 - Angle] * Radius) >> 8);

            DrawLine(X, Y, xEnd, yEnd, Color);
        }
        #endregion

        #region Rectangle
        public void DrawRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            DrawLine(X, Y, X + Width, Y, Color); // Top Line
            DrawLine(X, Y, X, Y + Height, Color); // Left Line
            DrawLine(Width + X, Y + Height, Width + X, Y, Color); // Right Line
            DrawLine(Width + X, Y + Height, X, Height + Y, Color); // Bottom Line
        }
        public void DrawFilledRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            for (int IY = Y; IY < Y + Height; IY++)
            {
                for (int IX = X; IX < X + Width; IX++)
                {
                    SetPixel(IX, IY, Color);
                }
            }
        }
        #endregion

        #region Circle
        public void DrawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            for (double I = StartAngle; I < EndAngle; I += 0.05)
            {
                SetPixel((int)(X + (Math.Cos(I) * Radius)), (int)(Y + (Math.Sin(I) * Radius)), Color);
            }
        }
        public void DrawFilledCircle(int X, int Y, int Radius, Color Color)
        {
            int y = 0;
            int xChange = 1 - (Radius << 1);
            int yChange = 0;
            int radiusError = 0;

            while (Radius >= y)
            {
                for (int I = X - Radius; I <= X + Radius; I++)
                {
                    SetPixel(I, Y + y, Color);
                    SetPixel(I, Y - y, Color);
                }
                for (int I = X - y; I <= X + y; I++)
                {
                    SetPixel(I, Y + Radius, Color);
                    SetPixel(I, Y - Radius, Color);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    Radius--;
                    radiusError += xChange;
                    xChange += 2;
                }
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
        #endregion

        #region Image
        public void DrawBitmap(int X, int Y, Bitmap Bitmap)
        {
            for (int IX = 0; IX < Bitmap.Width; IX++)
            {
                for (int IY = 0; IY < Bitmap.Height; IY++)
                {
                    SetPixel(X + IX, Y + IY, Color.FromArgb(Bitmap.rawData[(Bitmap.Width * IY) + IX]));
                }
            }
        }
        #endregion

        #region Text
        public void DrawChar(int X, int Y, PCScreenFont Font, char Char, Color Color)
        {
            int p = Font.Height * (byte)Char;

            for (int cy = 0; cy < Font.Height; cy++)
            {
                for (byte cx = 0; cx < Font.Width; cx++)
                {
                    if (Font.ConvertByteToBitAddres(Font.Data[p + cy], cx + 1))
                    {
                        SetPixel(X + (Font.Width - cx), Y + cy, Color);
                    }
                }
            }
        }
        public void DrawString(int X, int Y, PCScreenFont Font, string String, Color Color)
        {
            foreach (char Char in String)
            {
                DrawChar(X, Y, Font, Char, Color);
                X += Font.Width;
            }
        }
        #endregion

        #region Misc
        public void Clear(Color Color = default)
        {
            if (Color == default)
            {
                Color = Color.Black;
            }

            Array.Fill(Buffer, Color.ToArgb());
            Array.Fill(AABuffer, Color.Transparent.ToArgb());
        }
        public void Update()
        {
            // Copy antialising to the buffer
            for (int IX = 0; IX < Width; IX++)
            {
                for (int IY = 0; IY < Height; IY++)
                {
                    SetPixel(IX, IY, Color.FromArgb(AABuffer[(Width * IY) + IX]));
                }
            }
            Cosmos.Core.Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy(Buffer, 0, Buffer.Length);
        }
        private static Color AlphaBlend(Color PixelColor, Color SetColor)
        {
            int R = (SetColor.R * SetColor.A) + (PixelColor.R * (255 - SetColor.A)) >> 8;
            int G = (SetColor.G * SetColor.A) + (PixelColor.G * (255 - SetColor.A)) >> 8;
            int B = (SetColor.B * SetColor.A) + (PixelColor.B * (255 - SetColor.A)) >> 8;
            return Color.FromArgb(R, G, B);
        }
        #endregion

        #endregion
    }
}