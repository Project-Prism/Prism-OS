using System.Drawing;
using static PrismProject.Display.Visual2D.Display;
using static PrismProject.Display.Visual2D.DisplayConfig;
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
                StartDisk();
                NetStart(Local, Subnet);


                Basic.DrawRoundRect(200, 200, 400, 400, 6, Color.Blue);

                Basic.DrawBMP(Width / 2, Height / 2, Prism, AnchorPoint.Center);
                Basic.DrawString("Prism OS\nDate: " + Day + "/" + Month + "/" + Year + "\nTEST PURPOSES ONLY\n\nSystem starting...", Default, Color.White, Width / 2, Height / 2 + (int)Prism.Height + 16, AnchorPoint.Center);
                _System.Threading.Thread.Sleep(40);

                // end of kernel
                throw new Exception("End_Of_Kernel");
            }
            catch (Exception exc)
            {
                Services.ReliabilityService.CompleteFail(new Exception(exc.Message));
            }
        }
    }
}