using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Appbar = Color.FromArgb(75, 24, 24, 24);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(30, 30, 30);
        public static Color Button = Color.LawnGreen;
        public static Color Background = Color.CornflowerBlue;
        public static Color Text = Color.White;

        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Canvas canvas = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();

        public static void Start()
        {
            Driver.Init();
            int Prg = 0;
            while (Prg != screenX / 2)
            {
                Prg++;
                draw.Loadbar(screenX / 4, Convert.ToInt32(screenY / 1.5), screenX / 2, screenY / 70, Prg);
            }
            Tools.Sleep(2);
            draw.Clear(Color.Black);
            draw.Clear(Background);
            while (Kernel.canvasRunning)
            {
                draw.Box(Appbar, 0, screenY - 30, screenX, 30);
                draw.Circle(Button, 20, screenY - 15, 10);
                //draw.Textbox("Microsoft Sans Serif", "Used memory: " + Memory.Used + "MB", Color.Black, Color.White, 0, 0, 250);
                //draw.Textbox("Microsoft Sans Serif", "Total memory: " + Memory.Total + "MB", Color.Black, Color.White, 0, 15, 250);
                //draw.Textbox("Microsoft Sans Serif", "Free memory: " + Memory.Free + "MB", Color.Black, Color.White, 0, 30, 250);
                if (Memory.Free == 0)
                {
                    draw.Clear(Color.Beige);
                    draw.Textbox("Lucida Sans", "Out of memory!", Color.Black, Color.Red, screenX / 4, screenY / 4, 200);
                }
                //draw.Window("Lucida Sans", screenX / 4, screenY/4, screenX/2, screenX/2, "Empty Window");
                cursor.Update();
            }
        }
    }
}
