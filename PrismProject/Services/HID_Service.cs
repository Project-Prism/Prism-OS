using static PrismProject.Graphics.Canvas2;
using Cosmos.System;

namespace PrismProject.Services
{
    class Keyboard_Service
    {

    }
    class Mouse_Service
    {
        public Mouse_Service()
        {
            MouseManager.ScreenWidth = (uint)Width;
            MouseManager.ScreenHeight = (uint)Height;
        }
        public static int MouseX { get; } = (int)MouseManager.X;
        public static int Mousey { get; } = (int)MouseManager.Y;
        public static bool IsLeftClicked { get; } = MouseManager.MouseState == MouseState.Left;
        public static bool IsRightClicked { get; } = MouseManager.MouseState == MouseState.Right;

        public static void TickForward()
        {
            Basic.DrawBMP(MouseX, Mousey, Resource_Service.Mouse, AnchorPoint.TopLeft);
        }
    }
}
