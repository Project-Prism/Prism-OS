using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;

namespace Prism.Graphics
{
    /// <summary>
    /// Doubble buffered array canvas
    /// </summary>
    class BitmapCanvas
    {
        public int Width = 800;
        public int Height = 600;
        public Bitmap Canvas;

        public BitmapCanvas(Bitmap aImage)
        {
            Canvas = aImage;
        }

        public void SetPixel(int X, int Y, Color aColor)
        {
            Canvas.rawData[X + Y * Canvas.Width] = aColor.ToArgb();
        }

        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(Canvas.rawData[X + Y * Canvas.Width]);
        }

        public void Clear()
        {
            for (int X = 0; X < Width; X++)
            {
                for (int Y = 0; Y < Height; Y++)
                {
                    Canvas.rawData[X + Y * Canvas.Width] = Color.Black.ToArgb();
                }
            }
        }

        public void DrawFilledRectangle(int X, int Y, int Width, int Height, Color aColor)
        {
            for (int aX = X; X < Width; X++)
            {
                for (int aY = Y; Y < Height; Y++)
                {
                    Canvas.rawData[X + Y * Canvas.Width] = aColor.ToArgb();
                }
            }
        }

        public void DrawFilledCircle(int X, int Y, int aR, Color aColor)
        {
            int r2 = aR * aR;
            int area = r2 << 2;
            int rr = aR << 1;

            for (int i = 0; i < area; i++)
            {
                int tx = (i % rr) - aR;
                int ty = (i / rr) - aR;

                if (tx * tx + ty * ty <= r2)
                {
                    Canvas.rawData[X + Y * Canvas.Width] = aColor.ToArgb();
                }
            }
        }

        public void DrawImageAlpha(int X, int Y, Image aImage)
        {
            X -= (int)(aImage.Width / 2);
            Y -= (int)(aImage.Height / 2);

            for (int _x = 0; _x < aImage.Width; _x++)
            {
                for (int _y = 0; _y < aImage.Height; _y++)
                {
                    Canvas.rawData[X + Y * Canvas.Width] = aImage.rawData[_x + _y * aImage.Width];
                }
            }
        }

        public void DrawString(int X, int Y, Font aFont, string Text, Color aColor)
        {
            foreach (char aChar in Text)
            {
                DrawChar(X, Y, aChar, aFont, aColor);
                X += aFont.Width;
            }
        }

        public void DrawChar(int X, int Y, char aChar, Font aFont, Color aColor)
        {
            int p = aFont.Height * (byte)aChar;

            for (int aX = 0; aX < aFont.Width; aX++)
            {
                for (int aY = 0; aY < aFont.Height; aY++)
                {
                    Canvas.rawData[X + Y * Canvas.Width] = aFont.Data[p + aY];
                }
            }
        }

        public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color color)
        {
            // still needs testing

            double angleX, angleY;
            angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
            angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);

            //Display.DrawLine(new Pen(color), X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100));
        }

        public void DrawRoundRect(int X, int Y, int Width, int Height, int aR, Color aColor)
        {
            // Still needs testing

            DrawFilledRectangle(X, Y + aR, Width, Height - (aR * 2), aColor);
            DrawFilledRectangle(X + aR, Y, Width - (aR * 2), Height, aColor);

            //int x2 = aX + Width, y2 = aY + Height, r2 = aR + aR;
            //DrawFilledRectangle(aX, aY + aR, Width, Height - r2, aColor); // Left Right
            //DrawFilledRectangle(aX + aR, aY, Width - r2, aR, aColor); //Bottom Bar
            //DrawFilledRectangle(aX + aR, y2 - aR, Width - r2, aR, aColor); //Top Bar

            DrawFilledCircle(X + aR, Y + aR, aR, aColor); // Top left
            DrawFilledCircle(Width - aR - 1, Y + aR, aR, aColor); // Top Right
            DrawFilledCircle(1 + aR, Height - aR - 1, aR, aColor); // Bottom Left
            DrawFilledCircle(Width - aR - 1, Height - aR - 1, aR, aColor); // Bottom Right
        }
    }
}
