using System;

namespace PrismOS.Tests
{
    public static class Sound
    {
        public static void Tick(double Amp = 0.25, double Freq = 1000, int Samples = 20)
        {
            Amp *= short.MaxValue;
            for (int n = 0; n < Samples; n++)
            {
                Cosmos.System.PCSpeaker.Beep((uint)(Amp * Math.Sin(2 * Math.PI * n * Freq / Samples)));
            }
        }
    }
}