using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject
{
    public class Gui
    {
        #region Theming
        public static Canvas canvas;
        public static int screenX = 800;
        public static int screenY = 600;

        public static Pen taskbar = new Pen(Color.DarkSlateGray);
        public static Pen menubtn = new Pen(Color.Red);
        public static Pen main_theme = new Pen(Color.White);
        public static Pen main_theme_title = new Pen(Color.DarkSlateGray);

        public static Color backColor = Color.CornflowerBlue;
        #endregion

        public static void start()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            canvas.Clear(backColor);
        }

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(taskbar, 0, screenY - 30, screenX, 30);
        }

        public static void draw_menubtn()
        {
            //canvas.DrawFilledRectangle(menubtn, 0, screenY - 30, 30, 30);
            canvas.DrawFilledCircle(menubtn, 20, screenY - 15, 10);
        }

        public static bool check_click(int x, int y, int width, int height)
        {
            return Mouse.X <= x + width && Mouse.X >= x
                && Mouse.Y <= y + height && Mouse.Y >= y
                    && MouseManager.MouseState == MouseState.Left;
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

        public static void enable()
        {
            Console.Clear();
            start();
            Mouse.start();

            //only update when the mouse moves, needs to be changed in the future, but should work for now.
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                int oldx = Mouse.X;
                if (oldx != Mouse.X)
                {
                    draw_dialog();
                    draw_taskbar();
                    draw_menubtn();
                    draw_mouse();

                    if (check_click(0, screenY - 30, 30, 30))
                        Cosmos.System.Power.Reboot();
                }
            }


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
    }
}