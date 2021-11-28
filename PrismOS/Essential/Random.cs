namespace PrismOS.Essential
{
    public static class Random
    {
        private readonly static System.Random Rand = new();

        public static int GetRandomNumber(int From, int To) => Rand.Next(From, To);
    }
}
