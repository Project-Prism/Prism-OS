using System.Drawing;
using Image = Cosmos.System.Graphics.Image;
using Cosmos.HAL.Drivers;

namespace PrismOS.UI
{
    public static class Framework
    {
        public class Theme
        {
            public Theme(int[] Theme)
            {
                BackGround = Color.FromArgb(Theme[0]);
                ForeGround = Color.FromArgb(Theme[1]);
                Text = Color.FromArgb(Theme[2]);
            }

            public Theme(byte[] Theme)
            {
                BackGround = Color.FromArgb(Theme[0]);
                ForeGround = Color.FromArgb(Theme[1]);
                Text = Color.FromArgb(Theme[2]);
            }

            public Color ForeGround { get; set; }
            public Color BackGround { get; set; }
            public Color Text { get; set; }
        }

        /// <summary>
        /// A new canvas class, not fully implemented.
        /// </summary>
        public class Canvas
        {
            public Canvas(int aWidth, int aHeight)
            {
                Width = aWidth;
                Height = aHeight;
                Buffer = new int[aWidth * aHeight];
                Driver = new((ushort)aWidth, (ushort)aHeight, 32);
                DriverName = nameof(Driver.GetType);
            }

            public int Width { get; set; }
            public int Height { get; set; }
            public int[] Buffer { get; set; }
            public VBEDriver Driver { get; set; }
            public string DriverName { get; set; }

            public void Update()
            {
                Driver.CopyVRAM(0, Buffer, 0, Buffer.Length);
            }

            #region Lines
            public virtual void DrawLine(int X, int Y, int X2, int Y2, Color Color)
            {
                // Not implemented yet.
            }

            public virtual void DrawLine(int X, int Y, double Angle, Color Color)
            {
            }
            #endregion Lines

            #region Squares
            public virtual void DrawFilledSquare(int X, int Y, int Width, int Height, Color Color)
            {
                int aColor = Color.ToArgb();

                for (int aX = X; aX < Width; aX++)
                {
                    for (int aY = Y; aY < Width; aY++)
                    {
                        Buffer[GetOffset(aX, aY)] = aColor;
                    }
                }
            }

            public virtual void DrawFilledSquare(int X, int Y, int Size, Color Color)
            {
                DrawFilledSquare(X, Y, Size, Size, Color);
            }

            public virtual void DrawSquare(int X, int Y, int Width, int Height, Color Color)
            {
                DrawLine(X, Y, X + Width, Y, Color);
                DrawLine(X, Y, X, Y + Height, Color);
                DrawLine(X + Width, Y, X + Width, Y + Height, Color);
                DrawLine(X, Y + Height, X + Width, Y + Height, Color);
            }

            public virtual void DrawSquare(int X, int Y, int Size, Color Color)
            {
                DrawSquare(X, Y, Size, Size, Color);
            }
            #endregion Squares

            #region Circles
            public virtual void DrawFilledCircle(int X, int Y, int Size, Color Color)
            {
                int aColor = Color.ToArgb();

                int x = Size;
                int y = 0;
                int xChange = 1 - (Size << 1);
                int yChange = 0;
                int radiusError = 0;

                while (x >= y)
                {
                    for (int i = X - x; i <= X + x; i++)
                    {
                        Buffer[GetOffset(i, Y + y)] = aColor;
                        Buffer[GetOffset(i, Y - y)] = aColor;
                    }
                    for (int i = X - y; i <= X + y; i++)
                    {
                        Buffer[GetOffset(i, Y + x)] = aColor;
                        Buffer[GetOffset(i, Y - x)] = aColor;
                    }

                    y++;
                    radiusError += yChange;
                    yChange += 2;
                    if (((radiusError << 1) + xChange) > 0)
                    {
                        x--;
                        radiusError += xChange;
                        xChange += 2;
                    }
                }
            }

            public virtual void DrawCircle(int X, int Y, int Size, Color Color)
            {
                int aColor = Color.ToArgb();

                for (int j = 1; j <= 25; j++)
                {
                    Size = (j + 1) * 5;

                    for (double i = 0.0; i < 360.0; i += 0.1)
                    {
                        double angle = i * System.Math.PI / 180;

                        int x = (int)(150 + (Size * System.Math.Cos(angle)));
                        int y = (int)(150 + (Size * System.Math.Sin(angle)));

                        Buffer[GetOffset(x, y)] = aColor;
                    }
                }
            }
            #endregion Circles

            #region Text
            public virtual void DrawChar()
            {
                // not implemented yet
            }
            public virtual void DrawString(int X, int Y, Cosmos.System.Graphics.Fonts.Font Font, string Text, Color Color)
            {
                // Not implemented yet
            }
            #endregion Text

            #region Images
            public virtual void DrawImage(int X, int Y, Image Image)
            {
                // Not implemented yet.
            }
            #endregion Images

            #region Clear
            public virtual void Clear()
            {
                Clear(Color.Black);
            }
            public virtual void Clear(Color Color)
            {
                DrawFilledSquare(0, 0, Width, Height, Color);
            }
            #endregion Clear

            private int GetOffset(int X, int Y)
            {
                const int Stride = 32 / 8;
                int Pitch = Width * 32 / 8;

                return (X * Stride) + (Y * Pitch);
            }
        }
    }
}