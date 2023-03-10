using Cosmos.System.Audio.IO;
using IL2CPU.API.Attribs;

namespace PrismOS
{
    public static class Media
    {
        // Include The Files At Compile Time
        public const string Base = "PrismOS.Media.";
        [ManifestResourceStream(ResourceName = Base + "Audio.Shutdown-Alt.wav")] public readonly static byte[] ShutdownAltB;
        [ManifestResourceStream(ResourceName = Base + "Audio.Shutdown.wav")] public readonly static byte[] ShutdownB;
        [ManifestResourceStream(ResourceName = Base + "Audio.Startup.wav")] public readonly static byte[] StartupB;
        [ManifestResourceStream(ResourceName = Base + "Executables.Test.elf")] public readonly static byte[] ELF;

        // System Sounds
        public static MemoryAudioStream ShutdownAlt = MemoryAudioStream.FromWave(ShutdownAltB);
        public static MemoryAudioStream Shutdown = MemoryAudioStream.FromWave(ShutdownB);
        public static MemoryAudioStream Startup = MemoryAudioStream.FromWave(StartupB);
    }
}