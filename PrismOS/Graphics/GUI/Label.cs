namespace PrismOS.Graphics.GUI
{
    public class Label : Element
    {
        public Canvas.Font Font = Canvas.Font.Default;
        public string Text;
        public bool Center;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Canvas.DrawString(
                Parent.Position.X + Position.X + (Size.Width / 2),
                Parent.Position.Y + Position.Y + (Size.Height / 2),
                Text,
                Parent.Theme.Font,
                Parent.Theme.Foreground,
                Center);
        }
    }
}