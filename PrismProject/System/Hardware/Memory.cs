using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismProject
{
    internal class Memory
    {
        private static readonly Glib_core Gcore;
        private static readonly SVGAIICanvas Function = Driver.Function;
        private static readonly int Sx = Driver.Width, Sy = Driver.Height;
        public static uint Total { get => Cosmos.Core.CPU.GetAmountOfRAM(); }
        public static uint Used { get => (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576; }
        public static uint Free { get => Total - Used; }
        public static uint Used_percent { get => (Used * 100) / Total; }
        public static uint Free_percent { get => 100 - Used_percent; }

        public static void OutOfMemoryWarning()
        {
            if (!Kernel.canvasRunning)
            {
                Driver.Init();
            }
            Function.Clear(Color.DarkOrange);
            Gcore.Textbox(Driver.Font, "LOW MEMORY!", Themes.Textbox.TB_Border, Themes.Textbox.YB_Inner, Color.Red, Sx / 4, Sy / 2, 200);
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