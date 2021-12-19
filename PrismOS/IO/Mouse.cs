using Cosmos.System;

namespace PrismOS.IO
{
    public class Mouse
    {
        public Mouse(int Width, int Height)
        {
            MouseManager.ScreenWidth = (uint)Width;
            MouseManager.ScreenHeight = (uint)Height;
        }

        public static bool IsWithin(int X1, int Y1, int X2, int Y2)
        {
            return MouseX > X1 && MouseX < X2 && MouseY > Y1 && MouseY < Y2;
        }

        public static bool IsClicked()
        {
            return MouseManager.MouseState == MouseState.Left || MouseManager.MouseState == MouseState.Right;
        }

        public static int MouseX { get => (int)MouseManager.X; }
        public static int MouseY { get => (int)MouseManager.Y; }
    }
}
