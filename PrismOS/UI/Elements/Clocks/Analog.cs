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

        public new void Draw()
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