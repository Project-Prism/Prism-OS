using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Elements.Clocks
{
    public class Analog : Element
    {
        public Analog(int X, int Y, int Radius, Element Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = Radius;
            this.Parent = Parent;
            this.Canvas = Parent.Canvas;
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
            Canvas.DrawFilledCircle(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Radius: Radius + 45,
                Color: Background);

            Canvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Hour,
                Radius: Radius,
                Color: Foreground);

            Canvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Minute,
                Radius: Radius + 20,
                Color: Foreground);

            Canvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Second,
                Radius: Radius + 40,
                Color: Accent);
        }
    }
}