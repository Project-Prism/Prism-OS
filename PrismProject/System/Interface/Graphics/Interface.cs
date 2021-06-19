using System;
using System.Drawing;

namespace PrismProject
{
    class Interface
    {
        public static Color Appbar = Color.FromArgb(24, 24, 24);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(30, 30, 30);
        public static Color Button = Color.HotPink;
        public static Color Background = Color.CornflowerBlue;

        public static void Start()
        {
            Drawing draw = new Drawing();
            Cursor cursor = new Cursor();
            //one percent for the load bar is 3.4
            draw.Clear(Color.Black);
            int Prg = 0;
            while (Prg != 340)
            {
                Prg++;
                Prg++;
                Prg++;
                Prg++;
                Prg++;
                draw.Loadbar(Convert.ToInt32(Drawing.screenX / 3.1), Convert.ToInt32(Drawing.screenY / 1.3), Drawing.screenX / 3, Drawing.screenY / 65, Prg);
            }
            Tools.Sleep(3);
            draw.Clear(Color.Black);
            draw.Clear(Background);
            
            while (Kernel.canvasRunning)
            {
                draw.Box(Appbar, 0, Drawing.screenY - 30, Drawing.screenX, 30);
                draw.Circle(Button, 20, Drawing.screenY - 15, 10);
                draw.Box(Window, Drawing.screenX / 3, Drawing.screenY / 4, Drawing.screenX / 4, Drawing.screenY / 4);
                draw.Box(Windowbar, Drawing.screenX / 3, Drawing.screenY / 4, Drawing.screenX / 4, Drawing.screenY / 25);
                cursor.Draw();
            }
        }
    }
}
