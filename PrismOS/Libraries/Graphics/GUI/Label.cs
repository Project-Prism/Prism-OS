namespace PrismOS.Libraries.Graphics.GUI
{
    public class Label : Element
    {
        public string Text;
        public bool Underline, Crossout, Center;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Canvas.DrawString(Parent.X + X + (Width / 2), Parent.Y + Y + (Height / 2), Text, Theme.Foreground, Underline, Crossout, Center);
        }
    }
}