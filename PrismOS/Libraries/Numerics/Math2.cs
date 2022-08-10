using System.Collections.Generic;

namespace PrismOS.Libraries.Numerics
{
    public unsafe static class Math2
    {
        // https://www.gabrielgambetta.com/computer-graphics-from-scratch/06-lines.html
        public static int[] Lerp(int i0, int d0, int i1, int d1)
        {
            List<int> Values = new();
            int A = (d1 - d0) / (i1 - i0);
            int D = d0;
            for (int I = i0; I < i1; I++)
            {
                Values.Add(D);
                D += A;
            }
            return Values.ToArray();
        }

        public static float InverseSqrt(float Number)
        {
            long I = *&I;
            float X2 = Number * 0.5f, Y = Number;
            I = 0x5f3759df - (I >> 1);
            Y = *&I;
            Y *= 1.5f - (X2 * Y * Y);
            return Y;
        }

        public static double Degrees(double Radians)
        {
            return Radians * 57.2957795;
        }
    }
}