using System.Collections.Generic;
using System.Drawing;
using static PrismOS.UI.Extras;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace PrismOS.UI
{
    public static class Framework
    {
        public class Window
        {
            public Window(int aX, int aY, int aWidth, int aHeight, int aRadius)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Radius = aRadius;
                Children = new List<Component>();
            }

            public List<Component> Children;
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public int Radius;

            public void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(new Pen(Color.DarkGray), X, Y, Width, Height);

                foreach (Component Comp in Children)
                {
                    Comp.Draw();
                }
            }
        }

        public abstract class Component
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public int Radius;
            public string Text;
            public Window Parent;

            public abstract void Draw();
        }

        public class Button : Component
        {
            public Button(int aX, int aY, int aWidth, int aHeight, int aRadius, Window aParent)
            {
                Parent = aParent;
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Radius = aRadius;
            }

            public new int X;
            public new int Y;
            public new int Width;
            public new int Height;
            public new int Radius;
            public new string Text;
            public new Window Parent;

            public override void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(
                    pen: new Pen(Colorizer.Button.Background),
                    x_start: Parent.X + (X - (Width / 2)),
                    y_start: Parent.Y + (Y - (Height / 2)),
                    width: Width,
                    height: Height);
            }
        }

        public class Label : Component
        {
            public Label(int aX, int aY, Window aParent)
            {
                Parent = aParent;
                X = aX;
                Y = aY;
            }

            public new int X;
            public new int Y;
            public new string Text;
            public new Window Parent;

            public override void Draw()
            {
                Extras.Canvas.DrawString(
                    str: "",
                    aFont: PCScreenFont.Default,
                    pen: new Pen(Color.White),
                    x: Parent.X + X - (PCScreenFont.Default.Width * Text.Length / 2),
                    y: Parent.Y + Y);
            }
        }

        public class Test : Component
        {
            public Test(int aX, int aY, int aWidth, int aHeight, int aRadius, Window aParent)
            {
                Parent = aParent;
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Radius = aRadius;
            }

            public new int X;
            public new int Y;
            public new int Width;
            public new int Height;
            public new int Radius;
            public new string Text;
            public new Window Parent;

            public override void Draw()
            {
                for (int n = X; n < Width; n++)
                {
                    Extras.Canvas.DrawPoint(new Pen(Color.White), Parent.X + n, Parent.Y + Y);
                    if (n.ToString().EndsWith("0"))
                    {
                        Extras.Canvas.DrawLine(new Pen(Color.White), Parent.X + n, Parent.Y + Y - 5, Parent.X + n, Parent.Y + Y + 5);
                        //Extras.Canvas.DrawString(n.ToString(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(Color.White), n, Y - 7);
                    }
                }
            }
        }
    }
}