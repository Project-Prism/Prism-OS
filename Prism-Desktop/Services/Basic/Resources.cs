using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace Prism.Services.Basic
{
    internal static class Resources
    {
        [ManifestResourceStream(ResourceName = "Prism.Files.Icons.Prism.bmp")] static readonly byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "Prism.Files.Icons.Mouse.bmp")] static readonly byte[] Byte_Mouse;
        [ManifestResourceStream(ResourceName = "Prism.Files.Icons.Warning.bmp")] static readonly byte[] Byte_Warning;

        // Images
        public static Bitmap Prism = new(Byte_Prism);
        public static Bitmap Mouse = new(Byte_Mouse);
        public static Bitmap Warning = new(Byte_Warning);
    }
}
