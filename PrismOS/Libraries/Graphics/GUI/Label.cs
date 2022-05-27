namespace PrismOS.Libraries.Graphics.GUI
{
    public class Label : Element
    {
        public string Text;
        public Color Color;
        public bool Underline, Crossout, Center;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible)
            {
                Canvas.DrawString(Parent.X + X + (Width / 2), Parent.Y + Y + (Height / 2), Text, Color, Underline, Crossout, Center);
            }
        }
    }
}