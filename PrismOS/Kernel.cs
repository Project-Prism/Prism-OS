using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics;
using System.Drawing;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(1024, 768, true);

            try
            {
                while (true)
                {
                    Canvas.Clear(Color.Green);

                    if (Mouse.X > 100 && Mouse.Y > 100 && Mouse.X < 300 && Mouse.Y < 225)
                    {
                        Canvas.DrawFilledRectangle(100, 100, 200, 125, 0, Color.LightGray);
                    }
                    else
                    {
                        Canvas.DrawFilledRectangle(100, 100, 200, 125, 0, Color.White);
                    }

                    Canvas.DrawString(0, 0, "Center text!", Color.White, Canvas.Position.Center);
                    Canvas.DrawString(5, 5, "Center text!", Color.White, Canvas.Position.Center);
                    Canvas.DrawString(0, 0, "left text!", Color.White, Canvas.Position.Left);
                    Canvas.DrawString(0, 0, "right text!", Color.White, Canvas.Position.Right);
                    Canvas.DrawString(0, 0, "top text!", Color.White, Canvas.Position.Top);
                    Canvas.DrawString(0, 0, "bottom text!", Color.White, Canvas.Position.Bottom);

                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.White);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
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