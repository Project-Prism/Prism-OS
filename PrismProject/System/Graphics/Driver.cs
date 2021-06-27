using Cosmos.System.Graphics;
using System;

namespace PrismProject
{
    class Driver
    {
        public static int screenX = 1024;
        public static int screenY = 768;
        public static Canvas canvas;

        public static void Init()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            canvas.Display();
            canvas.Clear();
        }
    }
}
