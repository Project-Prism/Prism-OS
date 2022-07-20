namespace PrismOS.Libraries.Numerics
{
    public unsafe static class Math2
    {
        // [0] = X, [1] = Y
        public static (int, int) LinearInterpolate(int X1, int Y1, int X2, int Y2, double T)
        {
            return ((int, int))((1-T) * X1 + T * Y1, (1-T) * X2 + T * Y2);
        }
        public static (int, int) LinearInterpolate((int, int) P0, (int, int) P1, double T)
        {
            return ((int, int))((1 - T) * P0.Item1 + T * P0.Item2, (1 - T) * P1.Item1 + T * P1.Item2);
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
    }
}