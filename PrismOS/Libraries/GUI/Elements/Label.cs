using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.GUI.Elements
{
    public class Label : Element
    {
        public string Text;
        public Color Color;

        public override void Update(Canvas Canvas)
        {
            if (Visible)
            {
                Canvas.DrawString(X, Y, Text, Color);
            }
        }
    }
}