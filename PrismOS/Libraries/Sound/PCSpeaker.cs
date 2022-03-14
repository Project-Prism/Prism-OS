using System;

namespace PrismOS.Libraries.Sound
{
    public unsafe static class PCSpeaker
    {
        public static void Play(byte[] RawBytes)
        {
            short[] Audio = new short[RawBytes.Length / 2];
            Buffer.BlockCopy(RawBytes, 44, Audio, 0, RawBytes.Length);

            for (int I = 0; I < Audio.Length; I++)
            {
                if (Audio[I] > 37 && Audio[I] < short.MaxValue)
                {
                    Cosmos.HAL.PCSpeaker.Beep((uint)Audio[I], 1);
                }
            }
        }
    }
}