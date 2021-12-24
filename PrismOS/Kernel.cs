using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
using System.Collections.Generic;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            //InitVFS();
            //InitNet();

            List<UI.SaltUI.Window> Windows = new();

            Windows.Add(new UI.SaltUI.Window(50, 50, 100, 50));

            while (true)
            {
                Windows[0].Draw();
            }
        }
    }
}