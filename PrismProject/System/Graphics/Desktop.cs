using Cosmos.System.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Appbar = Color.FromArgb(24, 24, 24);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(30, 30, 30);
        public static Color Button = Color.HotPink;
        public static Color Background = Color.CornflowerBlue;
        public static Color Text = Color.White;

        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Canvas Draw = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();

        public static void Start()
        {
            Driver.Init();
            int Prg = 0;
            while (Prg != screenY/2.5)
            {
                //1 percent is 3.4
                Prg++;
                draw.Loadbar(screenX/4, Convert.ToInt32(screenY /1.6), Convert.ToInt32(screenX /1.6), screenY/65, Prg);
            }
            Tools.Sleep(2);
            Draw.Clear(Background);
            while (Kernel.canvasRunning)
            {
                draw.Box(Appbar, 0, screenY - 30, screenX, 30);
                draw.Circle(Button, 20, screenY - 15, 10);
                draw.Box(Window, screenX / 3, screenY / 4, screenX / 4, screenY / 4);
                draw.Box(Windowbar, screenX / 3, screenY / 4, screenX / 4, screenY / 25);
                cursor.Update();
            }
        }
    }
}
