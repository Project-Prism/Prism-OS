namespace PrismOS.Graphics.GUI
{
    public class Textbox : Element
    {
        public Canvas.Font Font = Canvas.Font.Default;
        public string Hint = "";
        public string Text = "";

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Parent == Runtime.Windows[^1] && Cosmos.System.KeyboardManager.TryReadKey(out var Key))
            {
                if (Key.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                {
                    Text = Text[0..(Text.Length - 1)];
                }
                else
                {
                    Text += Key.KeyChar;
                }
            }

            Canvas.DrawFilledRectangle(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                Size.Width,
                Size.Height,
                Parent.Theme.Radius,
                Parent.Theme.Background);
            
            if (Text.Length == 0 && Hint.Length != 0)
            {
                Canvas.DrawString(
                    Parent.Position.X + Position.X + ((Size.Width / 2) - Hint.Length * Font.Width / 2),
                    Parent.Position.Y + Position.Y,
                    Hint,
                    Parent.Theme.Font,
                    Parent.Theme.ForegroundLight,
                    true);
            }
            else
            {
                Canvas.DrawString(
                    Parent.Position.X + Position.X + ((Size.Width / 2) - Text.Length * Font.Width / 2),
                    Parent.Position.Y + Position.Y, Text, Font,
                    Parent.Theme.Foreground,
                    true);
            }
            
            Canvas.DrawRectangle(
                Parent.Position.X + Position.X,
                Parent.Position.Y + Position.Y,
                Size.Width,
                Size.Height,
                Parent.Theme.Radius,
                Parent.Theme.Foreground);
        }
    }
}