using Color = System.Drawing.Color;
using VBE = Cosmos.HAL.Drivers.VBEDriver;

namespace PrismOS.UI
{
    public class SaltCanvas
    {
        private VBE Driver { get; }
        private int[] Buffer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SaltCanvas(int Width, int Height)
        {
            Resize(Width, Height);
        }

        public void Resize(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new int[Width * Height * (32 / 8)];
            Driver.VBESet((ushort)Width, (ushort)Height, 32);
        }

        public void Update()
        {
            Driver.CopyVRAM(0, Buffer, 0, Buffer.Length);
            Driver.Swap();
        }

        public void SetPixel(int X, int Y, Color Color)
        {
            Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8))] = Color.B;
            Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 1] = Color.G;
            Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 2] = Color.R;
            Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 3] = Color.A;
        }

        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(
                Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8))],
                Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 1],
                Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 2],
                Buffer[(X * (32 / 8)) + (Y * Width * (32 / 8)) + 3]);
        }

        public void Clear(Color Color)
        {
            for (int X = 0; X < Width; X++)
            {
                for (int Y = 0; Y < Height; Y++)
                {
                    SetPixel(X, Y, Color);
                }
            }
        }
    }
}