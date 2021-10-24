using Cosmos.System.Graphics.Fonts;
using System.Collections.Generic;

namespace Prism.Services.Basic
{
    class FontManager
    {
        public static Dictionary<string, PCScreenFont> FontList = new();
        public static PCScreenFont Default = PCScreenFont.Default;

        public static void Load(string Name, byte[] FontData)
        {
            FontList.Add(Name, PCScreenFont.LoadFont(FontData));
        }

        public static void ApplyFont(PCScreenFont font)
        {
            Default = font;
        }
    }
}
