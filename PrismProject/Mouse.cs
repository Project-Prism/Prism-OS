using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;

namespace PrismProject
{
    public class Mouse
    {
        private static int lastX, lastY;

        public static int X { get => (int)MouseManager.X; }

        public static int Y { get => (int)MouseManager.Y; }

        private static readonly Pen reset = new Pen(Gui.backColor);
        private static readonly Pen pen = new Pen(Color.White);

        public static void start()
        {
            MouseManager.ScreenWidth = (uint)Gui.screenX;
            MouseManager.ScreenHeight = (uint)Gui.screenY;
        }

        public static void draw()
        {
            int x = X;
            int y = Y;

            if (reset.Color != Gui.backColor)
                reset.Color = Gui.backColor;

            Gui.canvas.DrawFilledRectangle(reset, lastX, lastY, 10, 10);
            Gui.canvas.DrawFilledRectangle(pen, x, y, 10, 10);
            //Gui.canvas.DrawFilledCircle(reset, lastX, lastY, 10);
            //Gui.canvas.DrawFilledCircle(pen, x, y, 10);

            lastX = x;
            lastY = y;
        }
    }
}
