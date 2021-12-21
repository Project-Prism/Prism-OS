using Cosmos.Core;
using Cosmos.HAL;

namespace PrismOS.Testing
{
    public static class VideoDriver
    {
        public static int GetOffset(int X, int Y, int Depth, Driver Driver)
        {
            int BPP = Depth / 8;
            int Pitch = Driver.Width * BPP;

            return (X * BPP) + (Y * Pitch);
        }

        public abstract class Driver
        {
            public int Width { get; internal set; }
            public int Height { get; internal set; }
            public int Depth { get; internal set; }
            public PCIDevice Device { get; set; }
            public int[] Buffer { get; set; }

            public abstract void SetBuffer(int[] aBuffer);

            public abstract void Update();

            public abstract void SetPixel(int X, int Y, int Color);

            public abstract int GetPixel(int X, int Y);

            public abstract void Fill(int X, int Y, int Width, int Height, int Color);
        }

        public class VbeDriver : Driver
        {
            public VbeDriver(int aWidth, int aHeight, int aDepth)
            {
                Width = aWidth;
                Height = aHeight;
                Depth = aDepth;
                Buffer = new int[aWidth * aHeight * (aDepth / 8)];
            }

            public new int Width { get; internal set; }
            public new int Height { get; internal set; }
            public new int Depth { get; internal set; }
            public new PCIDevice Device { get; set; }
            public new int[] Buffer { get; set; }

            public override void SetBuffer(int[] aBuffer)
            {
                Buffer = aBuffer;
            }

            public override void Update()
            {
            }

            public override void SetPixel(int X, int Y, int Color)
            {
                Buffer[GetOffset(X, Y, Depth, this)] = Color;
            }

            public override int GetPixel(int X, int Y)
            {
                return Buffer[GetOffset(X, Y, Depth, this)];
            }

            public override void Fill(int X, int Y, int Width, int Height, int Color)
            {
                for (int aX = X; aX < Width; aX++)
                {
                    for (int aY = Y; aY < Width; aY++)
                    {
                        SetPixel(aX, aY, Color);
                    }
                }
            }

            public override string ToString()
            {
                return "VBE-Driver";
            }
        }
    }
}
