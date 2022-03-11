namespace PrismOS.Core
{
    public unsafe static class Math
    {
        public const double PI = 3.14159265;

        public static float InverseSqrt(float Number)
        {
            long I = *&I;
            float X2 = Number * 0.5f, Y = Number;
            I = 0x5f3759df - (I >> 1);
            Y = *&I;
            Y *= 1.5f - (X2 * Y * Y);
            return Y;
        }

        public static int Abs(int Number)
        {
            return +Number;
        }
        public static double Abs(double Number)
        {
            return +Number;
        }
        public static float Abs(float Number)
        {
            return +Number;
        }

        public static int Floor(double Number)
        {
            return (int)Number;
        }
        public static int Floor(decimal Number)
        {
            return (int)Number;
        }
        public static float Floor(float Number)
        {
            return (int)Number;
        }

        // Sin and Cos placeholder
        public static double Cos(double Number)
        {
            return System.Math.Cos(Number);
        }
        public static double Sin(double Number)
        {
            return System.Math.Sin(Number);
        }
        public static double Tan(double Number)
        {
            return System.Math.Tan(Number);
        }
    }
}