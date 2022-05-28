namespace PrismOS.Libraries.Graphics.GUI
{
    public class Panel : Element
    {
        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible && Theme != null)
            {
                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Theme.Background);
            }
        }
    }
}