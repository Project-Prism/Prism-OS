using Cosmos.System;
using Cosmos.System.Graphics;
using PrismProject.System2.Drivers;
using System.Drawing;

namespace PrismProject.System2.Drawing
{
    /// <summary>
    /// The mouse manager.
    /// </summary>
    internal class Mouse
    {
            private static readonly int SW = Video.SW, SH = Video.SH;
            public static int lastX, lastY;
            public static int X { get => (int)MouseManager.X; }
            public static int Y { get => (int)MouseManager.Y; }
            public static MouseState State { get => MouseManager.MouseState; }

            public Mouse()
            {
                MouseManager.ScreenWidth = (uint)SW;
                MouseManager.ScreenHeight = (uint)SH;
            }

            public static void Update()
            {
            Icons.Icons.DrawIcon(X, Y, 12, 12, Color.White, "arrow");
                lastX = X;
                lastY = Y;
            }
        }
    }
