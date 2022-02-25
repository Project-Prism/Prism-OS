using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string Base = "PrismOS.Files.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] private static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] private static byte[] CursorB;

        public static Bitmap Wallpaper => new(WallpaperB);
        public static Bitmap Cursor => new(CursorB);
    }
}