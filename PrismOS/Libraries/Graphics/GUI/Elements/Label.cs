using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Label : Element
    {
        public string Text;
        public Color Color;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible)
            {
                Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Color);
            }
        }
    }
}