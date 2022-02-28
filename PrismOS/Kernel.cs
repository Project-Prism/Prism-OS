using System.Drawing;
using Canvas = PrismOS.Graphics.Canvas;
using Mouse = Cosmos.System.MouseManager;
using PrismOS.Tests;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(1280, 720);
            Paint Paint = new();

            try
            {
                while (true)
                {
                    Canvas.Clear(Color.Green);
                    Graphics.UI.Clock(Canvas);
                    //Paint.Clock2(Canvas);
                    Canvas.DrawString(15, 60, Canvas.Width + "x" + Canvas.Height + " (" + Canvas.FPS + " FPS)", Color.White);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch { }
        }
    }
}