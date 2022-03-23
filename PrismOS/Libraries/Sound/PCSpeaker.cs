using PrismOS.Libraries.Formats;

namespace PrismOS.Libraries.Sound
{
    public unsafe static class PCSpeaker
    {
        public static void Play(WAVFile File, int TimeSpan = default)
        {
            // If timespan is default, set to WAVFiles's length, else, keep set value
            //TimeSpan = (TimeSpan == default ? File.Time : Timespan);

            for (int I = 0; I < File.Audio.Length; I++)
            {
                if (File.Audio[I] > 37 && File.Audio[I] < short.MaxValue)
                {
                    Cosmos.HAL.PCSpeaker.Beep((uint)File.Audio[I], 1);
                }
                System.Threading.Thread.Sleep((TimeSpan / File.Audio.Length) * 1000);
            }
        }
    }
}