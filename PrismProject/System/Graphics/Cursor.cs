using Cosmos.System;
using Cosmos.System.Graphics;
using System;

namespace PrismProject
{
    internal class Cursor
    {
        private static readonly Glib_core Gcore;
        private static readonly SVGAIICanvas Function = Driver.Function;
        private static readonly int Sx = Driver.Width, Sy = Driver.Height;

        //mouse color and setup
        public static int lastX, lastY;

        public static int X { get => (int)MouseManager.X; }
        public static int Y { get => (int)MouseManager.Y; }

        public static MouseState State { get => MouseManager.MouseState; }

        public Cursor()
        {
            MouseManager.ScreenWidth = (uint)Sx;
            MouseManager.ScreenHeight = (uint)Sy;
        }

        public static void Update()
        {
            //Function.DrawFilledRectangle(new Pen(Themes.Desktop_main), Math.Max(0, lastX - 8), Math.Max(0, lastY - 8), 32, 32);
            //Function.DrawImageAlpha(Images.mouse, X, Y);
            //lastX = X;
            //lastY = Y;
            Driver.Function.SetCursor(true, X, Y);
        }
    }
}