using PrismOS.Libraries.Resource.Images;
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
        [ManifestResourceStream(ResourceName = Base + "Splash.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Testing.xml")] public readonly static byte[] Testing1B;
        [ManifestResourceStream(ResourceName = Base + "Testing.ini")] public readonly static byte[] Testing2B;
        [ManifestResourceStream(ResourceName = Base + "Default.btf")] public readonly static byte[] Font1B;
        [ManifestResourceStream(ResourceName = Base + "Vista.wav")] public readonly static byte[] VistaB;
        [ManifestResourceStream(ResourceName = Base + "W98.wav")] public static readonly byte[] W98B;

        // Misc
        public static FrameBuffer Wallpaper = BMP.FromBitmap(WallpaperB);
        public static FrameBuffer Cursor = BMP.FromBitmap(CursorB);

        // System Sounds
        public static MemoryAudioStream Vista = MemoryAudioStream.FromWave(VistaB);

        // Pre-Sized Logos
        public static FrameBuffer Logo = BMP.FromBitmap(LogoB);
        public static FrameBuffer Logo512 = Logo.Resize(512, 512);
        public static FrameBuffer Logo256 = Logo.Resize(256, 256);
        public static FrameBuffer Logo128 = Logo.Resize(128, 128);
        public static FrameBuffer Logo64 = Logo.Resize(64, 64);
    }
}