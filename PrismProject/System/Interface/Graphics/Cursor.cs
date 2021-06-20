using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrismProject
{
    class Cursor
    {
        public static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }
        public static Color Mouse_color = Color.White;

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)Drawing.screenX;
            MouseManager.ScreenHeight = (uint)Drawing.screenY;
        }

        public void Draw()
        {
            int x = X;
            int y = Y;
            Drawing.canvas.DrawFilledRectangle(new Pen(Interface.Background), lastX, lastY, 10, 10);
            Drawing.canvas.DrawFilledRectangle(new Pen(Mouse_color), x, y, 10, 10);
            lastX = x;
            lastY = y;
            
        }
    }
}
