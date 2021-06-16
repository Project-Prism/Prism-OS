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
        #region GUI stuff
        //define canvas
        public static Canvas canvas;
        //init mouse service
        private static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }
        private static Color Mouse_color = Color.White;
        //set fallback screen resolution if no resoultion is available.
        public static int screenX = 1024;
        public static int screenY = 768;
        //system theme colors
        public static Color Background = Color.CornflowerBlue;
        public static Color Appbar = Color.FromArgb(24, 24, 24);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(30, 30, 30);
        public static Color Button = Color.HotPink;
        #endregion
        //methods for drawing basic shapes and system components.
        public static void Mouse_init()
        {
            MouseManager.ScreenWidth = (uint)Graphics.screenX;
            MouseManager.ScreenHeight = (uint)Graphics.screenY;
        }
        public static void Draw_mouse()
        {
            int x = X;
            int y = Y;
            Graphics.canvas.DrawFilledRectangle(new Pen(Background), lastX, lastY, 10, 10);
            Graphics.canvas.DrawFilledRectangle(new Pen(Mouse_color), x, y, 10, 10);
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
        public static void Draw_loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Draw_box(Color.DarkGray, Convert.ToInt32(fromX), Convert.ToInt32(fromY), length, height);
            Draw_box(Color.White, Convert.ToInt32(fromX), Convert.ToInt32(fromY), percentage, height);
        }
        public static void Clear(Color color)
        {
            canvas.Clear(color);
        }
        public static void Exit()
        {
            if (Kernel.enabled)
            {
                Kernel.enabled = false;
                canvas.Disable();
                PCScreenFont screenFont = PCScreenFont.Default;
                VGAScreen.SetFont(screenFont.CreateVGAFont(), screenFont.Height);
            }
        }
        public static void Start_canvas()
        {
            Kernel.enabled = true;
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            Clear(Color.Black);
            int Prg = 0;
            while (Prg != 345)
            {
                Prg++;
                Prg++;
                Prg++;
                Prg++;
                Prg++;
                Boot_screen(Prg);
            }
            Tools.Sleep(3);
            Clear(Color.Black);
            Desktop();
            //time since main services started
            int Runtime = 0;
            while (Kernel.enabled == true)
            {
                Runtime++;
            }
        }
        //boot screen, lock screen, and desktop.
        public static void Boot_screen(int Prog)
        {
            Draw_loadbar(Convert.ToInt32(screenX/3.1), Convert.ToInt32(screenY/1.3), screenX/3, screenY/50, Prog);
        }
        public static void Desktop()
        {
            Clear(Background);
            Mouse_init();
            while (Kernel.enabled)
            {
                Draw_box(Appbar, 0, screenY - 30, screenX, 30);
                Draw_circle(Button, 20, screenY - 15, 10);
                Draw_box(Window, screenX / 3, screenY / 4, screenX / 4, screenY / 4);
                Draw_box(Windowbar, screenX / 3, screenY / 4, screenX / 4, screenY / 25);
                Draw_mouse();
            }
        }
    }
}