using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS
{
    public static class Assets
    {
        public const string Base = "PrismOS.Assets.";
        [ManifestResourceStream(ResourceName = Base + "Wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "Cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "Logo.bmp")] public readonly static byte[] LogoB;
        [ManifestResourceStream(ResourceName = Base + "Font1.bf")] public readonly static byte[] Font1B;
        [ManifestResourceStream(ResourceName = Base + "Audio.wav")] public readonly static byte[] Audio;

        public static FrameBuffer Cursor = FrameBuffer.FromBitmap(CursorB);
        public static FrameBuffer Logo = FrameBuffer.FromBitmap(LogoB);
        public static FrameBuffer Wallpaper = FrameBuffer.FromBitmap(WallpaperB);
    }
}