using PrismOS.Libraries.Formats;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string Base = "PrismOS.Files.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] private readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Logo.bmp")] private readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;
        [ManifestResourceStream(ResourceName = Base + "NoIcon.bmp")] private readonly static byte[] FileDefaultB;

        public static Bitmap Wallpaper { get; } = new(WallpaperB);
        public static Bitmap Cursor { get; } = new(CursorB);
        public static Bitmap Logo { get; } = new(LogoB);
        public static Bitmap NoIcon { get; } = new Bitmap(FileDefaultB);
    }
}