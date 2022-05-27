namespace PrismOS.Libraries.Graphics.GUI
{
    public class Panel : Element
    {
        public Color Color;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible)
            {
                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Color);
            }
        }
    }
}