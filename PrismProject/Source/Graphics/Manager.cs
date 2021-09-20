using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject.Source.Graphics
{
    internal class CoalWM
    {
    }

    internal class Drawables
    {
        public static readonly int Width = 800;
        public static readonly int Height = 600;
        public static readonly SVGAIICanvas Screen = new SVGAIICanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));
        public static int[] UI_Set = new int[] { Width / 2, Height / 2 };

        public static int TextXCenter(int leters)
        {
            return UI_Set[0] - (BitFont.RegisteredBitFont["Comfortaa"].Size / leters);
        }

        public static void Clear(Color color)
        {
            Screen.Clear(color);
        }

        public static void DrawAngle(int X, int Y, int Angle, int Radius, Color color)
        {
            Double angleX, angleY;
            angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
            angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);
            Screen.DrawLine(new Pen(color), X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100));
        }

        public static void DrawCirc(int X, int Y, int Radius, Color color, bool filled)
        {
            switch (filled)
            {
                case true: Screen.DrawFilledCircle(new Pen(color), X, Y, Radius); break;
                case false: Screen.DrawCircle(new Pen(color), X, Y, Radius); break;
            }
        }

        public static void DrawEllipse(int X, int Y, int X2, int Y2, Color color, bool filled)
        {
            switch (filled)
            {
                case true: Screen.DrawFilledEllipse(new Pen(color), X, Y, X2, Y2); break;
                case false: Screen.DrawEllipse(new Pen(color), X, Y, X2, Y2); break;
            }
        }

        public static void DrawBMP(int X, int Y, Bitmap image)
        {
            Screen.DrawImageAlpha(image, X, Y);
        }

        public static void DrawProgBar(int X, int Y, int X2, int Y2, int Percent)
        {
            DrawRoundRect(X, Y, X2, Y2, 50, Themes.ProgBar[0], new int[] { 1, 1, 1, 1 });
            DrawRoundRect(X, Y, X2 / Percent, Y2, 50, Themes.ProgBar[1], new int[] { 1, 1, 1, 1 });
        }

        public static void DrawRect(int X, int Y, int X2, int Y2, Color color, bool filled)
        {
            switch (filled)
            {
                case true:
                    Screen.DrawFilledRectangle(new Pen(color), X, Y, X2, Y2);
                    break;
                case false:
                    Screen.DrawRectangle(new Pen(color), X, Y, X2, Y2);
                    break;
            }
        }

        public static void DrawRoundRect(int X, int Y, int X2, int Y2, int R, Color color, int[] Sides)
        {
            int x2 = X + X2, y2 = Y + Y2, r2 = R + R;
            Screen.DrawFilledRectangle(new Pen(color), X, Y + R, X2, Y2 - r2); // Left Right
            Screen.DrawFilledRectangle(new Pen(color), X + R, Y, X2 - r2, R);//Bottom Bar
            Screen.DrawFilledRectangle(new Pen(color), X + R, y2 - R, X2 - r2, R);//Top Bar

            if (Sides[0] == 1)
            { Screen.DrawFilledCircle(new Pen(color), X + R, Y + R, R); }
            else { } //Top Left
            if (Sides[1] == 1)
            { Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, Y + R, R); }
            else { } //Top Right
            if (Sides[2] == 1)
            { Screen.DrawFilledCircle(new Pen(color), X + R, y2 - R - 1, R); }
            else { } //Bottom Left
            if (Sides[3] == 1)
            { Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y2 - R - 1, R); }
            else { } //Bottom Right
        }

        public static void DrawText(int X, int Y, string str, string Font, Color color)
        {
            Screen.DrawBitFontString(Font, color, str, X, Y);
        }

        public static void DrawTriangle(int X, int Y, int X2, int Y2, int X3, int Y3, Color color)
        {
            Screen.DrawTriangle(new Pen(color), X, Y, X2, Y2, X3, Y3);
        }

        public static void DrawWindowBase(int X, int Y, int X2, int Y2, int R, Color[] Theme)
        {
            DrawRoundRect(X - 1, Y - 1, X2 + 2, Y2 + 2 + (Y2 / 20), R, Theme[4], new int[] { 1, 1, 1, 1 }); //shadow
            DrawRoundRect(X, Y + (Y2 / 20), X2, Y2, R, Theme[2], new int[] { 1, 1, 1, 1 }); //window
            DrawRoundRect(X, Y, X2, Y2 / 20, R, Theme[0], new int[] { 1, 1, 1, 1 }); //title bar
        }
    }
}