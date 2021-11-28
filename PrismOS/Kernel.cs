using System;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                var X = new Libraries.UI.Containers.Window(100, 100, 500, 500, 5, 5, false);
                X.Children.Add(new Libraries.UI.Components.Button(50, 50, 15, 35, 0, X));
                X.Children.Add(new Libraries.UI.Components.Label(100, 100, "Some Text goes here", X));

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