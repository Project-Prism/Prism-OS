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
            Basic.DrawBMP(Width / 2, Height / 2 - (int)Warning.Height, Warning, AnchorPoint.Center);

            Basic.DrawString(
                Text: Locales.EN_US.CrashMSG + exc.Message,
                Font: SystemDefault,
                c: Color.Black,
                X: Width / 2,
                Y: Height / 2 + (int)Prism.Height + 32 - (int)Warning.Height, AnchorPoint.Center);

            Core.Threading.Thread.Sleep(100);
            
            Cosmos.System.Power.Shutdown();
        }
    }
}
