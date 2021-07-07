using System;
using System.Drawing;

namespace PrismProject
{
    class Memory
    {
        public static uint Total = Cosmos.Core.CPU.GetAmountOfRAM();
        public static uint Used = Cosmos.Core.CPU.GetEndOfKernel() + 1024 / 1048576;
        public static uint Free = Total - Used;

        public static void OutOfMemoryWarning()
        {
            Elements draw = new Elements();
            int screenX = Driver.screenX;
            int screenY = Driver.screenY;
            if (!Kernel.canvasRunning)
            {
                Driver.Init();
            }
            draw.Clear(Color.DarkOrange);
            draw.Textbox(Driver.font, "Out of memory!", Color.Black, Color.Red, Convert.ToInt32(screenX / 1.3), screenY / 4, 200);
            Cosmos.Core.Bootstrap.CPU.Halt();
        }
    }
}
