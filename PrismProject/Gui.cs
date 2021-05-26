using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject
{
    public class Gui
    {
        //theming and variables
        public static Canvas canvas;
        public static int screenX = 800;
        public static int screenY = 400;

        public static Pen taskbar = new Pen(Color.DarkSlateGray);
        public static Pen menubtn = new Pen(Color.Red);
        public static Pen main_theme = new Pen(Color.White);
        public static Pen main_theme_title = new Pen(Color.DarkSlateGray);
        public static Pen main_theme_text = new Pen(Color.Black);

        public static Color backColor = Color.CornflowerBlue;

        public static void enable()
        {
            Console.Clear();
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenY, screenX, ColorDepth.ColorDepth32);
            Mouse.start();
            //only update when the mouse moves, needs to be changed in the future, but should work for now.
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                if (check_click(0, screenY - 30, 30, 30))
                    disable();
                canvas.Clear(backColor);
                draw_mouse();
                draw_dialog();
                draw_taskbar();
                draw_menubtn();
            }
        }

        public static void disable()
        {
            if (Kernel.enabled)
            {
                Kernel.enabled = false;

                canvas.Disable();
                PCScreenFont screenFont = PCScreenFont.Default;
                VGAScreen.SetFont(screenFont.CreateVGAFont(), screenFont.Height);
            }
        }

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(taskbar, 0, screenY - 30, screenX, 30);
        }

        public static void draw_menubtn()
        {
            canvas.DrawFilledCircle(menubtn, 20, screenY - 15, 10);
        }

        public static void draw_dialog()
        {
            canvas.DrawFilledRectangle(main_theme, screenX / 4, screenY / 4, screenX / 4, screenY / 4);
            canvas.DrawFilledRectangle(main_theme_title, screenX / 4, screenY / 4, screenX / 4, screenY / 20);
        }

        public static void draw_mouse()
        {
            Mouse.draw();
        }

        public static bool check_click(int x, int y, int width, int height)
        {
            return Mouse.X <= x + width && Mouse.X >= x
                && Mouse.Y <= y + height && Mouse.Y >= y
                    && MouseManager.MouseState == MouseState.Left;
        }
    }
}