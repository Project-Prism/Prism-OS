using Color = PrismOS.UI.Common.Color;
using Global = Cosmos.Core.Global;
using VBE = Cosmos.HAL.Drivers.VBEDriver;
using System;

namespace PrismOS.UI
{
    public class Canvas
    {
        public Canvas(int Width, int Height)
        {
            VBE = new((ushort)Width, (ushort)Height, 32);
            this.Width = Width;
            this.Height = Height;
            Buffer = new int[Width * Height];
            Update();
        }

        #region Properties
        public int[] Buffer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public VBE VBE { get; set; }
        #endregion Properties

        #region Drawing
        public void SetPixel(int X, int Y, Color Color)
        {
            if (Color.Alpha < 255 && Color.Alpha != 0)
            {
                Color = AlphaBlend(GetPixel(X, Y), Color);
            }
            Buffer[X + (Width * Y)] = Color.ARGB;
        }
        public Color GetPixel(int X, int Y)
        {
            return new(Buffer[X + (Width * Y)]);
        }

        public void DrawLine(int FromX, int FromY, int ToX, int ToY, Color Color)
        {
            int i, sdx, sdy, dxabs, dyabs, x, y, px, py;

            dxabs = Math.Abs(FromX);
            dyabs = Math.Abs(FromY);
            sdx = Math.Sign(FromX);
            sdy = Math.Sign(FromY);
            x = dyabs >> 1;
            y = dxabs >> 1;
            px = ToX;
            py = ToY;

            if (dxabs >= dyabs) /* the line is more horizontal than vertical */
            {
                for (i = 0; i < dxabs; i++)
                {
                    y += dyabs;
                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }
                    px += sdx;
                    SetPixel(px, py, Color);
                }
            }
            else /* the line is more vertical than horizontal */
            {
                for (i = 0; i < dyabs; i++)
                {
                    x += dxabs;
                    if (x >= dyabs)
                    {
                        x -= dyabs;
                        px += sdx;
                    }
                    py += sdy;
                    SetPixel(px, py, Color);
                }
            }
        }
        public void DrawRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            DrawLine(X, Y, X + Width, Y, Color); // Top Line
            DrawLine(X, Y, X, Y + Height, Color); // Left Line
            DrawLine(Width + X, Y + Height, Width + X, Y, Color); // Right Line
            DrawLine(Width + X, Y + Height, X, Height + Y, Color); // Bottom Line
        }
        public void DrawFilledRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            X -= Width / 2;
            Y -= Height / 2;

            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    SetPixel(X + w, Y + h, Color);
                }
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

        public static Color AlphaBlend(Color PixelColor, Color SetColor)
        {
            int R = (SetColor.Red * SetColor.Alpha) + (PixelColor.Red * (255 - SetColor.Alpha)) >> 8;
            int G = (SetColor.Green * SetColor.Alpha) + (PixelColor.Green * (255 - SetColor.Alpha)) >> 8;
            int B = (SetColor.Blue * SetColor.Alpha) + (PixelColor.Blue * (255 - SetColor.Alpha)) >> 8;
            return new(R, G, B);
        }
        public void Clear(Color Color)
        {
            Array.Fill(Buffer, Color.ARGB);
        }
        public void Update()
        {
            Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy(Buffer, 0, Buffer.Length);
        }
        #endregion Drawing
    }
}