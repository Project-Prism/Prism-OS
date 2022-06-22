namespace PrismOS.Graphics.GUI
{
    public class Button : Element
    {
        public string Text;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Color BG, FG;
            if (Clicked)
            {
                BG = Parent.Theme.BackgroundClick;
                FG = Parent.Theme.ForegroundClick;
            }
            else if (Hovering)
            {
                BG = Parent.Theme.BackgroundHover;
                FG = Parent.Theme.ForegroundHover;
            }
            else
            {
                BG = Parent.Theme.Background;
                FG = Parent.Theme.Foreground;
            }

            int SX = Parent.Position.X + Position.X + ((Size.Width / 2) - Text.Length * Parent.Theme.Font.Width / 2);
            int SY = Parent.Position.Y + Position.Y + ((Size.Height / 2) - (Text.Split('\n').Length * Parent.Theme.Font.Height / 2));
            
            Canvas.DrawFilledRectangle(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                Size.Width,
                Size.Height,
                Parent.Theme.Radius,
                BG);
            
            Canvas.DrawString(
                SX,
                SY,
                Text,
                Parent.Theme.Font,
                FG);
            
            Canvas.DrawRectangle(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                Size.Width - 1,
                Size.Height - 1,
                Parent.Theme.Radius,
                Parent.Theme.Foreground);
        }
    }
}