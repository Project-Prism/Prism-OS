using PrismOS.Graphics.GUI;
using PrismOS.Graphics.GUI.Containers;

namespace PrismOS.Graphics.GUI.Clocks
{
    public class Analog : Base
    {
        public Analog(int X, int Y, int Radius, Window Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = Radius;
            this.Parent = Parent;
        }

        public new void Draw()
        {
            Canvas.GetCanvas.DrawFilledCircle(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Radius: Radius + 45,
                Color: Parent.Theme.Background);

            Canvas.GetCanvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Hour,
                Radius: Radius,
                Color: Parent.Theme.Foreground);

            Canvas.GetCanvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Minute,
                Radius: Radius + 20,
                Color: Parent.Theme.Foreground);

            Canvas.GetCanvas.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Second,
                Radius: Radius + 40,
                Color: Parent.Theme.Accent);
        }
    }
}