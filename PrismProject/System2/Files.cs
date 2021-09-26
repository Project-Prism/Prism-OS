using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;


namespace PrismProject.System2
{
    class Files
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Images.Boot.bmp")] public static byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Images.Arrow.bmp")] public static byte[] Byte_Arrow;
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Default.theme")] public static byte[] ThemeFile;



        // Images
        public static Bitmap Boot_bmp = new Bitmap(Byte_Prism);
        public static Bitmap Arrow_bmp = new Bitmap(Byte_Arrow);

        // System folder structure
        public static string System_Folder = @"0:\Prism-Core\";
        public static string System_Config = @"0:\Prism-Core\System.conf";
        public static string System_log = @"0:\Prism-Core\CrashLogs.log";
        public static string Global_Folder = @"0:\Prism-Core\Global\";
        public static string Global_Tmp = @"0:\Prism-Core\Global\temp\";
    }
}
