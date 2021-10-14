using static PrismProject.Services.FontManager_Service;
using System.Drawing;
using static Cosmos.Core.CPU;
using static PrismProject.Graphics.Canvas2;
using static PrismProject.Filesystem.Functions;
using static PrismProject.Services.Resource_Service;
using static PrismProject.Services.Network_Service;
using static PrismProject.Services.Time_Service;
using PrismProject.Services;
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
                Basic.DrawString("Prism OS\nDate: " + Day + "/" + Month + "/" + Year + "\nTEST PURPOSES ONLY\n\nSystem starting...", SystemDefault, Color.White, Width / 2, Height / 2 + (int)Prism.Height + 16, AnchorPoint.Center);

                StartDisk();
                NetStart(Local, Subnet, Gateway2);
            }
            catch (Exception exc)
            {
                SystemException_Service.Main(exc);
            }
        }
    }
}