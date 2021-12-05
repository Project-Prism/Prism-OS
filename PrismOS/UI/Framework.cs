using System.Collections.Generic;
using System.Drawing;
using static PrismOS.UI.Extras;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;

namespace PrismOS.UI
{
    public static class Framework
    {
        public abstract class Component
        {
            public struct Properties
            {
                public static Component Parent { get; set; }
                public static List<Component> Children { get; set; }
                public static int X { get; set; }
                public static int Y { get; set; }
                public static int Width { get; set; }
                public static int Height { get; set; }
                public static int Radius { get; set; }
                public static string Text { get; set; }
                public static Bitmap Icon { get; set; }
                public static bool FullScreen { get; set; }
            }

            public abstract void Draw();
        }

        public class Window : Component
        {
            public Window(int X, int Y, int Width, int Height, int Radius, bool FullScreen, string Text, Bitmap Icon)
            {
                Properties.X = X;
                Properties.Y = Y;
                Properties.Width = Width;
                Properties.Height = Height;
                Properties.Radius = Radius;
                Properties.Text = Text;
                Properties.Icon = Icon;
                Properties.FullScreen = FullScreen;
            }

            public new struct Properties
            {
                public static Component Parent { get; set; }
                public static List<Component> Children { get; set; }
                public static int X { get; set; }
                public static int Y { get; set; }
                public static int Width { get; set; }
                public static int Height { get; set; }
                public static int Radius { get; set; }
                public static string Text { get; set; }
                public static Bitmap Icon { get; set; }
                public static bool FullScreen { get; set; }
            }

            public override void Draw()
            {
                if (Properties.FullScreen)
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.Black), 0, 0, Extras.Width, Extras.Height);
                }
                else
                {
                    Extras.Canvas.DrawFilledRectangle(new Pen(Color.White), Properties.X, Properties.Y, Properties.Width, Properties.Height);
                }

                foreach (Component Comp in Properties.Children)
                {
                    Comp.Draw();
                }
            }
        }

        public class Clickable : Component
        {
            public Clickable(int X, int Y, int Width, int Height, int Radius, Component Parent)
            {
                Properties.Parent = Parent;
                Properties.Children = new List<Component>();
                Properties.X = X;
                Properties.Y = Y;
                Properties.Width = Width;
                Properties.Height = Height;
                Properties.Radius = Radius;
            }

            public new struct Properties
            {
                public static Component Parent { get; set; }
                public static List<Component> Children { get; set; }
                public static int X { get; set; }
                public static int Y { get; set; }
                public static int Width { get; set; }
                public static int Height { get; set; }
                public static int Radius { get; set; }
            }

            public override void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(
                    pen: new Pen(Colorizer.Button.Background),
                    x_start: Properties.Parent.X + Properties.X - (Properties.Width / 2),
                    y_start: Properties.Parent.Y + Properties.Y,
                    width: Properties.Parent.X + Properties.Width - (Properties.Width / 2),
                    height: Properties.Parent.Y + Properties.Height - (Properties.Height / 2));
            }
        }

        // below here needs fixing
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
                X = aX;
                Y = aY;
                Width = aWidth;
                Percent = aPercent;
            }

            public override void Draw()
            {
                Extras.Canvas.DrawFilledRectangle(new Pen(Color.DimGray), Parent.X + X - (Width / 2), Parent.Y + Y - (Height / 2), Width, 50);
                Extras.Canvas.DrawFilledRectangle(new Pen(Color.DimGray), Parent.X + X - (Width / 2), Parent.Y + Y - (Height / 2), Percent / Percent, 50);
            }
        }
    }
}