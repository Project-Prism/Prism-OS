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
        }

        #region Properties
        public VBEDriver VBE { get; }
        public int[] Buffer { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int FPS { get; private set; }
        public bool ShowFPS { get; set; }
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
            int w = X2 - X1;
            int h = Y2 - Y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (longest <= shortest)
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;

            int color = Color.ToArgb();
            for (int i = 0; i <= longest; i++)
            {
                
                if (Color.A < 255)
                {
                    Color = AlphaBlend(GetPixel(X1, Y1), Color);
                }
                Buffer[(Width * Y1) + X1] = color;
                
                numerator += shortest;
                if (numerator >= longest)
                {
                    numerator -= longest;
                    X1 += dx1;
                    Y1 += dy1;
                }
                else
                {
                    X1 += dx2;
                    Y1 += dy2;
                }
            }
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
                DrawLine(IX, Y, IX, Height, Color);
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
