using System;

namespace PrismOS.Libraries.Sound
{
    public unsafe static class PCSpeaker
    {
        public static void PlayMono(byte[] Audio)
        {
            Console.Write("Prepairing audio... ");
            short[] SAudio = new short[Audio.Length / 2];
            for (int I = 0; I < Audio.Length; I += 2)
            {
                SAudio[I / 2] = (short)(Audio[I] | Audio[I + 1] << 8);
            }

            Console.WriteLine("Done.");
            for (int I = 0; I < SAudio.Length; I++)
            {
                if (Math.Abs(SAudio[I]) < 37 || Math.Abs(SAudio[I]) < short.MaxValue)
                    continue;

                Cosmos.HAL.PCSpeaker.Beep((uint)Math.Abs(SAudio[I]), (uint)Cosmos.System.Durations.Sixteenth);
            }
        }
    }
}