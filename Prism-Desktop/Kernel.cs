using Prism.Graphics;
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
                DBACanvas.DrawFilledCircle(50, 50, 50, Color.White);
                DBACanvas.DrawFilledCircle(50, 50, 49, Color.Black);
                BitmapCanvas x = new(Services.Basic.Resources.Warning);
                x.DrawString(20, 20, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, "Testing!", Color.Red);
                x.Dispose();

                while (true)
                {

                }
            }
            catch (Exception exc)
            {
                DBACanvas.Display.Disable();
                Console.WriteLine("Exception: " + exc.Message);
                while (true)
                {

                }
            }
        }
    }
}