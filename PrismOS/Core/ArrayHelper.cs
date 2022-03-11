namespace PrismOS.Core
{
    public static class ArrayHelper
    {
        public static int LongestString(string[] Array)
        {
            int Longest = 0;

            for (int I = 0; I < Array.Length; I++)
            {
                if (Array[I].Length > Longest)
                    Longest = Array[I].Length;
            }
            return Longest;
        }
    }
}