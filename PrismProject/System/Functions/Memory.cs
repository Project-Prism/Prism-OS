using System;
using System.Drawing;

namespace PrismProject
{
    class Memory
    {
        public static int Total = Convert.ToInt32(Cosmos.Core.CPU.GetAmountOfRAM());
        public static int Used = Convert.ToInt32(Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576;
        public static int Free = Used * 100 / Total;
        public static Elements draw = new Elements();
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;

        public static void OutOfMemoryWarning()
        {
            if (!Kernel.canvasRunning)
            {
                Driver.Init();
            }
            draw.Clear(Color.DarkOrange);
            draw.Textbox("Century_Gothic", "Out of memory!", Color.Black, Color.Red, Convert.ToInt32(screenX / 1.3), screenY / 4, 200);
            Cosmos.Core.Bootstrap.CPU.Halt();
        }
    }
}
