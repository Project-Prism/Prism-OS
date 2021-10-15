using System;
using System.Drawing;
using static PrismProject.Display.Visual2D.Canvas2;
using static PrismProject.Services.Resources;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismProject.Services
{
    class ReliabilityService
    {
        public static void Check_FS_Integrity()
        {
            throw new NotImplementedException();
        }

        public static void ReportException(Exception exception)
        {
            // Not implemented yet
            // Should warn the user about an error in the system via a popup message
        }

        public static void CompleteFail(Exception exc)
        {
            Canvas.Clear(Color.Chocolate);
            Basic.DrawBMP(Width / 2, Height / 2 - (int)Warning.Height, Warning, AnchorPoint.Center);

            Basic.DrawString(
                Text: "Prism OS has an issue, and needs to restart.\nPlease report this issue if this is not a test build\n============\nAditional Info\n============\n" + exc.Message,
                Font: Default, // use default incase main font becomes corrupt.
                c: Color.Black,
                X: Width / 2,
                Y: Height / 2 + (int)Prism.Height + 32 - (int)Warning.Height, AnchorPoint.Center);

            _System.Threading.Thread.Sleep(5);
            
            Cosmos.System.Power.Shutdown();
        }
        // Remove when report exception is implemented, stability is key and should avoid shutting down system.
    }
}
