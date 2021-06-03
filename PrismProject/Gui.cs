using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject
{
    public class Gui
    {
        #region theming and variables
        public static Canvas canvas;

        public static Pen taskbar_primary = new Pen(Color.DarkSlateGray);
        public static Pen taskbar_secondary = new Pen(Color.White);
        public static Pen menubtn = new Pen(Color.DarkTurquoise);
        public static Pen main_theme = new Pen(Color.White);
        public static Pen main_theme_title = new Pen(Color.DarkSlateGray);
        public static Pen main_theme_text = new Pen(Color.Black);

        public static Color backColor = Color.CornflowerBlue;

        public static int screenX = 600;
        public static int screenY = 800;

        public static int dwidth = screenX / 3;
        public static int dheight = screenY / 4;
        public static int xlocation = screenX / 4;
        public static int ylocation = screenY / 4;
        #endregion

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(taskbar_primary, 0, screenY - 30, screenX, 30);
        }

        public static void draw_menubtn()
        {
            canvas.DrawFilledCircle(menubtn, 20, screenY - 15, 10);
        }

        public static void draw_dialog()
        {
            
            canvas.DrawFilledRectangle(main_theme, xlocation, ylocation, dwidth, dheight);
            canvas.DrawFilledRectangle(main_theme_title, xlocation, ylocation, dwidth, dheight);
        }

        public static bool check_click(int x, int y, int width, int height)
        {
            return Mouse.X <= x + width && Mouse.X >= x
                && Mouse.Y <= y + height && Mouse.Y >= y
                    && MouseManager.MouseState == MouseState.Left;
        }

        public static void enable()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            Console.Clear();
            Mouse.start();
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                if (check_click(0, screenY - 30, 30, 30))
                    disable();
                canvas.Clear(backColor);
                Mouse.draw();
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

    }
}