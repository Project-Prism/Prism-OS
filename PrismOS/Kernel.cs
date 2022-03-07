using PrismOS.Libraries.Graphics;
using System.Drawing;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Apps.GameOfLife GOL = new();
            Canvas Canvas = new(1024, 768, true);

            try
            {
                while (true)
                {
                    Canvas.Clear();
                    GOL.Update(Canvas);
                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS + (GOL.Paused ? "\nGame paused." : "\nGame running."), Color.White);
                    Canvas.Update();
                }
            }
            catch (Exception EX)
            {
                new Apps.Menus().Update1(EX.Message, Canvas);
            }
        }
    }
}