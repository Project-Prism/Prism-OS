using System.Drawing;
using Mouse = Cosmos.System.MouseManager;
using Canvas = PrismOS.Libraries.Graphics.Canvas;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(1920, 1080);
            Tests.TEngine T = new(Canvas);

            try
            {
                while (true)
                {
                    Canvas.Clear();
                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.White);
                    T.OnUpdate();
                    //Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch (Exception EX)
            {
                Canvas.VBE.DisableDisplay();
                Console.Clear();
                Console.WriteLine("Error! " + EX.Message);
                while (true) { }
            }
        }
    }
}