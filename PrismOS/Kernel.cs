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
            Console.Clear();
            Canvas Canvas = new(1920, 1080);

            try
            {
                for (int I = 4000; I > 0; I--)
                {
                    Canvas.Clear();
                    Canvas.DrawCircle(Canvas.Width / 2, Canvas.Height / 2, 9, Color.White, I, I + 270);
                    Canvas.DrawCircle(Canvas.Width / 2, Canvas.Height / 2, 10, Color.White, I, I + 270);
                    Canvas.DrawCircle(Canvas.Width / 2, Canvas.Height / 2, 11, Color.White, I, I + 270);
                    Canvas.Update();
                }

                while (true)
                {
                    Canvas.Clear(Color.Green);
                    if (Cosmos.System.KeyboardManager.AltPressed)
                    {
                        Canvas.Clear(Color.FromArgb(100, 25, 25, 25));
                        Canvas.DrawFilledRectangle(Canvas.Width / 2 - 100, Canvas.Height / 2 - 50, 200, 100, Color.White);
                        Canvas.DrawFilledRectangle(Canvas.Width / 2 - 100, Canvas.Height / 2 - 50, 200, 15, Color.DarkSlateBlue);
                        Canvas.DrawString(Canvas.Width / 2 - 100, Canvas.Height / 2 - 50, "UAC Prompt", Color.White);
                    }
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch { }
        }
    }
}