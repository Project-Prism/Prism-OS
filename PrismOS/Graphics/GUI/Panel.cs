namespace PrismOS.Graphics.GUI
{
    public class Panel : Element
    {
        public override void Update(Canvas Canvas, Window Parent)
        {
            Canvas.DrawFilledRectangle(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                Size.Width,
                Size.Height,
                Parent.Theme.Radius,
                Parent.Theme.Background);
        }
    }
}