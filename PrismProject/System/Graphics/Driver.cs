using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismProject
{
    class Driver
    {
        public static int screenX = 800;
        public static int screenY = 600;
        public static ColorDepth colors = ColorDepth.ColorDepth32;
        private static Color[] pixelBuffer = new Color[(screenX * screenY) + screenX];
        private static Color[] pixelBufferOld = new Color[(screenX * screenY) + screenX];
        public static Canvas canvas = FullScreenCanvas.GetFullScreenCanvas();

        public static void Init()
        {
            canvas.Mode = new Mode(screenX, screenY, colors);
        }
    }
}
