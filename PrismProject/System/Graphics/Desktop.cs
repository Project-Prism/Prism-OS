using System;
using System.Drawing;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Appbar = Color.FromArgb(0,120,212);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(0,120,212);
        public static Color Button = Color.White;
        public static Color Background = Color.FromArgb(40,40,40);
        public static Color Text = Color.White;

        //Define the graphics method

        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static Elements draw = new Elements();
        private static Cursor cursor = new Cursor();

        public static void Start()
        {
            Driver.Init();
            int Prg = 0;
            while (Prg != screenX / 2)
            {
                Prg++;
                draw.Loadbar(screenX / 4, Convert.ToInt32(screenY / 1.5), screenX / 2, screenY / 90, Prg);
            }
            Tools.Sleep(2);
            draw.Clear(Background);
            while (Kernel.canvasRunning)
            {
                if (Memory.Free < 100)
                {
                    Memory.OutOfMemoryWarning();
                    Cosmos.Core.Bootstrap.CPU.Halt();
                }
                draw.Box(Appbar, 0, screenY - 30, screenX, 30);
                draw.Circle(Button, 20, screenY - 15, 10);
                draw.Text(Driver.font, "Used memory: " + Memory.Used + " MB (" + Memory.Used_percent + "%)", Text, 0, 0);
                draw.Text(Driver.font, "Total memory: " + Memory.Total + " MB", Text, 0, 15);
                draw.Text(Driver.font, "Free memory: " + Memory.Free + " MB (" + Memory.Free_percent + "%)", Text, 0, 30);
                draw.Window(Driver.font, screenX/4,screenY/4,screenX/2,screenX/2, "Empty Window");
                cursor.Update();
            }
        }
        public static void Start_rec()
        {
            Driver.Init();
            draw.Clear(Color.Red);
            while (Kernel.canvasRunning)
            {
                draw.Window(Driver.font, screenX / 4, screenY / 4, screenX / 2, screenX / 2, "SYSTEM FAILURE");
                draw.Text(Driver.font, "System crashed! Unrecoverable error occured.", Text, screenX/4+75, screenY/4+75);
                cursor.Update();
            }
        }
    }
}
