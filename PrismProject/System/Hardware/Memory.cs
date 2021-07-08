using System;
using System.Drawing;

namespace PrismProject
{
    class Memory
    {
        public static uint Total = Cosmos.Core.CPU.GetAmountOfRAM();
        public static uint Used = (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576;
        public static uint Free = Total - Used;
        public static uint Used_percent = (Used * 100) / Total;
        public static uint Free_percent = 100 - Used_percent;

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
            draw.Textbox(Driver.font, "LOW MEMORY!", Color.Black, Color.Red, screenX / 4, screenY / 2, 200);
            Cosmos.Core.Bootstrap.CPU.Halt();
        }
    }
}
