using PrismOS.Graphics.Utilities;

namespace PrismOS.Graphics.GUI.Clocks
{
    public class Analog : Element
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
            Parent.Screen.DrawFilledCircle(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Radius: Radius + 45,
                Color: Parent.Theme.Background);

            Parent.Screen.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Hour,
                Radius: Radius,
                Color: Parent.Theme.Foreground);

            Parent.Screen.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Minute,
                Radius: Radius + 20,
                Color: Parent.Theme.Foreground);

            Parent.Screen.DrawAngledLine(
                X: Parent.X + X,
                Y: Parent.Y + Y,
                Angle: System.DateTime.Now.Second,
                Radius: Radius + 40,
                Color: Parent.Theme.Accent);
        }
    }
}