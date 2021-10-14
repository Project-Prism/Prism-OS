using System;
using System.Drawing;
using static PrismProject.Graphics.Canvas2;
using static PrismProject.Services.Resource_Service;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

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
                Font: Default, // use default incase main font becomes corrupt.
                c: Color.Black,
                X: Width / 2,
                Y: Height / 2 + (int)Prism.Height + 32 - (int)Warning.Height, AnchorPoint.Center);

            _System.Threading.Thread.Sleep(5);
            
            Cosmos.System.Power.Shutdown();
        }
    }
}
