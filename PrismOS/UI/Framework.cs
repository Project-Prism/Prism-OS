using System.Collections.Generic;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using static PrismOS.UI.Extras;

namespace PrismOS.UI
{
    public static class Framework
    {
        public abstract class Component
        {
            public int X, Y, Width, Height, Radius;
            public string Text;
            public Bitmap Icon;
            public Component Parent;

            public abstract void Draw();
        }

        public class Window : Component
        {
            public new int X, Y, Width, Height, Radius;
            public new string Text;
            public new Bitmap Icon;
            public new Component Parent;

            #region Window specific
            public List<Component> Children = new();
            public bool IsVisible, IsFullScreen;
            #endregion Window specific

            public Window(int aX, int aY, int aWidth, int aHeight, int aRadius, string aText, Bitmap aIcon)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Radius = aRadius;
                Text = aText;
                Icon = aIcon;
                Parent = null;
            }

            public override void Draw()
            {
                if (IsFullScreen)
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.Black), 0, 0, Extras.Width, Extras.Height);
                }
                else
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.White), X, Y, Width, Height);
                }

                foreach (Component Comp in Children)
                {
                    Comp.Draw();
                }
            }
        }

        public class Button : Component
        {
            public new int X, Y, Width, Height, Radius;
            public new string Text;
            public new Bitmap Icon;
            public new Component Parent;

            public Button(int aX, int aY, int aWidth, int aHeight, int aRadius, Window aParent)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Radius = aRadius;
                Parent = aParent;
            }

            /// <summary>
            /// Draw the component.
            /// </summary>
            public override void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(
                    pen: new Pen(Colorizer.Button.Background),
                    x_start: Parent.X + X - (Width / 2),
                    y_start: Parent.Y + Y,
                    width: Parent.X + Width - (Width / 2),
                    height: Parent.Y + Height - (Height / 2));
            }
        }

        public class Image : Component
        {
            public new int X, Y, Radius;
            public new Bitmap Icon;
            public new Component Parent;

            public Image(int aX, int aY, Bitmap aImage, Component aParent)
            {
                X = aX;
                Y = aY;
                Icon = aImage;
                Parent = aParent;
            }

            public override void Draw()
            {
                Extras.Canvas.DrawImageAlpha(
                    image: Icon,
                    x: Parent.X + X - ((int)Icon.Width / 2),
                    y: Parent.Y + Y - ((int)Icon.Height / 2));
            }
        }

        public class Label : Component
        {
            public new int X, Y, Radius;
            public new string Text;
            public new Component Parent;

            public Label(int aX, int aY, string aText, Window aParent)
            {
                X = aX;
                Y = aY;
                Text = aText;
                Parent = aParent;
            }

            public override void Draw()
            {
                Extras.Canvas.DrawString(
                    str: Text,
                    aFont: PCScreenFont.Default,
                    pen: new Pen(Colorizer.Label.Text),
                    x: Parent.X + X,
                    y: Parent.Y + Y);
            }
        }

        public class LoadBar : Component
        {
            public new int X, Y, Width;
            public new Component Parent;

            #region Loadbar specific
            public int Percent;
            #endregion Loadbar specific

            public LoadBar(int aX, int aY, int aWidth, int aPercent)
            {
                X = aX - (Width / 2);
                Y = aY - (Height / 2);
                Width = aWidth;
                Percent = aPercent;
            }

            public override void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(new Pen(Color.DimGray), X, Y, Width, 50);
                Extras.Canvas.DrawFilledRectangle(new Pen(Color.DimGray), X, Y, Percent / Percent, 50);
            }
        }
    }
}