using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using static Cosmos.Core.CPU;
using static PrismProject.Graphics.Canvas2;
using static PrismProject.Core.FileSystem;
using static PrismProject.Services.Resource_Service;
using static PrismProject.Services.Network_Service;
using static PrismProject.Services.Time_Service;
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
                Basic.DrawString("Prism OS\nDate: " + Day + "/" + Month + "/" + Year + "\nInstalled ram: " + GetAmountOfRAM() + " MB\n\nSystem starting...", PCScreenFont.Default, Color.White, Width / 2, Height / 2 + (int)Prism.Height + 16, AnchorPoint.Center);
                StartDisk();
                NetStart(Local, Subnet, Gateway2);
            }
            catch (Exception exc)
            {
                Services.SystemException_Service.Main(exc);
            }
        }
    }
}