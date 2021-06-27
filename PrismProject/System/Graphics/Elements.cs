using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject
{
    class Elements
    {
        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Canvas Draw = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();

        public void Box(Color color, int fromX, int fromY, int Width, int Height) { Draw.DrawFilledRectangle(new Pen(color), fromX, fromY, Width, Height); }
        public void Circle(Color color, int fromX, int fromY, int radius) { Draw.DrawFilledCircle(new Pen(color), fromX, fromY, radius); }
        public void Triangle(Color color, int x1, int y1, int x2, int y2, int x3, int y3) { Draw.DrawTriangle(new Pen(color), x1, y1, x2, y2, x3, y3); }
        public void Loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Box(Color.DarkGray, Convert.ToInt32(fromX), Convert.ToInt32(fromY), length, height);
            Box(Color.White, Convert.ToInt32(fromX), Convert.ToInt32(fromY), percentage, height);
        }
        public void Clear(Color color) { Draw.Clear(color); }
        public static void Exit()
        {
            if (Kernel.canvasRunning)
            {
                Kernel.canvasRunning = false;
                Draw.Disable();
            }
        }
    }
}
