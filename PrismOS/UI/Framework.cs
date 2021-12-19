using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace PrismOS.UI
{
    public static class Framework
    {
        public static bool MouseIsWithin(int X1, int Y1, int X2, int Y2)
        {
            return MouseX > X1 && MouseX < X2 && MouseY > Y1 && MouseY < Y2;
        }

        public static bool IsClicked()
        {
            return MouseManager.MouseState == MouseState.Left || MouseManager.MouseState == MouseState.Right;
        }

        #region Configuration
        public static int Width { get; set; } = 1280;
        public static int Height { get; set; } = 720;
        public static int MouseX { get => (int)MouseManager.X; }
        public static int MouseY { get => (int)MouseManager.Y; }
        public static Mode Mode { get => new(Width, Height, ColorDepth.ColorDepth32); }
        public static Canvas Canvas { get => FullScreenCanvas.GetFullScreenCanvas(Mode); }
        #endregion Configuration

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
    }
}