using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.IO;

namespace PrismProject.Source.Assets
{
    class AssetList
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Boot.bmp")] public static Bitmap BootLogo;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Comfortaa.BFont")] public static string ComfortaaFont;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Default.Chars")] public static string FontCharSet;
        public static string Font0 = "Comfortaa";

        public static void InitFont()
        {
            Graphics.BitFont.RegisterBitFont(Font0, new Graphics.BitFontDescriptor(FontCharSet, new MemoryStream(Convert.FromBase64String(ComfortaaFont)), 16));
        }
    }
}
