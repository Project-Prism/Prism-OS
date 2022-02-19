using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] private static readonly byte[] WallPaperBytes;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] private static readonly byte[] CursorBytes;

        public const string Base = "PrismOS.Files.";
        public static Bitmap WallPaper { get; set; } = new(WallPaperBytes);
        public static Bitmap Cursor { get; set; } = new(CursorBytes);
    }
}