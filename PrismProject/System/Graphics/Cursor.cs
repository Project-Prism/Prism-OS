using Cosmos.System;
using System;

namespace PrismProject
{
    class Cursor
    {
        //Define the graphics method
        private static int screenX = Driver.screenX;
        private static int screenY = Driver.screenY;
        private static drawable draw = new drawable();

        //mouse color and setup
        public static int lastX, lastY;
        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }

        public static MouseState State { get => MouseManager.MouseState; }

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)screenX;
            MouseManager.ScreenHeight = (uint)screenY;
        }

        public void Update()
        {
            if (X != lastX & Y != lastY) { draw.Box(Desktop.Background, Math.Max(0, lastX - 8), Math.Max(0, lastY - 8), 32, 32); }
            draw.Image(Images.mouse, X, Y);
            lastX = X;
            lastY = Y;
        }
    }
}
