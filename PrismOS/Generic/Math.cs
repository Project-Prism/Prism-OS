namespace PrismOS.Generic
{
    public static class Math
    {
        public static int PercentIncrease(int Initial, int Final)
        {
            return 100 * ((Final - Initial) / Initial);
        }

        public static int PercentOf(int Percent, int Total)
        {
            return Percent * Total / 100;
        }

        public static bool IsPrime(int N)
        {
            if (N <= 1) return false;
            if (N == 2) return true;
            if (N % 2 == 0) return false;

            var boundary = (int)System.Math.Floor(System.Math.Sqrt(N));

            for (int i = 3; i <= boundary; i += 2)
                if (N % i == 0) { return false; }

            return true;
        }

        public static bool IsComposite(int N)
        {
            return !IsPrime(N); // Return the oposite of IsPrime()
        }

        public static int GCD(int N1, int N2)
        {
            while (N1 != 0 && N2 != 0)
            {
                if (N1 > N2)
                    N1 %= N2;
                else
                    N2 %= N1;
            }

            return N1 | N2;
        }

        public static int[] ToLowestTerms(int N1, int N2)
        {
            int Gdc = GCD(N1, N2);
            return new int[] { N1 / Gdc, N2 / Gdc };
        }

        public static int Difference(int N1, int N2)
        {
            return System.Math.Abs(N1 - N2);
        }
    }
}