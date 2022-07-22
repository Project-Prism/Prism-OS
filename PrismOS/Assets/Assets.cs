using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;
using Cosmos.System.Audio.IO;

namespace PrismOS
{
    public static class Assets
    {
        // Include The Files At Compile Time
        public const string Base = "PrismOS.Assets.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Logo.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Font1.bf")] public readonly static byte[] Font1B;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;
        [ManifestResourceStream(ResourceName = Base + "W98.wav")] public static readonly byte[] W98B;
        [ManifestResourceStream(ResourceName = Base + "W98OFF.wav")] public static readonly byte[] W98OFFB;

        // Misc
        public static FrameBuffer Wallpaper = FrameBuffer.FromBitmap(WallpaperB);
        public static FrameBuffer Cursor = FrameBuffer.FromBitmap(CursorB);

        // System Sounds
        public static MemoryAudioStream Window98Startup = MemoryAudioStream.FromWave(W98B);
        //public static MemoryAudioStream Window98Shutdown = MemoryAudioStream.FromWave(W98OFFB);

        // Pre-Sized Logos
        public static FrameBuffer Logo = FrameBuffer.FromBitmap(LogoB);
        //public static FrameBuffer Logo512 = Logo.Resize(512, 512);
        public static FrameBuffer Logo256 = Logo.Resize(256, 256);
        //public static FrameBuffer Logo128 = Logo.Resize(128, 128);
        //public static FrameBuffer Logo64 = Logo.Resize(64, 64);
    }
}