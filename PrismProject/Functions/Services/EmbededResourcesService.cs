using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;


namespace PrismProject.Functions.Services
{
    class EmbededResourceService
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Functions.Services.Files.Boot.bmp")] public static byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Functions.Services.Files.Arrow.bmp")] public static byte[] Byte_Arrow;
        [ManifestResourceStream(ResourceName = "PrismProject.Functions.Services.Files.polyfish_lines.gif")] public static byte[] Phish;

        // Images
        public static Bitmap Boot_bmp = new Bitmap(Byte_Prism);
        public static Bitmap Arrow_bmp = new Bitmap(Byte_Arrow);
    }
}
