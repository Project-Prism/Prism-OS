using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Files
{
    public static class Resources
    {
        public const string IconBase = "PrismOS.Files.Icons.";
        public const string SoundBase = "PrismOS.Files.Sound.";
        [ManifestResourceStream(ResourceName = IconBase + "Wallpaper.bmp")] private readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = IconBase + "Cursor.bmp")] private readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = IconBase + "Logo.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = SoundBase + "Audio.wav")] public readonly static byte[] Audio;

        public static Bitmap Wallpaper { get; } = new(WallpaperB);
        public static Bitmap Cursor { get; } = new(CursorB);
        public static Bitmap Logo { get; } = new(LogoB);
    }
}