using PrismOS.Libraries.Graphics;
using IL2CPU.API.Attribs;
using Cosmos.System.Audio.IO;

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
        [ManifestResourceStream(ResourceName = Base + "W98.wav")] public static readonly byte[] W98B;
        [ManifestResourceStream(ResourceName = Base + "W98OFF.wav")] public static readonly byte[] W98OFFB;

        public static FrameBuffer Cursor = FrameBuffer.FromBitmap(CursorB);
        public static FrameBuffer Logo = FrameBuffer.FromBitmap(LogoB);
        public static FrameBuffer Wallpaper = FrameBuffer.FromBitmap(WallpaperB);
        public static MemoryAudioStream W98 = MemoryAudioStream.FromWave(W98B);
        public static MemoryAudioStream W98OFF = MemoryAudioStream.FromWave(W98OFFB);
    }
}