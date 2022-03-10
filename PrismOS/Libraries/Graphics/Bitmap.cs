using Cosmos.System.Graphics;
using System.Drawing;
using System;

namespace PrismOS.Libraries.Graphics
{
    public class Bitmap : Cosmos.System.Graphics.Bitmap
    {
        public Bitmap(string path) : base(path)
        {
        }
        public Bitmap(byte[] imageData) : base(imageData)
        {
        }
        public Bitmap(string path, ColorOrder colorOrder = ColorOrder.BGR) : base(path, colorOrder)
        {
        }
        public Bitmap(byte[] imageData, ColorOrder colorOrder = ColorOrder.BGR) : base(imageData, colorOrder)
        {
        }
        public Bitmap(uint Width, uint Height, ColorDepth colorDepth) : base(Width, Height, colorDepth)
        {
        }
        public Bitmap(uint Width, uint Height, byte[] pixelData, ColorDepth colorDepth) : base(Width, Height, pixelData, colorDepth)
        {
        }

        private Bitmap LastFrame;

        public void AddGrayScale()
        {
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C = Color.FromArgb(rawData[I]);
                rawData[I] = Color.FromArgb(C.R / 3, C.G / 3, C.B / 3).ToArgb();
            }
        }
        public void AddThreshHold(byte MinValue, byte MaxValue)
        {
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C = Color.FromArgb(rawData[I]);
                int B = (C.R / 3) + (C.G / 3) + (C.B / 3);

                if (B < MinValue || B > MaxValue)
                {
                    rawData[I] = Color.Black.ToArgb();
                }
            }
        }
        public void ShowChanged()
        {
            LastFrame = this;
            for (int I = 0; I < rawData.Length; I++)
            {
                if (rawData[I] != LastFrame.rawData[I])
                {
                    Color C1 = Color.FromArgb(rawData[I]);
                    Color C2 = Color.FromArgb(LastFrame.rawData[I]);

                    int RDiff = Math.Abs(C1.R - C2.R);
                    int GDiff = Math.Abs(C1.G - C2.G);
                    int BDiff = Math.Abs(C1.B - C2.B);

                    rawData[I] = Color.FromArgb(RDiff, GDiff, BDiff).ToArgb();
                }
                else
                {
                    rawData[I] = Color.Black.ToArgb();
                }
            }
        }
        public void Ghost()
        {
            LastFrame = this;
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C1 = Color.FromArgb(LastFrame.rawData[I]);
                Color C2 = Color.FromArgb(rawData[I]);

                int R = (C1.R / 2) + (C2.R / 2);
                int G = (C1.G / 2) + (C2.G / 2);
                int B = (C1.B / 2) + (C2.B / 2);

                rawData[I] = Color.FromArgb(R, G, B).ToArgb();
            }
        }
        public void Tint(Color Color)
        {
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C = Color.FromArgb(rawData[I]);
                byte R = (byte)((C.R / 2) - Color.R + Color.R);
                byte G = (byte)((C.G / 2) - Color.G + Color.G);
                byte B = (byte)((C.B / 2) - Color.B + Color.B);
                rawData[I] = Color.FromArgb(R, G, B).ToArgb();
            }
        }
        public void GetNoise()
        {
            Random Random = new();
            for (int I = 0; I < rawData.Length; I++)
            {
                rawData[I] = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255)).ToArgb();
            }
            LastFrame = this;
        }
        public void GetSine(Color Color, bool Clear, double Freq = 40.0, double Amp = 0.25)
        {
            if (Clear)
                Array.Fill(rawData, Color.Black.ToArgb());
            Amp *= Height;
            for (int X = 0; X < Width; X++)
            {
                rawData[(Width * ((Height / 5) + ((short)(Amp * Math.Sin(2 * Math.PI * X * Freq / 8000)) / 2))) + X] = Color.ToArgb();
            }
        }
        public void Resize()
        {
            Bitmap Temp = new(Width, Height, ColorDepth.ColorDepth32);
            int x_ratio = (int)((Width << 16) / Width) + 1;
            int y_ratio = (int)((Height << 16) / Height) + 1;
            int x2, y2;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    x2 = (j * x_ratio) >> 16;
                    y2 = (i * y_ratio) >> 16;
                    Temp.rawData[(i * Width) + j] = rawData[(y2 * Width) + x2];
                }
            }
            rawData = Temp.rawData;
            Width = Temp.Width;
            Height = Temp.Height;
        }
    }
}