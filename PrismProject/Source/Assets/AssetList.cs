using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.IO;
using static PrismProject.Source.Graphics.BitFont;

namespace PrismProject.Source.Assets
{
    class AssetList
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Boot.bmp")] public static Bitmap BootLogo;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Comfortaa.BFont")] public static string ComfortaaFont;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Default.Chars")] public static string FontCharSet;
        private static readonly MemoryStream Comfortaa = new MemoryStream(Convert.FromBase64String(ComfortaaFont));
        public static string Font0 = "Comfortaa";
        public static void InitStatic()
        {
            RegisterBitFont("Comfortaa", new Graphics.BitFontDescriptor(FontCharSet, Comfortaa, 24));
        }
    }
}
