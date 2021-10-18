using System;
using System.Drawing;
using static PrismProject.Display.Visual2D.Display;
using static PrismProject.Display.Visual2D.DisplayConfig;
using static PrismProject.Services.Resources;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

/// <summary>
/// Archive for old stuff
/// </summary>
namespace PrismProject.Archive
{
    class ReliabilityService
    {
        public static void ReportException(Exception exception)
        {
            // Not implemented yet
            // Should warn the user about an error in the system via a popup message
        }

        public static void CompleteFail(Exception exc)
        {
            Controler.Clear(Color.Chocolate);
            Basic.DrawBMP(Width / 2, Height / 2 - (int)Warning.Height, Warning, AnchorPoint.Center);
            Basic.DrawString(
                Text: "Prism OS has an issue! \nPlease report this issue if this is not a test build\n============\nAditional Info\n============\n" + exc.Message + "\n\nPress any key to continue.",
                Font: Default, // use default incase main font becomes corrupt.
                c: Color.Black,
                X: Width / 2,
                Y: Height / 2 + (int)Prism.Height + 32 - (int)Warning.Height, AnchorPoint.Center);
            Cosmos.System.KeyboardManager.ReadKey();
            return;
        }
        // Remove when report exception is implemented, stability is key and should avoid shutting down system.
    }
}
