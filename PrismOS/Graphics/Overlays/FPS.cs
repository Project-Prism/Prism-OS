using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismOS.Graphics.Overlays
{
    public static class FPS
    {
        public static void Draw()
        {
            Canvas.GetCanvas.DrawFilledRectangle(25, 25, Default.Width * ("FPS: " + Canvas.GetCanvas.FPS).Length, Default.Height, Color.FromArgb(100, 25, 25, 25));
            Canvas.GetCanvas.DrawString(30, 30, Default, "FPS: " + Canvas.GetCanvas.FPS, Color.White);
        }
    }
}
