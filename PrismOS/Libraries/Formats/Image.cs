using PrismOS.Libraries.Graphics;
using System;
using System.IO;

namespace PrismOS.Libraries.Formats
{
    public unsafe class Image
    {
        public Image(int Width, int Height)
        {
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
            if (Binary[0] == 'B' && Binary[1] == 'M') // Bitmap file detected
            {
                // Using cosmos to get bitmaps for now
                Cosmos.System.Graphics.Bitmap BMP = new(Binary);
                Width = (int)BMP.Width;
                Height = (int)BMP.Height;
                Buffer = (int*[])(object)BMP.rawData;
                return;
            }
            Reader.Dispose();
        }

        public Color AverageColor
        {
            get
            {
                Color T = new(255, 0, 0, 0);
                for (int I = 0; I < Buffer.Length; I++)
                {
                    Color T2 = new((uint)Buffer[I]);
                    T.R += (byte)(T2.R / Buffer.Length);
                    T.G += (byte)(T2.G / Buffer.Length);
                    T.B += (byte)(T2.B / Buffer.Length);
                }
                return T;
            }
        }
        public int Width, Height;
        public int*[] Buffer;

        public void SetPixel(int X, int Y, Color Color)
        {
            Buffer[X + Y * Width] = (int*)Color.AlphaBlend(Color, new((uint)Buffer[X + Y * Width])).ARGB;
        }
        public Color GetPixel(int X, int Y)
        {
            return new((uint)Buffer[X + Y * Width]);
        }

        #region Effects

        public Image Resize(int Width, int Height)
        {
            if (this == null)
            {
                throw new Exception("Cannot draw a null image file.");
            }
            Image Temp = new(Width, Height);
            for (int IX = 0; IX < this.Width; IX++)
            {
                for (int IY = 0; IY < this.Height; IY++)
                {
                    int X = IX / (this.Width / Width);
                    int Y = IY / (this.Height / Height);
                    Temp.Buffer[(Temp.Width * Y) + X] = Buffer[(this.Width * IY) + IX];
                }
            }
            return Temp;
        }
        // Generated using github's copilot, untested
        public Image RotateImage(Image Image, int Degrees)
        {
            if (Degrees > 360)
            {
                return this;
            }
            int Width = Image.Width;
            int Height = Image.Height;
            int NewWidth = Width;
            int NewHeight = Height;
            if (Degrees == 90 || Degrees == 270)
            {
                NewWidth = Height;
                NewHeight = Width;
            }
            Image NewImage = new(NewWidth, NewHeight);
            for (int i = 0; i < NewWidth; i++)
            {
                for (int j = 0; j < NewHeight; j++)
                {
                    NewImage.SetPixel(i, j, Image.GetPixel(Degrees == 90 ? Width - j - 1 : Degrees == 270 ? Width - i - 1 : i, Degrees == 90 ? j : Degrees == 270 ? Height - i - 1 : j));
                }
            }
            return NewImage;
        }
        public Image Grayscale()
        {
            Image Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((uint)Buffer[I]);
                Temp.Buffer[I] = (int*)new Color(C.R, C.R, C.R).ARGB;
            }
            return Temp;
        }
        public Image Threshhold(byte MinValue, byte MaxValue)
        {
            Image Temp = new(Width, Height);
            for (int I = 0; I < Buffer.Length; I++)
            {
                Color C = new((uint)Buffer[I]);
                int T = (C.R / 3) + (C.G / 3) + (C.B / 3);

                if (T < MinValue || T > MaxValue)
                {
                    Temp.Buffer[I] = (int*)Color.Black.ARGB;
                }
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
                    Color C1 = new((uint)Buffer[I]);
                    Color C2 = new((uint)Comparison.Buffer[I]);

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
                Color C1 = new((uint)Comparison.Buffer[I]);
                Color C2 = new((uint)Buffer[I]);

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