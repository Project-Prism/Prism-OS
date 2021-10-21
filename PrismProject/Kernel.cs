using System;
using static PrismProject.Graphics.WindowManager;
using static PrismProject.Services.Basic.Mouse_Service;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                var x = new Window(30, 30, 500, 500, "test!");
                x.AddChild(new Image(50, 50, Services.Basic.Resources.Warning, (self) => { self.X = 0; }));
                x.AddChild(new Button(100, 100, 50, 50, "Click me pls", (self) => { self.Label = "Clicked!"; }));
                

                while (true)
                {
                    x.Render();
                    TickForward();
                }
            }
            catch (Exception exc)
            {
                Graphics.Canvas2.Screen.Disable();
                Console.WriteLine("Exception: " + exc.Message);
            }
        }
    }
}