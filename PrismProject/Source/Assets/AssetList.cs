using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace PrismProject.Source.Assets
{
    class AssetList
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Boot.bmp")] public static byte[] Prism;
        //[ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Comfortaa.BFont")] public static byte[] ComfortaaFont;
        //[ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Default.Chars")] public static byte[] FontCharSet;

        public static string Font0 = "Comfortaa";
        public static Bitmap BootLogo = new Bitmap(Prism);

        public static void InitFont()
        {
            //Graphics.BitFont.RegisterBitFont(Font0, new Graphics.BitFontDescriptor(FontCharSet.ToString(), new MemoryStream(ComfortaaFont), 16));
        }
    }
}
