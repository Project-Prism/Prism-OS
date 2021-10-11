using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;


namespace PrismProject.Services
{
    class Resource_Service
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Files.Icons.Prism.bmp")] static readonly byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Files.Icons.Mouse.bmp")] static readonly byte[] Byte_Mouse;
        [ManifestResourceStream(ResourceName = "PrismProject.Files.Icons.Warning.bmp")] static readonly byte[] Byte_Warning;

        // Images
        public static Bitmap Prism = new Bitmap(Byte_Prism);
        public static Bitmap Mouse = new Bitmap(Byte_Mouse);
        public static Bitmap Warning = new Bitmap(Byte_Warning);
    }
}
