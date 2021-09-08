using PrismProject.Source.Tests;
using System.Drawing;
using System;
using IL2CPU.API.Attribs;
using Cosmos.System.Graphics;
using System.Threading;

namespace PrismProject.Source.Graphics
{
    /// <summary> Stuff for drawing </summary>
    class Drawables
    {

        //[ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.boot.bmp")] public static byte[] rnd;
        public static int Width = 800, Height = 600;
        private static readonly Tests.SVGAIICanvas Screen = new Tests.SVGAIICanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));

        public static void DrawCircle(int X, int Y, int Radius, Color color, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledCircle(new Pen(color), X, Y, Radius);
                    break;
                case false:
                    Screen.DrawCircle(new Pen(color), X, Y, Radius);
                    break;
                default:
                    Extras.CrashScreen.Main("Illegal operation while drawing to screen.");
                    break;
            }
        }
        public static void DrawRectangle(int X, int Y, int X2, int Y2, Color color, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledRectangle(new Pen(color), X, Y, X2, Y2);
                    break;
                case false:
                    Screen.DrawRectangle(new Pen(color), X, Y, X2, Y2);
                    break;
                default:
                    Extras.CrashScreen.Main("Illegal operation while drawing to screen.");
                    break;
            }
        }
        public static void DrawEllipse(int X, int Y, int X2, int Y2, Color color, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledEllipse(new Pen(color), X, Y, X2, Y2);
                    break;
                case false:
                    Screen.DrawEllipse(new Pen(color), X, Y, X2, Y2);
                    break;
                default:
                    Extras.CrashScreen.Main("Illegal operation while drawing to screen.");
                    break;
            }
        }
        public static void DrawImage(int X, int Y, Bitmap image)
        {
            Screen.DrawImageAlpha(image, X, Y);
        }
        public static void DrawTriangle(int X, int Y, int X2, int Y2, int X3, int Y3, Color color)
        {
            Screen.DrawTriangle(new Pen(color), X, Y, X2, Y2, X3, Y3);
        }
        public static void DrawRoundedRectangle(int X, int Y, int X2, int Y2, int R, Color color, int[] Sides)
        {
                int x2 = X + X2, y2 = Y + Y2, r2 = R + R;
                if (Sides[0] == 1) //Top
                {
                    Screen.DrawFilledCircle(new Pen(color), X + R, Y + R, R);
                    Screen.DrawFilledCircle(new Pen(color), x2 - R-5, Y + R, R);
                    Screen.DrawFilledRectangle(new Pen(color), X + R, Y - 1, X2 - r2, R);
                }
                if (Sides[1] == 1) //Bottom
                {
                    Screen.DrawFilledCircle(new Pen(color), X + R, y2 - R - 1, R);
                    Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y2 - R - 1, R);
                    Screen.DrawFilledRectangle(new Pen(color), X + R, y2 - R + 1, X2 - r2, R-1);
                }
                if (Sides[2] == 1) //Left
                {
                    Screen.DrawFilledCircle(new Pen(color), X + R, Y + R, R);
                    Screen.DrawFilledCircle(new Pen(color), X + R, y2 - R - 1, R);
                }
                if (Sides[3] == 1) //right
                {
                    Screen.DrawFilledCircle(new Pen(color), x2 - R, Y + R, R);
                    Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y2 - R - 1, R);
                }
                Screen.DrawFilledRectangle(new Pen(color), X, Y-1 + R, X2, Y2 - r2+1);
        }
        public static void DrawProgressBar(int X, int Y, int X2, int Y2, int Percent)
        {
            DrawRoundedRectangle(X, Y, X2, Y2, 50, Color.SlateGray, new int[]{1,1,1,1});
            DrawRoundedRectangle(X, Y, X2 / Percent, Y2, 50, Color.White, new int[]{1,1,1,1});
        }
        public static void DrawText(int X, int Y, string str, Color color)
        { Screen.DrawString(str, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(color), X, Y); }
        public static void TransparencyTest()
        {
            Screen.DrawCircle(new Pen(Screen.AlphaBlend(Color.FromArgb(24, 24, 24), Color.Red, 50)), 0, 0, 5);
        }
        public static void DrawAngle(int X, int Y, int angle, int radius, Color color)
        {
                int[] sine = new int[16] { 0, 27, 54, 79, 104, 128, 150, 171, 190, 201, 221, 233, 243, 250, 254, 255 };
                int xEnd, yEnd, quadrant, x_flip, y_flip;
                quadrant = angle / 15;
                switch (quadrant)
                {
                    case 0: x_flip = 1; y_flip = -1; break;
                    case 1: angle = Math.Abs(angle - 30); x_flip = y_flip = 1; break;
                    case 2: angle -= 30; x_flip = -1; y_flip = 1; break;
                    case 3: angle = Math.Abs(angle - 60); x_flip = y_flip = -1; break;
                    default: x_flip = y_flip = 1; break;
                }
                xEnd = X;
                yEnd = Y;
                if (angle > sine.Length) return;
                xEnd += (x_flip * ((sine[angle] * radius) >> 8));
                yEnd += (y_flip * ((sine[15 - angle] * radius) >> 8));

                Screen.DrawLine(new Pen(color), X, Y, xEnd, yEnd);
            }
        public static void Clear(Color color)
        {
            Screen.Clear(color);
        }
        public static void Tst()
        {
            Clear(Color.Black);
            //DrawImage(Width/2-128, Height/2-153, new Bitmap(rnd));
            Thread.Sleep(10000);
            Clear(Color.AliceBlue);
            DrawRoundedRectangle(Width / 2 - 100, Height / 2 - 100, Width / 2 + 50, Height / 2 + 50, 2, Color.FromArgb(255,200,200,200), new int[] { 1, 1, 1, 1 });
            DrawAngle(Width/2, Height/2, DateTime.Now.Hour, 40, Color.Black);
            DrawAngle(Width/2, Height/2, DateTime.Now.Minute, 60, Color.Black);
            DrawAngle(Width/2, Height/2, DateTime.Now.Second, 80, Color.Red);
        }
    }
    class CoalWM
    {
        
    }
}
