using IL2CPU.API.Attribs;
using PrismOS.Libraries.Formats;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string Base = "PrismOS.Files.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Content.bmp")] public readonly static byte[] ContentB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Logo.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;

        public static Image Cursor = new(CursorB);
        public static Image Logo = new(LogoB);
        public static Image Wallpaper = new(WallpaperB);
        public static Image Content = new(ContentB);
    }
}