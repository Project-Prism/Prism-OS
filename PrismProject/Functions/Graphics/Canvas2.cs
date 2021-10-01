using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Drawing;
using static Cosmos.System.Graphics.ColorDepth;
using static PrismProject.Functions.IO.SystemFiles;
using static PrismProject.Functions.Graphics.ThemeSystem;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject.Functions.Graphics
{
    class Canvas2
    {
        public static int Width = 800;
        public static int Height = 600;
        public static Canvas Canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth32));

        public class Shapes
        {
            public static void DrawRect(int X1, int Y1, int X2, int Y2, Color C, bool Filled)
            {
                switch (Filled)
                {
                    case true:
                        Canvas.DrawFilledRectangle(new Pen(C), X1, Y1, X2, Y2);
                        break;
                    case false:
                        Canvas.DrawRectangle(new Pen(C), X1, Y1, X2, Y2);
                        break;
                }
            }

            public static void DrawRoundRect(int X1, int Y1, int X2, int Y2, int R, Color C, int[] Sides)
            {
                int x2 = X1 + X2, y2 = Y1 + Y2, r2 = R + R;
                DrawRect(X1, Y1 + R, X2, Y2 - r2, C, true); // Left Right
                DrawRect(X1 + R, Y1, X2 - r2, R, C, true); //Bottom Bar
                DrawRect(X1 + R, y2 - R, X2 - r2, R, C, true); //Top Bar

                if (Sides[0] == 1)
                { DrawCirc(X1 + R, Y1 + R, R, C, true); }
                else { } //Top Left
                if (Sides[1] == 1)
                { DrawCirc(x2 - R - 1, Y1 + R, R, C, true); }
                else { } //Top Right
                if (Sides[2] == 1)
                { DrawCirc(X1 + R, y2 - R - 1, R, C, true); }
                else { } //Bottom Left
                if (Sides[3] == 1)
                { DrawCirc(x2 - R - 1, y2 - R - 1, R, C, true); }
                else { } //Bottom Right
            }

            public static void DrawCirc(int X1, int Y1, int R, Color C, bool filled)
            {
                switch (filled)
                {
                    case true:
                        //Canvas.DrawFilledCircle(new Pen(C), X1, Y1, R);
                        break;
                    case false:
                        Canvas.DrawCircle(new Pen(C), X1, Y1, R);
                        break;
                }
            }

            public static void DrawLine(int X1, int Y1, int X2, int Y2, Color C)
            {
                Canvas.DrawLine(new Pen(C), X1, Y1, X2, Y2);
            }

            public static void DrawAngle(int X, int Y, int Angle, int Radius, Color color)
            {
                Double angleX, angleY;
                angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
                angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);
                Shapes.DrawLine(X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100), color);
            }
        }

        public class Advanced
        {
            public static void DrawBMP(int X1, int Y1, Bitmap BMP)
            {
                //Canvas.DrawImageAlpha(BMP, X1, Y1);
            }

            public static void DrawPage(int X1, int Y1, int X2, int Y2, int R, string title)
            {
                int TextX = (X2 / 2) - (PCScreenFont.Default.Width * title.Length);
                Shapes.DrawRoundRect(X1 - 1, Y1 - 1, X2 + 2, Y2 + 2 + (Y2 / 15), R, Window[0], new int[] { 1, 1, 1, 1 }); //shadow
                Shapes.DrawRoundRect(X1, Y1 + (Y2 / 15), X2, Y2, R, Window[2], new int[] { 1, 1, 1, 1 }); //window
                Shapes.DrawRoundRect(X1, Y1, X2, Y2 / 15, R, Window[3], new int[] { 1, 1, 1, 1 }); //title bar
                DrawTXT(TextX, Y2 / 15 + 5, title, Color.White);
            }

            public static void DrawProgBar(int X1, int Y1, int X2, int Y2, int Percent)
            {
                Shapes.DrawRoundRect(X1, Y1, X2, Y2, 50, Progbar[0], new int[] { 1, 1, 1, 1 });
                Shapes.DrawRoundRect(X1, Y1, X2 / Percent, Y2, 50, Progbar[1], new int[] { 1, 1, 1, 1 });
            }

            public static void DrawTXT(int X1, int Y1, string TXT, Color C)
            {
                //Canvas.DrawString(TXT, PCScreenFont.Default, new Pen(C), X1, Y1);
            }

            public static void Reload()
            {
                Canvas.Disable();
                Canvas.Display();
            }
        }

        public class Mouse
        {
            public static void UpdateMouse()
            {
                Advanced.DrawBMP((int)MouseManager.X, (int)MouseManager.Y, Arrow_bmp);
            }
        }
    }
}
