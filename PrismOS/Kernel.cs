using System;
using static PrismOS.UI.Framework;
using Cosmos.System.Graphics;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                var X = new Window(100, 100, 500, 500, 5, "Title1", null);
                var Z = new UI.Framework.Image(75, 75, Essential.Assets.Warning, X);
                X.Children.Add(Z);

                while (true)
                {
                    X.Draw();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error! " + exc);
            }
        }
    }
}