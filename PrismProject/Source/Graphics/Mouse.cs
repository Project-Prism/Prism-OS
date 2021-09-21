using static PrismProject.Source.Graphics.ExtendedCanvas;
using static PrismProject.Source.Assets.AssetList;
using Cosmos.System;

namespace PrismProject.Source.Graphics
{
    class Mouse
    {
        private static readonly int X = (int)MouseManager.X;
        private static readonly int Y = (int)MouseManager.Y;
        public static void Update()
        {
            DrawBMP(X, Y, BootLogo);
        }
    }
}
