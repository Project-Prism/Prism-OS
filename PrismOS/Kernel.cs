using System.Drawing;
using Mouse = Cosmos.System.MouseManager;
using Canvas = PrismOS.Libraries.Graphics.Canvas;
using System;
using System.Collections.Generic;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Console.Clear();
            Canvas Canvas = new(1920, 1080);
            List<Point> Points = new();
            //Libraries.Sound.PCSpeaker.Tick(0.25, 1000, 100);
            double Freq = 40.0;

            try
            {
                while (true)
                {
                    Canvas.Clear(Color.Green);
                    foreach (Point P in Points)
                    {
                        foreach (Point P2 in Points)
                        {
                            if (P.X == P2.X && P.Y == P2.Y)
                                return;
                            Canvas.DrawLine(P.X, P.Y, P2.X, P2.Y, Color.White);
                        }
                    }

                    if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        Canvas.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Red);
                        Points.Add(new((int)Mouse.X, (int)Mouse.Y));
                    }
                    if (Mouse.MouseState == Cosmos.System.MouseState.Right)
                    {
                        Canvas.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Blue);
                        Points.Clear();
                    }
                    if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
                    {
                        switch (Key.Key)
                        {
                            case Cosmos.System.ConsoleKeyEx.NumPlus:
                                Freq += 10;
                                break;
                            case Cosmos.System.ConsoleKeyEx.NumMinus:
                                Freq -= 10;
                                break;
                        }
                    }

                    //Canvas.DrawFilledRectangle(300, 300, 200, 50, Color.DarkSlateGray);
                    //Canvas.DrawString(300, 300, "Sine wave demo (" + Freq + " HZ)", Color.White);
                    //Canvas.DrawSine(300, 315, 200, 50, Color.White, Freq);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch { }
        }
    }
}