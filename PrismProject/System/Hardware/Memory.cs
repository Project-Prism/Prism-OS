using System.Drawing;

namespace PrismProject
{
    class Memory
    {
        public static uint Total { get => Cosmos.Core.CPU.GetAmountOfRAM(); }
        public static uint Used { get => (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576; }
        public static uint Free { get => Total - Used; }
        public static uint Used_percent { get => (Used * 100) / Total; }
        public static uint Free_percent { get => 100 - Used_percent; }

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
        }
    }
}
