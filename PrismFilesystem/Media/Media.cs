using Cosmos.System.Audio.IO;
using IL2CPU.API.Attribs;
using PrismGraphics;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value
#pragma warning disable CS8604 // Possibly null value

namespace PrismFilesystem
{
    public static class Media
    {
        // Include The Files At Compile Time
        public const string Base = "PrismFilesystem.Media.";
		[ManifestResourceStream(ResourceName = Base + "Audio.Shutdown-Alt.wav")] private readonly static byte[] ShutdownAltB;
		[ManifestResourceStream(ResourceName = Base + "Audio.Shutdown.wav")] private readonly static byte[] ShutdownB;
        [ManifestResourceStream(ResourceName = Base + "Audio.Startup.wav")] private readonly static byte[] StartupB;
        [ManifestResourceStream(ResourceName = Base + "Executables.Test.elf")] public readonly static byte[] ELF;
        [ManifestResourceStream(ResourceName = Base + "Images.Prism.bmp")] private readonly static byte[] PrismB;
        [ManifestResourceStream(ResourceName = Base + "DOOM1.WAD")] public readonly static byte[] Doom;

        // System Sounds
        public static MemoryAudioStream ShutdownAlt = MemoryAudioStream.FromWave(ShutdownAltB);
        public static MemoryAudioStream Shutdown = MemoryAudioStream.FromWave(ShutdownB);
        public static MemoryAudioStream Startup = MemoryAudioStream.FromWave(StartupB);

        // System Icons
        public static Graphics Prism = Image.FromBitmap(PrismB);
    }
}