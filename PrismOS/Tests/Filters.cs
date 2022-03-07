using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismOS.Tests
{
    public static class Filters
    {
        private static Random Random { get; } = new();
        private static Bitmap LastFrame;

        public static Bitmap AddGrayScale(Bitmap Bitmap)
        {
            for (int I = 0; I < Bitmap.rawData.Length; I++)
            {
                Color C = Color.FromArgb(Bitmap.rawData[I]);
                Bitmap.rawData[I] = Color.FromArgb(C.R / 3, C.G / 3, C.B / 3).ToArgb();
            }
            return Bitmap;
        }

        public static Bitmap AddThreshHold(Bitmap Bitmap, byte MinValue, byte MaxValue)
        {
            for (int I = 0; I < Bitmap.rawData.Length; I++)
            {
                Color C = Color.FromArgb(Bitmap.rawData[I]);
                int B = (C.R / 3) + (C.G / 3) + (C.B / 3);

                if (B < MinValue || B > MaxValue)
                {
                    Bitmap.rawData[I] = Color.Black.ToArgb();
                }
            }
            return Bitmap;
        }

        public static Bitmap ShowChanged(Bitmap Bitmap)
        {
            Bitmap Temp = new(Bitmap.Width, Bitmap.Height, Bitmap.Depth);

            for (int I = 0; I < Bitmap.rawData.Length; I++)
            {
                if (Bitmap.rawData[I] != LastFrame.rawData[I])
                {
                    Color C1 = Color.FromArgb(Bitmap.rawData[I]);
                    Color C2 = Color.FromArgb(LastFrame.rawData[I]);

                    int RDiff = Math.Abs(C1.R - C2.R);
                    int GDiff = Math.Abs(C1.G - C2.G);
                    int BDiff = Math.Abs(C1.B - C2.B);

                    Temp.rawData[I] = Color.FromArgb(RDiff, GDiff, BDiff).ToArgb();
                }
                else
                {
                    Temp.rawData[I] = Color.Black.ToArgb();
                }
            }

            LastFrame = Bitmap;
            return Temp;
        }

        public static Bitmap Ghost(Bitmap Original, Bitmap New)
        {
            if (Original.rawData.Length != New.rawData.Length)
                return null;

            for (int I = 0; I < Original.rawData.Length; I++)
            {
                Color C1 = Color.FromArgb(Original.rawData[I]);
                Color C2 = Color.FromArgb(New.rawData[I]);

                int R = (C1.R / 2) + (C2.R / 2);
                int G = (C1.G / 2) + (C2.G / 2);
                int B = (C1.B / 2) + (C2.B / 2);

                Original.rawData[I] = Color.FromArgb(R, G, B).ToArgb();
            }
            return Original;
        }

        public static Bitmap Tint(Bitmap Bitmap, Color Color)
        {
            for (int I = 0; I < Bitmap.rawData.Length; I++)
            {
                Color C = Color.FromArgb(Bitmap.rawData[I]);
                byte R = (byte)((C.R / 2) - Color.R + Color.R);
                byte G = (byte)((C.G / 2) - Color.G + Color.G);
                byte B = (byte)((C.B / 2) - Color.B + Color.B);
                Bitmap.rawData[I] = Color.FromArgb(R, G, B).ToArgb();
            }
            return Bitmap;
        }

        public static Bitmap GetNoise(int Width, int Height)
        {
            Bitmap Temp = new((uint)Width, (uint)Height, ColorDepth.ColorDepth32);
            for (int I = 0; I < Width * Height; I++)
            {
                Temp.rawData[I] = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255)).ToArgb();
            }
            return Temp;
        }

        public static Bitmap GetSine(int Width, int Height, Color Color, double Freq = 40.0, double Amp = 0.25)
        {
            Bitmap Temp = new((uint)Width, (uint)Height, ColorDepth.ColorDepth32);
            Amp *= Height;
            for (int X = 0; X < Width; X++)
            {
                Temp.rawData[(Width * ((Height / 5) + ((short)(Amp * Math.Sin(2 * Math.PI * X * Freq / 8000)) / 2))) + X] = Color.ToArgb();
            }
            return Temp;
        }

        public static Bitmap Resize(Bitmap Bitmap, int Width, int Height)
        {
            Bitmap Temp = new((uint)Width, (uint)Height, ColorDepth.ColorDepth32);
            int x_ratio = (int)((Bitmap.Width << 16) / Width) + 1;
            int y_ratio = (int)((Bitmap.Height << 16) / Height) + 1;
            int x2, y2;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    x2 = (j * x_ratio) >> 16;
                    y2 = (i * y_ratio) >> 16;
                    Temp.rawData[(i * Width) + j] = Bitmap.rawData[(y2 * Bitmap.Width) + x2];
                }
            }
            return Temp;
        }
    }
}