using Cosmos.System;
using Cosmos.System.Graphics;

namespace Prism.Libraries.UI
{
    public static class Managment
    {
        public static int Width { get; set; } = 800;
        public static int Height { get; set; } = 600;
        public static int MouseX { get; } = (int)MouseManager.X;
        public static int MouseY { get; } = (int)MouseManager.Y;
        private static Mode Mode { get; set; } = new(Width, Height, ColorDepth.ColorDepth32);
        public static Canvas Canvas { get; } = FullScreenCanvas.GetFullScreenCanvas(Mode);

        public static bool MouseIsWithin(int X1, int Y1, int X2, int Y2)
        {
            return MouseX > X1 && MouseX < X2 && MouseY > Y1 && MouseY < Y2;
        }

        public static bool IsClicked()
        {
            return MouseManager.MouseState == MouseState.Left || MouseManager.MouseState == MouseState.Right;
        }
    }
}
