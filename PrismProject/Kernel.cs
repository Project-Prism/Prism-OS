using System.Drawing;
using static PrismProject.Display.Visual2D.Canvas2;
using static PrismProject.Filesystem.FSCore;
using static PrismProject.Services.Resources;
using static PrismProject.Network.NetworkCore;
using static PrismProject.Services.Time;
using static PrismProject.Services.FontManager;
using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Basic.DrawBMP(Width / 2, Height / 2, Prism, AnchorPoint.Center);
                Basic.DrawString("Prism OS\nDate: " + Day + "/" + Month + "/" + Year + "\nTEST PURPOSES ONLY\n\nSystem starting...", Default, Color.White, Width / 2, Height / 2 + (int)Prism.Height + 16, AnchorPoint.Center);

                StartDisk();
                NetStart(Local, Subnet, Gateway2);

                // end of kernel
                throw new Exception("Reached the end of the kernel, code execution can no longer continue.");
            }
            catch (Exception exc)
            {
                Services.ReliabilityService.CompleteFail(new Exception(exc.Message));
            }
        }
    }
}