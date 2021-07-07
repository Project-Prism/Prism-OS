using Cosmos.System;
using System.Drawing;

namespace PrismProject
{
    class Cursor
    {
        //Define the graphics method
        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static Elements draw = new Elements();

        //mouse color and setup
        private static int lastX, lastY;
        private static int X = (int)MouseManager.X;
        private static int Y = (int)MouseManager.Y;
        public static Color Mouse_color = Color.White;
        public static Color Mouse_back = Color.Black;

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)screenX;
            MouseManager.ScreenHeight = (uint)screenY;
        }

        public void Update()
        {
            //draw.Box(Desktop.Background, lastX, lastY, 10, 10);
            draw.Text("SegoeUI", "🡬", Desktop.Background, lastX, lastY);
            draw.Text("SegoeUI", "🡬", Mouse_color, X, Y);
            //draw.Box(Mouse_back, X, Y, 8, 8);
            //draw.Box(Mouse_color, X + 1, Y + 1, 6, 6);
            lastX = X;
            lastY = Y;
        }
    }
}
