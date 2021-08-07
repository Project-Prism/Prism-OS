using Cosmos.System;

namespace PrismProject.System2.proprietary
{
    internal class SetLayout
    {
        /// <summary>
        /// Sets a keyboard layout.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
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