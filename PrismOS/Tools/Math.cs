namespace PrismOS.Tools
{
    public static class Math
    {
        /// <summary>
        /// Get the value of a percent of a number.
        /// </summary>
        /// <param name="Percent"></param>
        /// <param name="Total"></param>
        public static int PercentOf(int Percent, int Total)
        {
            return (Percent * Total) / 100;
        }

        /// <summary>
        /// Check if a number is prime.
        /// </summary>
        /// <returns>True if the number is prime.</returns>
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

        /// <summary>
        /// Check if a number is composite.
        /// </summary>
        /// <param name="N"></param>
        /// <returns>True or False.</returns>
        public static bool IsComposite(int N)
        {
            return !IsPrime(N);
        }

        /// <summary>
        /// Get the greatest common divisor of two numbers.
        /// </summary>
        /// <param name="NumberOne"></param>
        /// <param name="NumberTwo"></param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
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

        /// <summary>
        /// Get the lowest terms of a fraction.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        /// <returns>The lowest terms of a fraction.</returns>
        public static int[] ToLowestTerms(int N1, int N2)
        {
            int Gdc = GCD(N1, N2);
            return new int[] { N1 / Gdc, N2 / Gdc };
        }

        /// <summary>
        /// Find the difference between two numbers.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        /// <returns>The difference between N1 and N2</returns>
        public static int Difference(int N1, int N2)
        {
            return System.Math.Abs(N1 - N2);
        }
    }
}
