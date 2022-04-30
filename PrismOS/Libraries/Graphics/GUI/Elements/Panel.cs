using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Panel : Element
    {
        public Color Color;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible)
            {
                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Color);
            }
        }
    }
}