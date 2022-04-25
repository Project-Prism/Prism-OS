using PrismOS.Libraries.Graphics;
using System;
using System.IO;

namespace PrismOS.Libraries.Formats
{
    public unsafe class Image
    {
        public Image(int Width, int Height)
        {
            System.Console.WriteLine($"[ DEBUG ] Creating new image with width {Width} and height {Height}");
            this.Width = Width;
            this.Height = Height;
            Buffer = new int*[Width * Height];
        }
        public Image(byte[] Binary)
        {
            BinaryReader Reader = new(new MemoryStream(Binary));
            if (Binary == null || Binary.Length == 0)
            {
                throw new FileLoadException("Binary data cannot be null or blank.");
            }
            if (Reader.ReadChars(2) == new char[] {'B', 'M' }) // Bitmap file detected
            {
                // Using cosmos to get bitmaps for now
                Cosmos.System.Graphics.Bitmap BMP = new(Binary);
                Width = (int)BMP.Width;
                Height = (int)BMP.Height;
                Buffer = (int*[])(object)BMP.rawData;
                return;
            }
            Reader.BaseStream.Position = 0;
            if(Reader.ReadString() == "RAW")
            {
                Width = Reader.ReadInt32();
                Height = Reader.ReadInt32();
                Buffer = (int*[])(object)Reader.ReadBytes(Width * Height * 4);
                return;
            }
        }

        public Color AverageColor
        {
            get
            {
                Color T = new(255, 0, 0, 0);
                for (int I = 0; I < Buffer.Length; I++)
                {
                    Color T2 = new((int)Buffer[I]);
                    T.R += (byte)(T2.R / Buffer.Length);
                    T.G += (byte)(T2.G / Buffer.Length);
                    T.B += (byte)(T2.B / Buffer.Length);
                }
                return T;
            }
        }
        public int Width, Height;
        public int*[] Buffer;

        public byte[] ToBinary()
        {
            BinaryWriter Writer = new(new MemoryStream());
            Writer.Write("RAW");
            Writer.Write(Width);
            Writer.Write(Height);
            Writer.Write((byte[])(object)Buffer);
            return (Writer.BaseStream as MemoryStream).ToArray();
        }

        #region Effects

        public Image Resize(int Width, int Height)
        {
            if (Width == this.Width && Height == this.Height)
            {
                return this;
            }
            Image Temp = new(Width, Height);
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
        public Image Rotate(int Angle)
        {
            Image Temp = new(Width, Height);

            double radians = (Angle * Math.PI) / 180;
            int sinf = (int)Math.Sin(radians);
            int cosf = (int)Math.Cos(radians);

            double X0 = 0.5 * (Width - 1);
            double Y0 = 0.5 * (Height - 1);

            // rotation
            for (int x = 0; x < Temp.Width; x++)
            {
                for (int y = 0; y < Temp.Height; y++)
                {
                    double a = x - X0;
                    double b = y - Y0;
                    int xx = (int)(+a * cosf - b * sinf + X0);
                    int yy = (int)(+a * sinf + b * cosf + Y0);

                    if (xx >= 0 && xx < Temp.Width && yy >= 0 && yy < Temp.Height)
                    {
                        Temp.Buffer[(y * Temp.Height + x) * 3 + 0] = Temp.Buffer[(yy * Temp.Height + xx) * 3 + 0];
                        Temp.Buffer[(y * Temp.Height + x) * 3 + 1] = Temp.Buffer[(yy * Temp.Height + xx) * 3 + 1];
                        Temp.Buffer[(y * Temp.Height + x) * 3 + 2] = Temp.Buffer[(yy * Temp.Height + xx) * 3 + 2];
                    }
                }
            }
            return Temp;
        }
        public Image Grayscale()
        {
            Image Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((int)Buffer[I]);
                Temp.Buffer[I] = (int*)new Color(C.R, C.R, C.R).ARGB;
            }
            return Temp;
        }
        public Image Threshhold(byte MinValue, byte MaxValue)
        {
            Image Temp = new(Width, Height);
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
        public Image Tint(Color Color)
        {
            Image Temp = new(Width, Height);
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
        public Image ShowChanged(Image Comparison)
        {
            Image Temp = new(Width, Height);
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
        public Image Ghost(Image Comparison)
        {
            Image Temp = new(Width, Height);
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
        public Image GetNoise()
        {
            Image Temp = new(Width, Height);
            Random Random = new();
            for (int I = 0; I < Buffer.Length; I++)
            {
                Temp.Buffer[I] = (int*)new Color((byte)Random.Next(0, 255), (byte)Random.Next(0, 255), (byte)Random.Next(0, 255)).ARGB;
            }
            return Temp;
        }
        public Image GetSine(Color Color, double Freq = 40.0, double Amp = 0.25)
        {
            Image Temp = new(Width, Height);
            Amp *= Height;
            for (int X = 0; X < Width; X++)
            {
                Temp.Buffer[(Width * ((Height / 5) + ((short)(Amp * Math.Sin(2 * Math.PI * X * Freq / 8000)) / 2))) + X] = (int*)Color.ARGB;
            }
            return Temp;
        }

        #endregion
    }
}