using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
using System.Collections.Generic;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static List<UI.SaltUI.Window> Windows = new();

        protected override void Run()
        {
            InitVFS();
            InitNet();

            UI.SaltUI.Window xDisplay = new(50, 50, 100, 50);
            xDisplay.Draw();
        }
    }
}