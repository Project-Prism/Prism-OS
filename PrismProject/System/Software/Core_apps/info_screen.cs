using System;
using System.Drawing;

namespace PrismProject
{
    class info_screen
    {
        //Default theme colors
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(0, 120, 212);
        public static Color Background = Color.FromArgb(40, 40, 40);
        public static Color Text = Color.Black;

        //Define the graphics method
        private static int screenX = Driver.screenX, screenY = Driver.screenY, cycles = 0;
        private static drawable draw = new drawable();

        public static void Start()
        {
            draw.Clear(Background);
            while (Kernel.canvasRunning)
            {
                cycles++;
                draw.Window(Driver.font, screenX / 5, screenY / 5, Convert.ToInt32(screenX / 1.5), screenY / 2, "System information", true);
                draw.Text(Text, Driver.font, "CPU: " + Cosmos.Core.CPU.GetCPUBrandString(), Convert.ToInt32(screenX /4.8), screenY/5+25);
                draw.Text(Text, Driver.font, "CPU vendor: " + Cosmos.Core.CPU.GetCPUVendorName(), Convert.ToInt32(screenX / 4.8), screenY / 5+41);
                draw.Text(Text, Driver.font, "Canvas cycles " + cycles, Convert.ToInt32(screenX / 4.8), screenY /5+57);
                draw.Text(Text, Driver.font, "Build/codename: Prism OS " + Kernel.Codename + " (" + Kernel.Kernel_build + ")", Convert.ToInt32(screenX / 4.8), screenY / 5+73);
            }
        }
    }

}