using System.Drawing;
using Cosmos.HAL.Drivers;

namespace PrismOS.UI
{
    /// <summary>
    /// This is a test for a better canvas.
    /// </summary>
    public class PrismUI
    {
        public PrismUI(int aWidth, int aHeight)
        {
            Width = aWidth;
            Height = aHeight;
            Buffer = new int[aWidth * aHeight];
            Driver = new((ushort)aWidth, (ushort)aHeight, 32);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Buffer { get; set; }
        public VBEDriver Driver { get; set; }

        public void Update()
        {
            Driver.CopyVRAM(0, Buffer, 0, Buffer.Length);
        }

        public virtual void DrawLine(int X, int Y, int X2, int Y2, Color Color)
        {

        }

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
                int aColor = Color.ToArgb();

                for (int aX = X; aX < Size; aX++)
                {
                    for (int aY = Y; aY < Size; aY++)
                    {
                        Buffer[GetOffset(aX, aY)] = aColor;
                    }
                }
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

                        int x = (int)(150 + Size * System.Math.Cos(angle));
                        int y = (int)(150 + Size * System.Math.Sin(angle));

                        Buffer[GetOffset(x, y)] = aColor;
                    }

                }
            }
        #endregion Circles

        private int GetOffset(int X, int Y)
        {
            const int Stride = 32 / 8;
            int Pitch = Width * 32 / 8;

            return (X * Stride) + (Y * Pitch);
        }
    }
}