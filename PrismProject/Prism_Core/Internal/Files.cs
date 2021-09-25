using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System.Drawing;
using static PrismProject.Prism_Core.System2.Convert;

namespace PrismProject.Prism_Core.Internal
{
    class Files
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Images.Boot.bmp")] public static byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Images.Arrow.bmp")] public static byte[] Byte_Arrow;
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Comfortaa.font")] public static byte[] FontConfig_Comfortaa;
        [ManifestResourceStream(ResourceName = "PrismProject.Prism_Core.Internal.Embeded.Default.theme")] public static byte[] ThemeFile;

        // Images
        public static Bitmap Boot_bmp = new Bitmap(Byte_Prism);
        public static Bitmap Arrow_bmp = new Bitmap(Byte_Arrow);

        // Fonts
        private static readonly string[] Fonts = FontConfig_Comfortaa.ToString().Split("\n");
        public static string Charset = Fonts[0];
        public static string Comfortaa_Name = Fonts[1];
        public static byte[] Comfortaa_Data = ToByteArray(Fonts[2]);

        // Themes
        private static readonly string[] RawThemeData = ThemeFile.ToString().Split("\n");
        public static Color[] WindowTheme = ToColorArray(RawThemeData[0].Split(" : "));
        public static Color[] ProgBarTheme = ToColorArray(RawThemeData[1].Split(" : "));

        // System folder structure
        public static string System_Folder = @"0:\Prism-Core\";
        public static string System_Config = @"0:\Prism-Core\System.conf";
        public static string System_log = @"0:\Prism-Core\CrashLogs.log";
        public static string Global_Folder = @"0:\Prism-Core\Global\";
        public static string Global_Tmp = @"0:\Prism-Core\Global\temp\";
    }
}
