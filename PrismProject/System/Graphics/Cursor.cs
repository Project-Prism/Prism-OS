using Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismProject
{
    class Cursor
    {
        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Canvas Draw = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();

        //mouse color and setup
        public static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }
        public static Color Mouse_color = Color.White;

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)Elements.screenX;
            MouseManager.ScreenHeight = (uint)Elements.screenY;
        }

        public void Update()
        {
            Draw.DrawFilledRectangle(new Pen(Desktop.Background), lastX, lastY, 10, 10);
            Draw.DrawFilledRectangle(new Pen(Mouse_color), X, Y, 10, 10);
            lastX = X;
            lastY = Y;
            
        }
    }
}
