using System.Drawing;

namespace PrismProject
{
    class Lock
    {
            //Default theme colors
            public static Color Window = Color.White;
            public static Color Windowbar = Color.FromArgb(0, 120, 212);
            public static Color Background = Color.FromArgb(40, 40, 40);
            public static Color Text = Color.White;

            //Define the graphics method
            private static int screenX = Driver.screenX, screenY = Driver.screenY;
            private static drawable draw = new drawable();
            private static Cursor cursor = new Cursor();

        public static void Start()
        {
            
            draw.Clear(Background);
            while (Kernel.canvasRunning)
            {
                draw.Window(Driver.font, screenX / 4, screenY / 4, screenX / 2, screenY / 2, "Login window", true);
                cursor.Update();
            }
        }
    }

}