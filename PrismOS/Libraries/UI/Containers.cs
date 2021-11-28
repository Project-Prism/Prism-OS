using System.Collections.Generic;
using System.Drawing;
using Cosmos.System.Graphics;

namespace PrismOS.Libraries.UI
{
    public static class Containers
    {
        public class Window
        {
            public int X, Y, Width, Height, BorderRadius, BorderThickness;
            public List<Components.Component> Children = new();
            public bool IsVisible, IsFullScreen;

            public Window(int aX, int aY, int aWidth, int aHeight, int aBorderRadius, int aBorderThickness, bool aIsFullScreen)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                BorderRadius = aBorderRadius;
                BorderThickness = aBorderThickness;
                IsFullScreen = aIsFullScreen;
            }

            public void Draw()
            {
                if(IsFullScreen)
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.Black),0, 0, Extras.Width, Extras.Height);
                }
                else
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.White), X, Y, Width, Height);
                }

                foreach(Components.Component Comp in Children)
                {
                    Comp.Draw();
                }
            }
        }
    }
}
