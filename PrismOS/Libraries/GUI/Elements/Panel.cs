using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.GUI.Elements
{
    public class Panel : Element
    {
        public Color Color;

        public override void Update(Canvas Canvas)
        {
            Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color);
        }
    }
}