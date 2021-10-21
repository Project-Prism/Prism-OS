using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject.Graphics
{
    class Canvas2
    {
        public static int Width = 800;
        public static int Height = 600;
        public static Canvas Screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));
        
        public static void DrawAngle(int X, int Y, int Angle, int Radius, Color color)
        {
            Double angleX, angleY;
            angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
            angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);
            Screen.DrawLine(new Pen(color), X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100));
        }

        public static void DrawRoundRect(int X1, int Y1, int X2, int Y2, int R, Color C)
        {
            int x2 = X1 + X2, y2 = Y1 + Y2, r2 = R + R;
            Screen.DrawFilledRectangle(new Pen(C), X1, Y1 + R, X2, Y2 - r2); // Left Right
            Screen.DrawFilledRectangle(new Pen(C), X1 + R, Y1, X2 - r2, R); //Bottom Bar
            Screen.DrawFilledRectangle(new Pen(C), X1 + R, y2 - R, X2 - r2, R); //Top Bar

            Screen.DrawFilledCircle(new Pen(C), X1 + R, Y1 + R, R); // Top left
            Screen.DrawFilledCircle(new Pen(C), x2 - R - 1, Y1 + R, R); // Top Right
            Screen.DrawFilledCircle(new Pen(C), 1 + R, y2 - R - 1, R); // Bottom Left
            Screen.DrawFilledCircle(new Pen(C), x2 - R - 1, y2 - R - 1, R);
        } // Bottom Right
    }
}
