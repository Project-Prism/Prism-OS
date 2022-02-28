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
            Paint Paint = new();
            Canvas Canvas = new(800, 480);

            while (true)
            {
                Canvas.Clear(Color.Green);
                Paint.Clock2(Canvas);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
        }
    }
}