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
                int r = 0;
                while (true)
                {
                    if (r == 255) { r = 0; }
                    r++;
                    ED1.Standard.Clear(Color.FromArgb(r, 0, 0));
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