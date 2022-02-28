using System.Drawing;
using Canvas = PrismOS.Graphics.Canvas;
using Mouse = Cosmos.System.MouseManager;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static string Status = "Status: Normal";

        protected override void Run()
        {
            Canvas Canvas = new(800, 480);

            try
            {
                while (true)
                {
                    Canvas.Clear(Color.Green);
                    Graphics.UI.Clock(Canvas);
                    //Paint.Clock2(Canvas);
                    Canvas.DrawString(15, 15, Status, Color.White);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch (Exception EX)
            {
                Status = EX.Message;
            }
        }
    }
}