using System;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class Clock : Element
    {
        public DateTime Time;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Canvas.DrawFilledCircle(
                Parent.Position.X + Position.X, 
                Parent.Position.Y + Position.Y,
                Parent.Theme.Radius, 
                Parent.Theme.Background);
            
            Canvas.DrawAngledLine(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                DateTime.Now.Hour * 30,
                Parent.Theme.Radius - 45,
                Parent.Theme.Background);
            
            Canvas.DrawAngledLine(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                DateTime.Now.Minute * 6,
                Parent.Theme.Radius - 25,
                Parent.Theme.Background);
            
            Canvas.DrawAngledLine(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                DateTime.Now.Second * 6,
                Parent.Theme.Radius - 5,
                Parent.Theme.Accent);
        }
    }
}