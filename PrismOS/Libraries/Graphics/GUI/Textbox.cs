namespace PrismOS.Libraries.Graphics.GUI
{
    public class Textbox : Element
    {
        public string Hint;
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
                Canvas.DrawString(Parent.X + X + (Width / 2), Parent.Y + Y + (Height / 2), Hint, Theme.ForegroundHint, true);
            }
            else
            {
                Canvas.DrawString(Parent.X + X + (Width / 2), Parent.Y + Y + (Height / 2), Text, Theme.Foreground, true);
            }
            Canvas.DrawRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Theme.Foreground);
        }
    }
}