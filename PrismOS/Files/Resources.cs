using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string Base = "PrismOS.Files.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] private readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] private readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;

        public static Bitmap Wallpaper => new(WallpaperB);
        public static Bitmap Cursor => new(CursorB);
    }
}