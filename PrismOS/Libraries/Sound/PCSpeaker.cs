namespace PrismOS.Libraries.Sound
{
    public static class PCSpeaker
    {
        public static void Play(Cosmos.HAL.Drivers.PCI.Audio.PCMStream PCM)
        {
            for (int I = 0; I < PCM.getData().Length; I++)
            {
                Beep((short)System.Math.Abs(PCM.getData()[I]), 0.1);
            }
        }

        public static void Beep(short Freq, double Duration)
        {
            Cosmos.HAL.PCSpeaker.Beep((uint)System.Math.Abs(Freq), (uint)Duration);
        }
    }
}