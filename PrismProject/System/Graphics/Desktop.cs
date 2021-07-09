using System.Drawing;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Appbar = Color.FromArgb(0, 120, 212);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(0, 120, 212);
        public static Color Button = Color.White;
        public static Color Background = Color.FromArgb(40, 40, 40);
        public static Color Text = Color.White;

        //Define the graphics method

        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static Elements draw = new Elements();
        private static Cursor cursor = new Cursor();

        public static void Start()
        {
            Driver.Init();
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
                draw.Rounded_Box(Color.Violet, 15, 15, 250, 45);
                draw.Text(Text, Driver.font, "Used memory: " + Memory.Used + " MB (" + Memory.Used_percent + "%)", 15, 15);
                draw.Text(Text, Driver.font, "Total memory: " + Memory.Total + " MB", 15, 30);
                draw.Text(Text, Driver.font, "Free memory: " + Memory.Free + " MB (" + Memory.Free_percent + "%)", 15, 45);
                draw.Window(Driver.font, screenX / 4, screenY / 4, screenX / 2, screenX / 2, "Empty Window");
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
                draw.Text(Text, Driver.font, "System crashed! Unrecoverable error occured.", screenX / 4 + 75, screenY / 4 + 75);
                cursor.Update();
            }
        }
    }
}
