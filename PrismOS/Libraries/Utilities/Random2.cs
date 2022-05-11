using System;

namespace PrismOS.Libraries.Utilities
{
    public static class Random2
    {
        public static int Next(int Min, int Max, int Mixing)
        {
            int Value = 0;
            Random R = new();
            for (int I = 0; I < Mixing; I++)
            {
                Value += R.Next(Min, Max) / Mixing;
            }
            return Value;
        }
    }
}