using Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismOS.Libraries
{
    public static class Extras
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

        public static class Colorizer
        {
            public static void Load(int[] Theme)
            {
                Button.Background = Color.FromArgb(Theme[1]);
                Button.Foreground = Color.FromArgb(Theme[2]);
                Button.Text = Color.FromArgb(Theme[3]);

                Label.Text = Color.FromArgb(Theme[4]);
            }

            public static class Window
            {
                public static Color Main { get; set; } = Color.White;
            }
            public static class Button
            {
                public static Color Background { get; set; } = Color.FromArgb(25, 25, 45);
                public static Color Foreground { get; set; } = Color.Yellow;
                public static Color Text { get; set; } = Color.White;
            }
            public static class Label
            {
                public static Color Text { get; set; } = Color.Black;
            }
        }
    }
}