using PCScreenFont = Cosmos.System.Graphics.Fonts.PCScreenFont;
using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using Color = System.Drawing.Color;
using System;

namespace PrismOS.UI
{
    public class Canvas
    {
        public Canvas(int Width, int Height)
        {
            VBE = new((ushort)Width, (ushort)Height, 32);
            Buffer = new int[Width * Height];
            this.Width = Width;
            this.Height = Height;
            Update();
            GetCanvas = this;
        }

        #region Properties
        public static Canvas GetCanvas { get; set; }
        public VBEDriver VBE { get; }
        public int[] Buffer { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int FPS { get; private set; }
        private int Frames { get; set; }
        private DateTime LT { get; set; }
        #endregion

        #region Drawing

        #region Pixel
        public void SetPixel(int X, int Y, Color Color)
        {
            if (X > Width || X < 0 || Y > Height || Y < 0)
            {
                return;
            }
            if (Color.A < 255)
            {
                Color = AlphaBlend(GetPixel(X, Y), Color);
            }
            Buffer[(Width * Y) + X] = Color.ToArgb();
        }
        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(Buffer[(Width * Y) + X]);
        }
        #endregion

        #region Line
        public void DrawLine(int X1, int Y1, int X2, int Y2, Color Color)
        {
            bool yLonger = false;
            int incrementVal;

            int shortLen = Y2 - Y1;
            int longLen = X2 - X1;
            if (Math.Abs(shortLen) > Math.Abs(longLen))
            {
                int swap = shortLen;
                shortLen = longLen;
                longLen = swap;
                yLonger = true;
            }

            if (longLen < 0) incrementVal = -1;
            else incrementVal = 1;

            double divDiff;
            if (shortLen == 0) divDiff = longLen;
            else divDiff = (double)longLen / (double)shortLen;
            if (yLonger)
            {
                for (int i = 0; i != longLen; i += incrementVal)
                {
                    SetPixel(X1 + (int)((double)i / divDiff), Y1 + i, Color);
                }
            }
            else
            {
                for (int i = 0; i != longLen; i += incrementVal)
                {
                    SetPixel(X1 + i, Y1 + (int)((double)i / divDiff), Color);
                }
            }
        }

        public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
        {
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

            xEnd += (x_flip * ((sine[Angle] * Radius) >> 8));
            yEnd += (y_flip * ((sine[15 - Angle] * Radius) >> 8));

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
            for (int IX = X; IX < X + Width; IX++)
            {
                for (int IY = Y; IY < Y + Height; IY++)
                {
                    SetPixel(IX, IY, Color);
                }
            }
        }
        #endregion

        #region Circle
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
        public void Clear(Color Color)
        {
            Array.Fill(Buffer, Color.ToArgb());
        }
        public void Update()
        {
            // Calculate fps
            Frames++;
            if ((DateTime.Now - LT).TotalSeconds >= 1)
            {
                FPS = Frames;
                Frames = 0;
                LT = DateTime.Now;
            }

            // Copy buffer to vram
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