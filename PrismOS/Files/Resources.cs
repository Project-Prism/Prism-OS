using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string Base = "PrismOS.Files.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] private readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] private readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;
        [ManifestResourceStream(ResourceName = Base + "Logo.bmp")] public readonly static byte[] LogoB;

        public static Bitmap Wallpaper { get; } = new(WallpaperB);
        public static Bitmap Cursor { get; } = new(CursorB);
        public static Bitmap Logo { get; } = new(LogoB);
    }
}