using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismOS.UI.Overlays
{
    public static class SystemInfo
    {
        public static void Tick()
        {
            Canvas.GetCanvas.DrawFilledRectangle(15, 15, 500, 95, Color.FromArgb(100, 25, 25, 25));
            Canvas.GetCanvas.DrawString(265, 25, Default, "FPS: " + Canvas.GetCanvas.FPS, Color.White);
            Canvas.GetCanvas.DrawString(265, 45, Default, "CPU: " + Cosmos.Core.CPU.GetCPUBrandString(), Color.White);
            Canvas.GetCanvas.DrawString(265, 65, Default, "RAM: " + Cosmos.Core.CPU.GetAmountOfRAM(), Color.White);
            Canvas.GetCanvas.DrawString(265, 85, Default, "Cycles: " + Cosmos.Core.CPU.GetCPUUptime(), Color.White);
        }
    }
}
