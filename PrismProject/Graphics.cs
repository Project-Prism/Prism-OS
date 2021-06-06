using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using System;

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
        public static int screenX = 600;
        public static int screenY = 800;
        //system theme colors
        public static Color Background = Color.CornflowerBlue;
        public static Color Appbar = Color.DimGray;
        public static Color Window = Color.White;
        public static Color Windowbar = Color.DimGray;
        public static Color Button = Color.DarkOliveGreen;

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
        public static void Draw_triangle(Color color, Cosmos.System.Graphics.Point p1, Cosmos.System.Graphics.Point p2, Cosmos.System.Graphics.Point p3)
        {
            canvas.DrawTriangle(new Pen(color), p1, p2, p3);
        }
        public static void Clear(Color color)
        {
            canvas.Clear(color);
        }
        public static void Demo()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            Mouse_init();
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                Clear(Background);
                Draw_box(Appbar, 0, screenY - 30, screenX, 30);
                Draw_circle(Button, 20, screenY - 15, 10);
                Draw_box(Window, screenX / 3, screenY / 4, screenX / 4, screenY / 4);
                Draw_box(Windowbar, screenX / 4, screenY / 4, screenX / 3, screenY / 4);
                Draw_mouse();
                Tools.Sleep(Convert.ToInt32("0.3"));
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