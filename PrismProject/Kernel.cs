using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using static PrismProject.Functions.Graphics.Canvas2;
using static PrismProject.Functions.Core.FileSystem;
using static PrismProject.Functions.Services.EmbededResourceService;
using static PrismProject.Functions.Services.NetoworkService;
using static PrismProject.Functions.Services.TimeService;
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
                Basic.DrawString("Prism OS\nTime: " + Day + "/" + Month + "/" + Year, PCScreenFont.Default, Color.White, Width / 2, Height / 2 + (int)Boot_bmp.Height + 16);
                StartDisk();
                NetStart(Local, Subnet, Gateway2);
                // GifParser.Parse(Phish);
            }
            catch
            {
                Canvas.Clear(Color.Chocolate);
                Basic.DrawBMP(Width / 2, Height / 2, Boot_bmp);
                Basic.DrawString("A critical error occured and cannot be recovered.\nPrism OS will now shut down.", PCScreenFont.Default, Color.Black, Width / 2, Height / 2 + (int)Boot_bmp.Height + 16);
                Functions.System2.Threading.Thread.Sleep(5);
                Cosmos.System.Power.Shutdown();
            }
        }
    }
}