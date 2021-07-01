using Cosmos.System;
using System.Drawing;

namespace PrismProject
{
    class Cursor
    {
        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Elements draw = new Elements();

        //mouse color and setup
        public static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }
        public static Color Mouse_color = Color.White;
        public static Color Mouse_back = Color.Black;

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)Elements.screenX;
            MouseManager.ScreenHeight = (uint)Elements.screenY;
        }

        public void Update()
        {
            draw.Box(Desktop.Background, lastX, lastY, 11, 11);
            draw.Box(Mouse_color, X - 1, Y - 1, 11, 11);
            draw.Box(Mouse_color, X, Y, 10, 10);
            lastX = X;
            lastY = Y;
            
        }
    }
}
