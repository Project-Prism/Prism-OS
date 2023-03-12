using Cosmos.System.Audio.IO;
using IL2CPU.API.Attribs;
using PrismGraphics;

namespace PrismOS
{
    public static class Media
    {
        // Include The Files At Compile Time
        public const string Base = "PrismOS.Media.";
        [ManifestResourceStream(ResourceName = Base + "Audio.Shutdown-Alt.wav")] private readonly static byte[] ShutdownAltB;
        [ManifestResourceStream(ResourceName = Base + "Audio.Shutdown.wav")] private readonly static byte[] ShutdownB;
        [ManifestResourceStream(ResourceName = Base + "Audio.Startup.wav")] private readonly static byte[] StartupB;
        [ManifestResourceStream(ResourceName = Base + "Executables.Test.elf")] public readonly static byte[] ELF;
        [ManifestResourceStream(ResourceName = Base + "Images.Prism.bmp")] private readonly static byte[] PrismB;

        // System Sounds
        public static MemoryAudioStream ShutdownAlt = MemoryAudioStream.FromWave(ShutdownAltB);
        public static MemoryAudioStream Shutdown = MemoryAudioStream.FromWave(ShutdownB);
        public static MemoryAudioStream Startup = MemoryAudioStream.FromWave(StartupB);

        // System Icons
        public static Image Prism = Image.FromBitmap(PrismB);
    }
}