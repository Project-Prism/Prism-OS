using System.Collections.Generic;
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

        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override int Radius { get; set; }
        public override Color Foreground { get; set; }
        public override Color Background { get; set; }
        public override Color Accent { get; set; }
        public override Element Parent { get; set; }
        public override List<Element> Children { get; set; }
        public override Canvas Canvas { get; set; }

        public override void Draw()
        {
            Canvas.DrawFilledRectangle(X, Y, Width, Height, Background);
            Canvas.DrawFilledRectangle(X, Y, Width, 50, Foreground);

            foreach(Element Object in Children)
            {
                Object.Draw();
            }
        }
    }
}