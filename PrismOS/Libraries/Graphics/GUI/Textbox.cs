namespace PrismOS.Libraries.Graphics.GUI
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

            Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Theme.Background);
            if (Text.Length == 0 && Hint.Length != 0)
            {
                Canvas.DrawString(Parent.X + X + ((Width / 2) - Hint.Length * Font.Width / 2), Parent.Y + Y, Hint, Font, Theme.ForegroundHint, false);
            }
            else
            {
                Canvas.DrawString(Parent.X + X + ((Width / 2) - Text.Length * Font.Width / 2), Parent.Y + Y, Text, Font, Theme.Foreground, false);
            }
            Canvas.DrawRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Theme.Foreground);
        }
    }
}