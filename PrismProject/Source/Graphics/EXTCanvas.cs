using Cosmos.System.Graphics;
using System;
using System.Drawing;
using static Cosmos.System.Graphics.ColorDepth;

namespace PrismProject.Source.Graphics
{
    class ExtendedCanvas
    {
        public static int Width = 720;
        public static int Height = 480;
        public static SVGAIICanvas EXTCanvas;

        public static void EXTInit()
        {
            EXTCanvas = new SVGAIICanvas(new Mode(Width, Height, ColorDepth32));
            EXTCanvas.Disable();
            EXTCanvas.Display();
        }

        public static void DrawRect(int X1, int Y1, int X2, int Y2, Color C, bool filled)
        {
            switch (filled)
            {
                case true:
                    EXTCanvas.DrawFilledRectangle(new Pen(C), X1, Y1, X2, Y2);
                    break;
                case false:
                    EXTCanvas.DrawRectangle(new Pen(C), X1, Y1, X2, Y2);
                    break;
            }
        }

        public static void DrawCirc(int X1, int Y1, int R, Color C, bool filled)
        {
            switch (filled)
            {
                case true:
                    EXTCanvas.DrawFilledCircle(new Pen(C), X1, Y1, R);
                    break;
                case false:
                    EXTCanvas.DrawCircle(new Pen(C), X1, Y1, R);
                    break;
            }
        }

        public static void DrawLine(int X1, int Y1, int X2, int Y2, Color C)
        {
            EXTCanvas.DrawLine(new Pen(C), X1, Y1, X2, Y2);
        }

        public static void DrawBMP(int X1, int Y1, Bitmap BMP)
        {
            EXTCanvas.DrawImageAlpha(BMP, X1, Y1);
        }

        public static void DrawRoundRect(int X1, int Y1, int X2, int Y2, int R, Color C, int[] sides)
        {
            DrawRect(X1 + R, Y1, X2 - R, Y2, C, true);
            DrawRect(X1, Y1 + R, X2, Y2 - R, C, true);

            if (sides[0] == 1)
            {
                DrawCirc(X1, Y1, R, C, true);
            }
            else
            {
                DrawRect(X1, Y1, X1 + R, Y1 + R, C, true);
            }
            if (sides[1] == 1)
            {
                DrawCirc(X2, Y1, R, C, true);
            }
            else
            {
                DrawRect(X2, Y2, X2 - R, Y2 + R, C, true);
            }
            if (sides[2] == 1)
            {
                DrawCirc(X1, Y2, R, C, true);
            }
            else
            {
                DrawRect(X1, Y2, X1 + R, Y2 - R, C, true);
            }
            if (sides[3] == 1)
            {
                DrawCirc(X2, Y2, R, C, true);
            }
            else
            {
                DrawRect(X2, Y2, X2 - R, Y2 - R, C, true);
            }
        }

        public static void DrawPage(int X1, int Y1, int X2, int Y2, int R, Color[] Theme)
        {
            DrawRoundRect(X1 - 1, Y1 - 1, X2 + 2, Y2 + 2 + (Y2 / 20), R, Theme[4], new int[] { 1, 1, 1, 1 }); //shadow
            DrawRoundRect(X1, Y1 + (Y2 / 20), X2, Y2, R, Theme[2], new int[] { 1, 1, 1, 1 }); //window
            DrawRoundRect(X1, Y1, X2, Y2 / 20, R, Theme[0], new int[] { 1, 1, 1, 1 }); //title bar
        }

        public static void DrawAngle(int X, int Y, int Angle, int Radius, Color color)
        {
            Double angleX, angleY;
            angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
            angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);
            DrawLine(X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100), color);
        }

        public static void DrawProgBar(int X1, int Y1, int X2, int Y2, int Percent)
        {
            DrawRoundRect(X1, Y1, X2, Y2, 50, Themes.ProgBar[0], new int[] { 1, 1, 1, 1 });
            DrawRoundRect(X1, Y1, X2 / Percent, Y2, 50, Themes.ProgBar[1], new int[] { 1, 1, 1, 1 });
        }

        public static void DrawTXT(int X1, int Y1, string TXT, Color C)
        {
            EXTCanvas.DrawBitFontString(Assets.AssetList.Font0, C, TXT, X1, Y1);
        }

        public static int TextXCenter(int leters)
        {
            return (Width / 2) - (BitFont.RegisteredBitFont["Comfortaa"].Size / leters);
        }
    }
}
