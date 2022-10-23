using Cosmos.System.Audio.IO;
using IL2CPU.API.Attribs;
using PrismGL2D.Formats;

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
        public static Bitmap Wallpaper = new(WallpaperB);
        public static Bitmap Cursor = new(CursorB);

        // System Sounds
        public static MemoryAudioStream Vista = MemoryAudioStream.FromWave(VistaB);

        // Pre-Sized Logo
        public static Bitmap Splash = (Bitmap)new Bitmap(LogoB).Scale(256, 256);
    }
}