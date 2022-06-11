using PrismOS.Libraries.Graphics;
using System;

namespace PrismOS.Libraries.Generators
{
    public static class Stars
    {
        public static void DrawStars(int Count, int MaxSize, int MinSize, int Width, int Height)
        {
            int X, Y, R;
            System.Random Random = new();
            Color Color1 = Color.Black;
            Color Color2 = Color.UbuntuPurple;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Canvas.Current.SetPixel(i, j, new Color(Color1.A, (byte)(Color1.R + (Color2.R - Color1.R) * i / Width), (byte)(Color1.G + (Color2.G - Color1.G) * i / Width), (byte)(Color1.B + (Color2.B - Color1.B) * i / Width)));
                }
            }
            for (int I = 0; I < Count; I++)
            {
                X = Random.Next(0, Width);
                Y = Random.Next(0, Height);
                R = Random.Next(MinSize, MaxSize);
                Canvas.Current.DrawFilledCircle(X, Y, R, Color.White);
            }

            X = Random.Next(0, Width);
            Y = Random.Next(0, Height / 2);
            R = Random.Next(15, 25);
            Canvas.Current.DrawFilledCircle(X, Y, R, Color.White);
        }
    }
}