using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.IO;
using static PrismProject.Source.Graphics.BitFont;

namespace PrismProject.Source.Resources
{
    internal class ResourceList
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Resources.Boot.bmp")] public static Bitmap BootLogo;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Resources.Comfortaa.BFont")] public static string ComfortaaFont;
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Resources.Default.Chars")] public static string FontCharSet;
        public static string Font0 = "Comfortaa";

        public static void InitStatic()
        {
            RegisterBitFont(Font0, new Graphics.BitFontDescriptor(FontCharSet, new MemoryStream(Convert.FromBase64String(ComfortaaFont)), 16));
        }
    }
}