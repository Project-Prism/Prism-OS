using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using static PrismOS.Libraries.Extras;

namespace PrismOS.Libraries.UI
{
    public static class Components
    {
        public abstract class Component
        {
            public string Text;
            public Bitmap Icon;
            public PCScreenFont Font;
            public Containers.Window Parent;
            public int X, Y, Width, Height, Radius;

            public void Draw()
            {
                // Threw in some random junk so there arent stupid warnings.
                _ = X;
            }
        }

        public class Button : Component
        {
            public new string Text;
            public new Bitmap Icon;
            public new PCScreenFont Font;
            public new Containers.Window Parent;
            public new int X, Y, Width, Height, Radius;

            public Button(int aX, int aY, int aWidth, int aHeight, int aRadius, Containers.Window aParent)
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
            /// <param name="OX">The X offset for drawing.</param>
            /// <param name="OY">The Y offset for drawing.</param>
            public new void Draw()
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

            public new void Draw()
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
            public new PCScreenFont Font;
            public new Containers.Window Parent;

            public Label(int aX, int aY, string aText, Containers.Window aParent)
            {
                X = aX;
                Y = aY;
                Text = aText;
                Parent = aParent;
                Font = PCScreenFont.Default;
            }

            public new void Draw()
            {
                Extras.Canvas.DrawString(
                    str: Text,
                    aFont: Font,
                    pen: new Pen(Colorizer.Label.Text),
                    x: Parent.X + X,
                    y: Parent.Y + Y);
            }
        }
    }
}
