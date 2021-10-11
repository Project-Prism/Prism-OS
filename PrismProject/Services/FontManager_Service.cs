using Cosmos.System.Graphics.Fonts;
using System;

namespace PrismProject.Services
{
    class FontManager_Service
    {
        public static PCScreenFont SystemDefault = PCScreenFont.Default;

        public static PCScreenFont GetFont(string FontData)
        {
            return PCScreenFont.LoadFont(Convert.FromBase64String(FontData));
        }

        public static void ApplyFont(PCScreenFont font)
        {
            SystemDefault = font;
        }
    }
}
