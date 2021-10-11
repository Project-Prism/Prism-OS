using System;
using System.Drawing;
using static PrismProject.Graphics.Canvas2;
using static PrismProject.Services.Resource_Service;
using static PrismProject.Services.FontManager_Service;

namespace PrismProject.Services
{
    class SystemException_Service
    {
        public static void Main(Exception exc)
        {
            Canvas.Clear(Color.Chocolate);
            Basic.DrawBMP(Width / 2, Height / 2, Warning, AnchorPoint.Center);
            Basic.DrawString("A critical error occured and cannot be recovered.\nPrism OS will now shut down.\n\nAditional info\n==============\n" + exc.Message, SystemDefault, Color.Black, Width / 2, Height / 2 + (int)Prism.Height + 16, AnchorPoint.Center);
            Core.Threading.Thread.Sleep(10);
            Cosmos.System.Power.Shutdown();
        }
    }
}
