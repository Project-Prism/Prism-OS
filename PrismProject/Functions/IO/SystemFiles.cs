using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;


namespace PrismProject.Functions.IO
{
    class SystemFiles
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Functions.IO.Embeded.Images.Boot.bmp")] public static byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Functions.IO.Embeded.Images.Arrow.bmp")] public static byte[] Byte_Arrow;



        // Images
        public static Bitmap Boot_bmp = new Bitmap(Byte_Prism);
        public static Bitmap Arrow_bmp = new Bitmap(Byte_Arrow);

        // System folder structure
        public static string[] Folders = new string[] { @"0:\Prism-Core\", @"0:\Prism-Core\Global\", @"0:\Prism-Core\Global\temp\" };
        public static string[] Files = new string[] { @"0:\Prism-Core\System.conf", @"0:\Prism-Core\CrashLogs.log" };
    }
}
