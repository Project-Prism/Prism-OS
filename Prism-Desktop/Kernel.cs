using System;

namespace Prism
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Prism.Libraries.UI.Framework.Draw_Text_Button(400, 400, 400, 50);
        }
    }
}