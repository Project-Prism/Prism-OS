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
                x.AddChild(new Image(50, 50, Services.Basic.Resources.Warning));
                

                while (true)
                {
                    x.Update();
                    TickForward();
                }
            }
            catch (Exception exc)
            {
                new Window(400, 300, 200, 50, "Error! " + exc.Message);
            }
        }
    }
}