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

        public static void DrawCircle(int X, int Y, int Radius, Pen pen, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledCircle(pen, X, Y, Radius);
                    break;
                case false:
                    Screen.DrawCircle(pen, X, Y, Radius);
                    break;
                default:
                    Extras.CrashScreen.Main("Illegal operation while drawing to screen.");
                    break;
            }
        }
        public static void DrawRectangle(int X, int Y, int X2, int Y2, Pen pen, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledRectangle(pen, X, Y, X2, Y2);
                    break;
                case false:
                    Screen.DrawRectangle(pen, X, Y, X2, Y2);
                    break;
                default:
                    Extras.CrashScreen.Main("Illegal operation while drawing to screen.");
                    break;
            }
        }
        public static void DrawEllipse(int X, int Y, int X2, int Y2, Pen pen, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledEllipse(pen, X, Y, X2, Y2);
                    break;
                case false:
                    Screen.DrawEllipse(pen, X, Y, X2, Y2);
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
        public static void DrawTriangle(int X, int Y, int X2, int Y2, int X3, int Y3, Pen pen)
        {
            Screen.DrawTriangle(pen, X, Y, X2, Y2, X3, Y3);
        }
        public static void DrawRoundedRectangle(int X, int Y, int X2, int Y2, int R, Pen pen, int[] Sides)
        {
            int x2 = X + X2, y2 = Y + Y2, r2 = R + R;
            Screen.DrawFilledRectangle(pen, X, Y + R, X2, Y2 - r2); // Left Right
            Screen.DrawFilledRectangle(pen, X + R, Y, X2 - r2, R);//Bottom Bar
            Screen.DrawFilledRectangle(pen, X + R, y2 - R, X2 - r2, R);//Top Bar

            if (Sides[0] == 1)
            { Screen.DrawFilledCircle(pen, X+R, Y+R, R); } else { } //Top Left
            if (Sides[1] == 1)
            { Screen.DrawFilledCircle(pen, x2-R-1, Y+R, R); } else { } //Top Right
            if (Sides[2] == 1)
            { Screen.DrawFilledCircle(pen, X + R, y2 - R - 1, R); } else { } //Bottom Left
            if (Sides[3] == 1)
            { Screen.DrawFilledCircle(pen, x2-R-1, y2-R-1, R); } else { } //Bottom Right
        }
        public static void DrawProgressBar(int X, int Y, int X2, int Y2, int Percent)
        {
            DrawRoundedRectangle(X, Y, X2, Y2, 50, Themes.Common[6], new int[]{1,1,1,1});
            DrawRoundedRectangle(X, Y, X2 / Percent, Y2, 50, Themes.Common[1], new int[]{1,1,1,1});
        }
        public static void DrawText(int X, int Y, string str, Pen pen)
        { Screen.DrawString(str, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, pen, X, Y); }
        public static void DrawAngle(int X, int Y, int Angle, int Radius, Pen pen)
        {
                int[] sine = new int[16] { 0, 27, 54, 79, 104, 128, 150, 171, 190, 201, 221, 233, 243, 250, 254, 255 };
                int xEnd, yEnd, quadrant, x_flip, y_flip;
                quadrant = Angle / 15;
                switch (quadrant)
                {
                    case 0: x_flip = 1; y_flip = -1; break;
                    case 1: Angle = Math.Abs(Angle - 30); x_flip = y_flip = 1; break;
                    case 2: Angle -= 30; x_flip = -1; y_flip = 1; break;
                    case 3: Angle = Math.Abs(Angle - 60); x_flip = y_flip = -1; break;
                    default: x_flip = y_flip = 1; break;
                }
                xEnd = X;
                yEnd = Y;
                if (Angle > sine.Length) return;
                xEnd += (x_flip * ((sine[Angle] * Radius) >> 8));
                yEnd += (y_flip * ((sine[15 - Angle] * Radius) >> 8));

                Screen.DrawLine(pen, X, Y, xEnd, yEnd);
            }
        public static void DrawWindowBase(int X, int Y, int X2, int Y2, int R, Pen[] pens)
        {
            DrawRoundedRectangle(X-1, Y-1, X2+2, Y2+2+(Y2/20), R, pens[4], new int[]{1,1,1,1}); //shadow
            DrawRoundedRectangle(X, Y+(Y2/20), X2, Y2, R, pens[2], new int[]{1,1,1,1}); //window
            DrawRoundedRectangle(X, Y, X2, Y2/20, R, pens[0], new int[]{1,1,1,1}); //title bar
        }
        public static void Clear(Pen pen)
        {
            Screen.Clear(pen.Color);
        }
        public static void TestScreen()
        {
            int clockscale = 5;
            //DrawImage(Width/2-128, Height/2-153, new Bitmap(rnd));
            Clear(Themes.Common[3]);
            DrawWindowBase(Width/2-150, Height/2-150, Width/2-100, Height/2, 4, Themes.Window);
            DrawCircle(Width/2, Height/2, clockscale*16, Themes.Common[1], true);
            DrawAngle(Width/2, Height/2, DateTime.Now.Hour, clockscale*4, Themes.Common[0]);
            DrawAngle(Width/2, Height/2, DateTime.Now.Minute, clockscale*8, Themes.Common[0]);
            DrawAngle(Width/2, Height/2, DateTime.Now.Second, clockscale*12, Themes.Common[2]);
        }
    }
    class CoalWM
    {
        
    }
}
