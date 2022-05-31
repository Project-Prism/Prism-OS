namespace PrismOS.Libraries.Graphics.GUI
{
    public class Button : Element
    {
        public string Text;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Color BG = Clicked ? Theme.BackgroundClick : Hovering ? Theme.BackgroundHover : Theme.Background;
            Color FG = Clicked ? Theme.ForegroundClick : Hovering ? Theme.ForegroundHover : Theme.Foreground;

            int SX = Parent.X + X + ((Width / 2) - Text.Length * Canvas.Font.Default.Width / 2);
            int SY = Parent.Y + Y + ((Height / 2) - (Text.Split('\n').Length * Canvas.Font.Default.Height / 2));

            Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, BG);
            Canvas.DrawString(SX, SY, Text, FG);
            Canvas.DrawRectangle(Parent.X + X, Parent.Y + Y, Width - 1, Height - 1, Radius, Theme.Foreground);
        }
    }
}