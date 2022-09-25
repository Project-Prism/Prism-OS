using Cosmos.System.Audio.IO;
using IL2CPU.API.Attribs;
using PrismGL2D.Formats;
using PrismGL2D;

namespace PrismOS
{
    public static class Assets
    {
        // Include The Files At Compile Time
        public const string Base = "PrismOS.Assets.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Splash.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Vista.wav")] public readonly static byte[] VistaB;
        
        // Misc
        public static Graphics Wallpaper = new Bitmap(WallpaperB);
        public static Graphics Cursor = new Bitmap(CursorB);

        // System Sounds
        public static MemoryAudioStream Vista = MemoryAudioStream.FromWave(VistaB);

        // Pre-Sized Logos
        public static Graphics Splash = new Bitmap(LogoB);
        public static Graphics Splash512 = Splash.Resize(512, 512);
        public static Graphics Splash256 = Splash.Resize(256, 256);
        public static Graphics Splash128 = Splash.Resize(128, 128);
        public static Graphics Splash64 = Splash.Resize(64, 64);
    }
}