using Cosmos.System.Graphics;
using PrismOS.Graphics;
using PrismOS.Tests;
using System;
using System.Drawing;
using Canvas = PrismOS.Graphics.Canvas;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(800, 480);
            int Frames = 0;
            DateTime LT = DateTime.Now;

            while (true)
            {
                Canvas.Clear(Color.DarkSlateGray);
                //Canvas.DrawCircle(20, 20, 10, Color.White);
                //Canvas.DrawCircle(50, 50, 10, Color.White, 0, 180);
                //Canvas.DrawFilledCircle(50, 20, 10, Color.White);
                //Canvas.DrawFilledCircle(20, 50, 10, Color.White, 0, 180);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
        }
    }
}