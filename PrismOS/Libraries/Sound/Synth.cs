using System;

namespace PrismOS.Libraries.Sound
{
    public static class Synth
    {
        public static short[] GenerateSineWave(int SampleRate, double Amp, double Freq, int BufferSize)
        {
            Amp *= short.MaxValue;
            short[] Buffer = new short[BufferSize];
            for (int I = 0; I < Buffer.Length; I++)
            {
                Buffer[I] = (short)(Amp * Math.Sin(2 * Math.PI * I * Freq / SampleRate));
            }
            return Buffer;
        }
    }
}