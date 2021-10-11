using static PrismProject.Functions.Graphics.Canvas2;
using Cosmos.System;

namespace PrismProject.Functions.Services
{
    class KeyboardService
    {

    }
    class MouseService
    {
        public MouseService()
        {
            MouseManager.ScreenWidth = (uint)Width;
            MouseManager.ScreenHeight = (uint)Height;
        }
        public static int MouseX { get; } = (int)MouseManager.X;
        public static int Mousey { get; } = (int)MouseManager.Y;
        public static bool IsLeftClicked { get; } = MouseManager.MouseState == MouseState.Left;
        public static bool IsRightClicked { get; } = MouseManager.MouseState == MouseState.Right;
    }
}
