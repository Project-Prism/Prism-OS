using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;

namespace PrismProject
{
    public class Graphics
    {
        public static Canvas canvas;
        #region mouse service
        private static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }
        private static readonly Pen reset = new Pen(Graphics.Background);
        private static Pen pen = new Pen(Color.White);
        #endregion
        //set fallback screen resolution if no resoultion is available.
        public static int screenX = 1024;
        public static int screenY = 768;
        //system theme colors
        public static Color Background = Color.CornflowerBlue;
        public static Color Appbar = Color.FromArgb(24, 24, 24);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(30, 30, 30);
        public static Color Button = Color.Aquamarine;

        public static void Mouse_init()
        {
            MouseManager.ScreenWidth = (uint)Graphics.screenX;
            MouseManager.ScreenHeight = (uint)Graphics.screenY;
        }
        public static void Draw_mouse()
        {
            int x = X;
            int y = Y;

            if (reset.Color != Graphics.Background)
                reset.Color = Graphics.Background;

            Graphics.canvas.DrawFilledRectangle(reset, lastX, lastY, 10, 10);
            Graphics.canvas.DrawFilledRectangle(pen, x, y, 10, 10);

            lastX = x;
            lastY = y;
        }
        public static void Draw_box(Color color, int fromX, int fromY, int Width, int Height)
        {
            canvas.DrawFilledRectangle(new Pen(color), fromX, fromY, Width, Height);
        }
        public static void Draw_circle(Color color, int fromX, int fromY, int radius)
        {
            canvas.DrawFilledCircle(new Pen(color), fromX, fromY, radius);
        }
        public static void Draw_triangle(Color color, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            canvas.DrawTriangle(new Pen(color), x1, y1, x2, y2, x3, y3);
        }
        public static void Clear(Color color)
        {
            canvas.Clear(color);
        }
        public static void Draw_back()
        {

        }
        public static void Desktop()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            Mouse_init();
            Clear(Background);
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                Draw_box(Appbar, 0, screenY - 30, screenX, 30);
                Draw_circle(Button, 20, screenY - 15, 10);
                Draw_box(Window, screenX / 3, screenY / 4, screenX / 4, screenY / 4);
                Draw_box(Windowbar, screenX / 3, screenY / 4, screenX / 4, screenY / 25);
                Draw_mouse();
            }
        }
        public static void Stop()
        {
            if (Kernel.enabled)
            {
                Kernel.enabled = false;
                canvas.Disable();
                PCScreenFont screenFont = PCScreenFont.Default;
                VGAScreen.SetFont(screenFont.CreateVGAFont(), screenFont.Height);
            }
        }
    }
}