using PrismOS.Libraries.Graphics;
using System;
using System.IO;

namespace PrismOS.Libraries.Formats
{
    public unsafe class BMP
    {
        public BMP(int Width, int Height, Cosmos.System.Graphics.Bitmap Bitmap)
        {
            int*[] NewBuffer = new int*[Width * Height];
            for (int IX = 0; IX < Bitmap.Width; IX++)
            {
                for (int IY = 0; IY < Bitmap.Height; IY++)
                {
                    NewBuffer[(Width * (IY / (Height / this.Height))) + IX / (Width / this.Width)] = (int*)Buffer[(this.Width * IY) + IX];
                }
            }

            this.Width = Width;
            this.Height = Height;
            Buffer = NewBuffer;
        }
        public BMP(Cosmos.System.Graphics.Bitmap Bitmap)
        {
            Width = (int)Bitmap.Width;
            Height = (int)Bitmap.Height;
            Buffer = (int*[])(object)Bitmap.rawData;
        }
        public BMP(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new int*[Width * Height];
        }
        public BMP(byte[] Binary)
        {
            BinaryReader Reader = new(new MemoryStream(Binary));
            Width = Reader.ReadInt32();
            Height = Reader.ReadInt32();
            Binary.CopyTo(Buffer, 8);
        }

        public readonly int*[] Buffer;
        public readonly int Width, Height;

        public byte[] ToBinary()
        {
            BinaryWriter Writer = new(new MemoryStream());
            BinaryWriter Writer2 = new(new MemoryStream());
            Writer.Write(Width);
            Writer.Write(Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Writer2.Write((int)Buffer[I]);
            }
            return (Writer.BaseStream as MemoryStream).ToArray();
        }

        public BMP Grayscale()
        {
            BMP Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((int)Buffer[I]);
                Temp.Buffer[I] = (int*)new Color((byte)(C.R / 3), (byte)(C.G / 3), (byte)(C.B / 3)).ARGB;
            }
            return Temp;
        }
        public BMP Threshhold(byte MinValue, byte MaxValue)
        {
            BMP Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((int)Buffer[I]);
                int T = (C.R / 3) + (C.G / 3) + (C.B / 3);

                if (T < MinValue || T > MaxValue)
                {
                    Temp.Buffer[I] = (int*)Color.Black.ARGB;
                }
            }
            return Temp;
        }
        public BMP Resize(int Width, int Height)
        {
            BMP Temp = new(Width, Height);
            for (int IX = 0; IX < this.Width; IX++)
            {
                for (int IY = 0; IY < this.Height; IY++)
                {
                    int X = IX / (this.Width / Width);
                    int Y = IY / (this.Height / Height);
                    Temp.Buffer[(Width * Y) + X] = Buffer[(this.Width * IY) + IX];
                }
            }
            return Temp;
        }
        public BMP Tint(Color Color)
        {
            BMP Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((int)Temp.Buffer[I]);
                byte R = (byte)((C.R / 2) - Color.R + Color.R);
                byte G = (byte)((C.G / 2) - Color.G + Color.G);
                byte B = (byte)((C.B / 2) - Color.B + Color.B);
                Temp.Buffer[I] = (int*)new Color(R, G, B).ARGB;
            }
            return Temp;
        }
        public BMP ShowChanged(BMP Comparison)
        {
            BMP Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                if (Buffer[I] != Comparison.Buffer[I])
                {
                    Color C1 = new((int)Buffer[I]);
                    Color C2 = new((int)Comparison.Buffer[I]);

                    byte RDiff = (byte)Math.Abs(C1.R - C2.R);
                    byte GDiff = (byte)Math.Abs(C1.G - C2.G);
                    byte BDiff = (byte)Math.Abs(C1.B - C2.B);

                    Temp.Buffer[I] = (int*)new Color(RDiff, GDiff, BDiff).ARGB;
                }
                else
                {
                    Temp.Buffer[I] = (int*)Color.Black.ARGB;
                }
            }
            return Temp;
        }
        public BMP Ghost(BMP Comparison)
        {
            BMP Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C1 = new((int)Comparison.Buffer[I]);
                Color C2 = new((int)Buffer[I]);

                byte R = (byte)((C1.R / 2) + (C2.R / 2));
                byte G = (byte)((C1.G / 2) + (C2.G / 2));
                byte B = (byte)((C1.B / 2) + (C2.B / 2));

                Temp.Buffer[I] = (int*)new Color(R, G, B).ARGB;
            }
            return Temp;
        }
        public BMP GetNoise()
        {
            BMP Temp = new(Width, Height);
            Random Random = new();
            for (int I = 0; I < Buffer.Length; I++)
            {
                Temp.Buffer[I] = (int*)new Color((byte)Random.Next(0, 255), (byte)Random.Next(0, 255), (byte)Random.Next(0, 255)).ARGB;
            }
            return Temp;
        }
        public BMP GetSine(Color Color, double Freq = 40.0, double Amp = 0.25)
        {
            BMP Temp = new(Width, Height);
            Amp *= Height;
            for (int X = 0; X < Width; X++)
            {
                Temp.Buffer[(Width * ((Height / 5) + ((short)(Amp * Math.Sin(2 * Math.PI * X * Freq / 8000)) / 2))) + X] = (int*)Color.ARGB;
            }
            return Temp;
        }
        public Color GetAverageColor()
        {
            Color Temp = new(255, 0, 0, 0);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color Temp2 = new((int)Buffer[I]);
                Temp.R += (byte)(Temp2.R / Buffer.Length);
                Temp.G += (byte)(Temp2.G / Buffer.Length);
                Temp.B += (byte)(Temp2.B / Buffer.Length);
            }
            return Temp;
        }
    }
}