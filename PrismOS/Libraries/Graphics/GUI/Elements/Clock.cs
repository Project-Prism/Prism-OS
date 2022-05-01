using static PrismOS.Libraries.Graphics.GUI.WindowManager;
using System;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Clock : Element
    {
        public DateTime Time;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible)
            {
                Canvas.DrawFilledCircle(Parent.X + X, Parent.Y + Y, Radius, Color.White);
                Canvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, DateTime.Now.Hour, Radius - 45, Color.Black);
                Canvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, DateTime.Now.Minute, Radius - 25, Color.Black);
                Canvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, DateTime.Now.Second, Radius - 5, Color.Red);
            }
        }
    }
}