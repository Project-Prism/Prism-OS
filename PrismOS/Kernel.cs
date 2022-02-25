using PrismOS.Graphics;
using System.Drawing;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(1280, 720);

            while (true)
            {
                Canvas.Clear(Color.DarkSlateGray);
                Canvas.DrawCircle(20, 20, 10, Color.White);
                Canvas.DrawCircle(50, 50, 10, Color.White, 0, 180);
                Canvas.DrawFilledCircle(50, 20, 10, Color.White);
                Canvas.DrawFilledCircle(20, 50, 10, Color.White, 0, 180);
                Canvas.DrawString(100, 100, "Hello, World!", Color.White);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
        }
    }
}