using System;
using static PrismOS.UI.Framework;
using static PrismOS.Storage.Extras;
using System.IO;
using Cosmos.System.Graphics;
using Arc;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Initiate();

                //var A = new UI.Framework.Image(0, 0, new Bitmap(File.ReadAllBytes(CD_Drive + ":\\img.bmp")), null);
                var B = new LoadBar(400, 500, 512, 0);

                while (true)
                {
                    //A.Draw();
                    B.Draw();
                    B.Percent++;
                }
            }
            catch (Exception exc)
            {
                UI.Extras.Canvas.Disable();
                Console.WriteLine("Error! " + exc);
                while (true)
                {
                }
            }
        }
    }
}