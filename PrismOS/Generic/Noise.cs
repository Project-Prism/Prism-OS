using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismOS.Generic
{
    public static class Noise
    {
        private static readonly Random Random = new();

        public static Bitmap GenWhiteNoiseImage(int Width, int Height)
        {
            byte[] Buffer = new byte[Width * Height];
            for (int I = 0; I < Buffer.Length; I++)
            {
                int X = Random.Next(0, 255);
                Buffer[I] = (byte)Color.FromArgb(X, X, X).ToArgb();
            }

            return new Bitmap((uint)Width, (uint)Height, Buffer, (ColorDepth)32);
        }
    }
}
