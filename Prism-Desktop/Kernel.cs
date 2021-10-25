using System;
using System.Drawing;

namespace Prism
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Services.WebService.StartServer();
                Graphics.DBACanvas.DrawFilledCircle(50, 50, 50, Color.White);
                Graphics.DBACanvas.DrawFilledCircle(50, 50, 49, Color.Black);
                var x = new Graphics.BitmapCanvas(Services.Basic.Resources.Warning);
                x.DrawString(20, 20, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, "Testing!", Color.Red);

                while (true)
                {

                }
            }
            catch (Exception exc)
            {
                Graphics.DBACanvas.Display.Disable();
                Console.WriteLine("Exception: " + exc.Message);
                while (true)
                {

                }
            }
        }
    }
}