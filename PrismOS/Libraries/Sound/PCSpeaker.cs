using Cosmos.HAL.Drivers.PCI.Audio;
using System;

namespace PrismOS.Libraries.Sound
{
    public static class PCSpeaker
    {
        public static void Tick(double Amp = 0.25, double Freq = 1000, int Samples = 20)
        {
            Amp *= short.MaxValue;
            for (int n = 0; n < Samples; n++)
            {
                Cosmos.System.PCSpeaker.Beep((uint)(Amp * Math.Sin(2 * Math.PI * n * Freq / Samples)));
            }
        }

        public static void Play(PCMStream PCMStream)
        {
            for (int I = 0; I < PCMStream.getData().Length; I++)
            {
                Cosmos.System.PCSpeaker.Beep(PCMStream.getData()[I], (uint)Cosmos.System.Durations.Sixteenth);
            }
        }
    }
}