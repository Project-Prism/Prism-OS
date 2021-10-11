using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using static Cosmos.Core.CPU;
using static PrismProject.Functions.Graphics.Canvas2;
using static PrismProject.Functions.Core.FileSystem;
using static PrismProject.Functions.Services.EmbededResourceService;
using static PrismProject.Functions.Services.Network_Service;
using static PrismProject.Functions.Services.Time_Service;
// using PrismProject.Functions.System2.Types;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Basic.DrawBMP(Width / 2, Height / 2, Boot_bmp);
                Basic.DrawString("Prism OS\nDate: " + Day + "/" + Month + "/" + Year + "\nInstalled ram: " + GetAmountOfRAM() + "MB\n\nSystem starting...", PCScreenFont.Default, Color.White, Width / 2, Height / 2 + (int)Boot_bmp.Height + 16);
                StartDisk();
                NetStart(Local, Subnet, Gateway2);
            }
            catch
            {
                Canvas.Clear(Color.Chocolate);
                Basic.DrawBMP(Width / 2, Height / 2, Boot_bmp);
                Basic.DrawString("A critical error occured and cannot be recovered.\nPrism OS will now shut down.", PCScreenFont.Default, Color.Black, Width / 2, Height / 2 + (int)Boot_bmp.Height + 16);
                Core.Threading.Thread.Sleep(5);
                Cosmos.System.Power.Shutdown();
            }
        }
    }
}