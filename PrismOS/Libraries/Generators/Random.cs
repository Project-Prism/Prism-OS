using System;
using PrismOS.Libraries.Formats;

namespace PrismOS.Libraries.Generators
{
    public unsafe static class Random
    {
        public static Image Generate(int Width, int Height)
        {
            Image TMP = new(Width, Height);
            System.Random R = new();
            uint* PTR = (uint*)R.Next(0, 4096000);
            for (int I = 0; I < Width * Height; I++)
            {
                TMP.Buffer[I] = (int*)PTR[I];
            }
            return TMP;
        }
    }
}