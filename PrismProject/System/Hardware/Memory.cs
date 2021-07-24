using System.Drawing;

namespace PrismProject
{
    internal class Memory
    {
        public static uint Total { get => Cosmos.Core.CPU.GetAmountOfRAM(); }
        public static uint Used { get => (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576; }
        public static uint Free { get => Total - Used; }
        public static uint Used_percent { get => (Used * 100) / Total; }
        public static uint Free_percent { get => 100 - Used_percent; }

        public static void OutOfMemoryWarning()
        {
            G_lib draw = new G_lib();
            int screenX = Driver.screenX;
            int screenY = Driver.screenY;
            if (!Kernel.canvasRunning)
            {
                Driver.Init();
            }
            Driver.Clear(Color.DarkOrange);
            draw.Textbox(Driver.font, "LOW MEMORY!", Themes.Textbox.TB_Border, Themes.Textbox.YB_Inner, Color.Red, screenX / 4, screenY / 2, 200);
        }

        public static void Memcheck()
        {
            while (Kernel.Running)
            {
                if (Memory.Free < 100)
                {
                    Memory.OutOfMemoryWarning();
                    Cosmos.Core.Bootstrap.CPU.Halt();
                }
            }
        }
    }
}