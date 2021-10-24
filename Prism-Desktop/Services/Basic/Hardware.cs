using Prism.Graphics;
using Cosmos.System;
using System.Drawing;
using Cosmos.System.Graphics;

namespace Prism.Services.Basic
{
    class Keyboard_Service
    {

    }
    class Mouse_Service
    {
        public Mouse_Service()
        {
            MouseManager.ScreenWidth = (uint)Canvas2.Width;
            MouseManager.ScreenHeight = (uint)Canvas2.Height;
        }
        public static int MouseX;
        public static int Mousey;
        public static bool IsLeftClicked { get; } = MouseManager.MouseState == MouseState.Left;
        public static bool IsRightClicked { get; } = MouseManager.MouseState == MouseState.Right;

        public static void TickForward()
        {
            MouseX = (int)MouseManager.X;
            Mousey = (int)MouseManager.Y;
            //Basic.DrawBMP(MouseX, Mousey, Resources.Mouse, AnchorPoint.TopLeft);
            Canvas2.Screen.DrawFilledRectangle(new Pen(Color.White), MouseX, Mousey, 16, 16);
        }

    }
}
