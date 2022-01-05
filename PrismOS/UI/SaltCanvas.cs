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
            Buffer = new int[Width * Height];
            Driver.VBESet((ushort)Width, (ushort)Height, 32);
        }

        public void Update()
        {
            Driver.CopyVRAM(0, Buffer, 0, Buffer.Length);
            Driver.Swap();
        }

        public void SetPixel(int X, int Y, Color Color)
        {
            Buffer[(Width * Y) + X] = Color.ToArgb();
        }

        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(Buffer[(Width * Y) + X]);
        }
    }
}