using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;


namespace PrismProject.Functions.Services
{
    class EmbededResourceService
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Files.Boot.bmp")] public static byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Files.Arrow.bmp")] public static byte[] Byte_Arrow;

        // Images
        public static Bitmap Boot_bmp = new Bitmap(Byte_Prism);
        public static Bitmap Arrow_bmp = new Bitmap(Byte_Arrow);
    }
}
