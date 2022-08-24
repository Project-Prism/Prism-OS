using PrismOS.Libraries.Resource.Images;
using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;
using Cosmos.System.Audio.IO;

namespace PrismOS.Libraries.Resource
{
    public static class Assets
    {
        // Include The Files At Compile Time
        public const string Base = "PrismOS.Libraries.Resource.Assets.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Splash.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Default.btf")] public readonly static byte[] Font1B;
        [ManifestResourceStream(ResourceName = Base + "Vista.wav")] public readonly static byte[] VistaB;
        
        // Misc
        public static FrameBuffer Wallpaper = new BMP(WallpaperB);
        public static FrameBuffer Cursor = new BMP(CursorB);

        // System Sounds
        public static MemoryAudioStream Vista = MemoryAudioStream.FromWave(VistaB);

        // Pre-Sized Logos
        public static FrameBuffer Splash = new BMP(LogoB);
        public static FrameBuffer Splash512 = Splash.Resize(512, 512);
        public static FrameBuffer Splash256 = Splash.Resize(256, 256);
        public static FrameBuffer Splash128 = Splash.Resize(128, 128);
        public static FrameBuffer Splash64 = Splash.Resize(64, 64);
    }
}