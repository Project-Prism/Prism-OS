using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace PrismOS.Essential
{
    public static class Assets
    {
        [ManifestResourceStream(ResourceName = "PrismOS.Essential.Assets.Prism.bmp")] private static readonly byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismOS.Essential.Assets.Mouse.bmp")] private static readonly byte[] Byte_Mouse;
        [ManifestResourceStream(ResourceName = "PrismOS.Essential.Assets.Warning.bmp")] private static readonly byte[] Byte_Warning;

        // Images
        public static Bitmap Prism = new(Byte_Prism);
        public static Bitmap Mouse = new(Byte_Mouse);
        public static Bitmap Warning = new(Byte_Warning);
    }
}