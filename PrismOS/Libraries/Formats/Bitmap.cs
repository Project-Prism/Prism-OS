using Cosmos.System.Graphics;
using PrismOS.Libraries.Graphics;
using System;

namespace PrismOS.Libraries.Formats
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
                Color C = new(rawData[I]);
                rawData[I] = new Color((byte)(C.R / 3), (byte)(C.G / 3), (byte)(C.B / 3)).ARGB;
            }
        }
        public void AddThreshHold(byte MinValue, byte MaxValue)
        {
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C = new(rawData[I]);
                int B = (C.R / 3) + (C.G / 3) + (C.B / 3);

                if (B < MinValue || B > MaxValue)
                {
                    rawData[I] = Color.Black.ARGB;
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
                    Color C1 = new(rawData[I]);
                    Color C2 = new(LastFrame.rawData[I]);

                    byte RDiff = (byte)Math.Abs(C1.R - C2.R);
                    byte GDiff = (byte)Math.Abs(C1.G - C2.G);
                    byte BDiff = (byte)Math.Abs(C1.B - C2.B);

                    rawData[I] = new Color(RDiff, GDiff, BDiff).ARGB;
                }
                else
                {
                    rawData[I] = Color.Black.ARGB;
                }
            }
        }
        public void Ghost()
        {
            LastFrame = this;
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C1 = new(LastFrame.rawData[I]);
                Color C2 = new(rawData[I]);

                byte R = (byte)((C1.R / 2) + (C2.R / 2));
                byte G = (byte)((C1.G / 2) + (C2.G / 2));
                byte B = (byte)((C1.B / 2) + (C2.B / 2));

                rawData[I] = new Color(R, G, B).ARGB;
            }
        }
        public void Tint(Color Color)
        {
            for (int I = 0; I < rawData.Length; I++)
            {
                Color C = new(rawData[I]);
                byte R = (byte)((C.R / 2) - Color.R + Color.R);
                byte G = (byte)((C.G / 2) - Color.G + Color.G);
                byte B = (byte)((C.B / 2) - Color.B + Color.B);
                rawData[I] = new Color(R, G, B).ARGB;
            }
        }
        public void GetNoise()
        {
            Random Random = new();
            for (int I = 0; I < rawData.Length; I++)
            {
                rawData[I] = new Color((byte)Random.Next(0, 255), (byte)Random.Next(0, 255), (byte)Random.Next(0, 255)).ARGB;
            }
            LastFrame = this;
            
        }
        public void GetSine(Color Color, bool Clear, double Freq = 40.0, double Amp = 0.25)
        {
            if (Clear)
                Array.Fill(rawData, Color.Black.ARGB);
            Amp *= Height;
            for (int X = 0; X < Width; X++)
            {
                rawData[(Width * ((Height / 5) + ((short)(Amp * Math.Sin(2 * Math.PI * X * Freq / 8000)) / 2))) + X] = Color.ARGB;
            }
        }
        public void Resize(int Width, int Height)
        {
            Bitmap Temp = new((uint)Width, (uint)Height, (ColorDepth)32);
            int x_ratio = (((int)this.Width << 16) / Width) + 1;
            int y_ratio = (((int)this.Height << 16) / Height) + 1;
            int x2, y2;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    x2 = (j * x_ratio) >> 16;
                    y2 = (i * y_ratio) >> 16;
                    Temp.rawData[(i * Width) + j] = rawData[(y2 * this.Width) + x2];
                }
            }
            this.Width = Temp.Width;
            this.Height = Temp.Height;
            rawData = Temp.rawData;
        }
        public Color GetAverageColor()
        {
            Color Temp = new(255, 0, 0, 0);
            for (int I = 0; I < rawData.Length; I++)
            {
                Color Temp2 = new(rawData[I]);
                Temp.R += (byte)(Temp2.R / rawData.Length);
                Temp.G += (byte)(Temp2.G / rawData.Length);
                Temp.B += (byte)(Temp2.B / rawData.Length);
            }
            return Temp;
        }
    }
}