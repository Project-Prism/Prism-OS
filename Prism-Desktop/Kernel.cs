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
                //Services.WebService.StartServer();
                Graphics.DBACanvas.DrawFilledCircle(50, 50, 50, Color.White);
                Graphics.DBACanvas.DrawFilledCircle(50, 50, 49, Color.Black);
                
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