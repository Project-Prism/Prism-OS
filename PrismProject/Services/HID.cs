using static PrismProject.Display.Visual2D.Display;
using static PrismProject.Display.Visual2D.DisplayConfig;
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
        public static int MouseX;
        public static int Mousey;
        public static bool IsLeftClicked { get; } = MouseManager.MouseState == MouseState.Left;
        public static bool IsRightClicked { get; } = MouseManager.MouseState == MouseState.Right;

        public static void TickForward()
        {
            MouseX = (int)MouseManager.X;
            Mousey = (int)MouseManager.Y;
            //Basic.DrawBMP(MouseX, Mousey, Resources.Mouse, AnchorPoint.TopLeft); Slow right now
            Basic.DrawRect(MouseX, Mousey, MouseX + 32, Mousey + 32, System.Drawing.Color.AliceBlue, true);
        }

    }
}
