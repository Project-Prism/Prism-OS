using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismProject
{
    class Driver
    {
        public static int screenX = 320;
        public static int screenY = 200;
        public static ColorDepth colors = ColorDepth.ColorDepth8;
        private static Color[] pixelBuffer = new Color[(screenX * screenY) + screenX];
        private static Color[] pixelBufferOld = new Color[(screenX * screenY) + screenX];
        public static Canvas canvas = FullScreenCanvas.GetFullScreenCanvas();

        public static void Init()
        {
            canvas.Mode = new Mode(screenX, screenY, colors);
            canvas.Display();
            canvas.Clear();
        }
        public static void setPixel(int x, int y, Color c)
        {
            if (x > screenX || y > screenY) return;
            pixelBuffer[(x * y) + x] = c;
        }
        public static void drawScreen()
        {
            Pen pen = new Pen(Color.Orange);
            for (int y = 0, h = screenY; y < h; y++)
            {
                for (int x = 0, w = screenX; x < w; x++)
                {
                    if (!(pixelBuffer[(y * x) + x] == pixelBufferOld[(y * y) + x]))
                    {
                        pen.Color = pixelBuffer[(y * screenX) + x];
                        canvas.DrawPoint(pen, x, y);
                    }
                }
            }
            for (int i = 0, len = pixelBuffer.Length; i < len; i++)
            {
                pixelBuffer[i] = pixelBufferOld[i];
            }
        }
        public static void clearScreen(Color c)
        {
            for (int i = 0, len = pixelBuffer.Length; i < len; i++)
            {
                pixelBuffer[i] = c;
            }
        }
        public static void update()
        {
            clearScreen(Color.Blue);
            setPixel(1, 1, Color.Black);
            setPixel(1, 2, Color.Black);
            setPixel(2, 1, Color.Black);
            setPixel(2, 2, Color.Black);
            drawScreen();
        }
    }
}
