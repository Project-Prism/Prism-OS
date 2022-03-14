using System.Collections.Generic;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS.Libraries.Graphics
{
    public static class GUI
    {
        public static List<Element> Windows = new();

        public class Element
        {
            public Element(int X, int Y, int Width, int Height, int Radius, string Text, Bitmap Icon, Color Foreground, Color Background, Element Parent = null)
            {
                this.X = X;
                this.Y = Y;
                this.Width = Width;
                this.Height = Height;
                this.Radius = Radius;
                this.Text = Text;
                this.Icon = Icon;
                this.Foreground = Foreground;
                this.Background = Background;
                this.Parent = Parent;
                Children = new();
            }

            public string Text;
            public Bitmap Icon;
            public Element Parent;
            public List<Element> Children;
            public Color Foreground, Background;
            public int X, Y, Width, Height, Radius;

            public void Draw(Canvas Canvas)
            {
                int IX = X, IY = Y;

                if (Parent != null)
                {
                    IX += Parent.X;
                    IY += Parent.Y;
                }

                Canvas.DrawFilledRectangle(IX, IY, Width, Height, Radius, Background);
                if (Text.Length != 0)
                {
                    Canvas.DrawString(IX, IY, Text, Foreground);
                }
                if (Icon != null)
                {
                    Canvas.DrawBitmap(IX, IY, Icon);
                }

                foreach (Element Element in Children)
                {
                    Element.Draw(Canvas);
                }
            }
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            if (Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height)
                return true;
            else
                return false;
        }
    }
}