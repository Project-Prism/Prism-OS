using Cosmos.System;
using System.Drawing;

namespace PrismOS.UI
{
    public static class Framework
    {
        public class Theme
        {
            public Theme(int[] Theme)
            {
                BackGround = Color.FromArgb(Theme[0]);
                ForeGround = Color.FromArgb(Theme[1]);
                Text = Color.FromArgb(Theme[2]);
            }

            public Theme(byte[] Theme)
            {
                BackGround = Color.FromArgb(Theme[0]);
                ForeGround = Color.FromArgb(Theme[1]);
                Text = Color.FromArgb(Theme[2]);
            }

            public Color ForeGround { get; set; }
            public Color BackGround { get; set; }
            public Color Text { get; set; }
        }

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
}