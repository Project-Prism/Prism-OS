using System;
using static Prism.Libraries.UI.Framework;

namespace Prism
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                UI.Image Boot = new(Width / 2, Height / 2, Essential.Resources.Prism, null);
                Boot.Draw();

                UI.Panel X = new(Width / 2, Height / 2, 400, 400, null);
                X.Children.Add(new UI.Image(Width / 2, Height / 2, Essential.Resources.Warning, X));

                while (true)
                {
                    X.Width++;
                    X.Height++;
                    X.Draw();
                }
            }
            catch(Exception exc)
            {
                Canvas.Disable();
                Console.WriteLine("Error! " + exc);
            }
        }
    }
}