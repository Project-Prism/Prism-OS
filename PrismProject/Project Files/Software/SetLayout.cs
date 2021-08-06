using Cosmos.System;

namespace PrismProject
{
    internal class SetLayout
    {
        public static void Keyboard(string type, string[] args)
        {
            if (type == "layout")
            {
                var layout = args[0];
                if (layout == "FR")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FR_Standard());
                else if (layout == "US")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.US_Standard());
                else if (layout == "DE")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DE_Standard());
            }
        }
    }
}