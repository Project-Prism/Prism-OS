using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;

namespace PrismProject
{
    class Drawing
    {

        public static int screenX = 1024;
        public static int screenY = 768;
        public static VBECanvas canvas = new VBECanvas(new Mode(screenX, screenY, ColorDepth.ColorDepth32));

        public Drawing()
        {
            Kernel.canvasRunning = true;
        }

        public void Box(Color color, int fromX, int fromY, int Width, int Height) { canvas.DrawFilledRectangle(new Pen(color), fromX, fromY, Width, Height); }

        public void Circle(Color color, int fromX, int fromY, int radius) { canvas.DrawFilledCircle(new Pen(color), fromX, fromY, radius); }

        public void Triangle(Color color, int x1, int y1, int x2, int y2, int x3, int y3) { canvas.DrawTriangle(new Pen(color), x1, y1, x2, y2, x3, y3); }

        public void Loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Box(Color.DarkGray, Convert.ToInt32(fromX), Convert.ToInt32(fromY), length, height);
            Box(Color.White, Convert.ToInt32(fromX), Convert.ToInt32(fromY), percentage, height);
        }

        public void Clear(Color color) { canvas.Clear(color); }

        public static void Exit()
        {
            if (Kernel.canvasRunning)
            {
                Kernel.canvasRunning = false;
                canvas.Disable();
                PCScreenFont screenFont = PCScreenFont.Default;
            }
        }
    }
}
