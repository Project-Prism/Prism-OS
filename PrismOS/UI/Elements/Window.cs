using System.Drawing;

namespace PrismOS.UI.Elements
{
    public class Window : Element
    {
        public Window(int X, int Y, int Width, int Height, Color Foreground, Color Background, Color Accent, Canvas Canvas)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Foreground = Foreground;
            this.Background = Background;
            this.Accent = Accent;
            this.Canvas = Canvas;
            Children = new();
        }

        public new void Draw()
        {
            Canvas.DrawFilledRectangle(X, Y, Width, Height, Background);
            Canvas.DrawFilledRectangle(X, Y, Width, 50, Foreground);

            foreach (Element Object in Children)
            {
                Object.Draw();
            }
        }
    }
}