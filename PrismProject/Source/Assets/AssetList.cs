using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System.IO;
using PrismProject.Source.Graphics;

namespace PrismProject.Source.Assets
{
    class AssetList
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Images.Boot.bmp")] private static byte[] PrismIcon;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Images.Arrow.bmp")] private static byte[] MouseIcon;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Fonts.Comfortaa.bitfont")] private static byte[] ComfortaaData;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Fonts.Charset.BCS")] private static byte[] CharSet;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Themes.Default.theme")] private static byte[] Theme_prism;

        public static string Comfortaa = "Comfortaa";
        public static Bitmap BootLogo = new Bitmap(PrismIcon);
        public static Bitmap MouseArrow = new Bitmap(MouseIcon);

        public static void InitFont()
        {
            BitFont.RegisterBitFont(Comfortaa, new BitFontDescriptor(CharSet.ToString(), new MemoryStream(ComfortaaData), 16));
        }
    }
}
