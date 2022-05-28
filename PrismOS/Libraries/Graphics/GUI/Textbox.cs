namespace PrismOS.Libraries.Graphics.GUI
{
    public class Textbox : Element
    {
        public string Hint;
        public string Text = "";

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible && Theme != null)
            {
                if (Text.Length == 0 && Hint.Length != 0)
                {
                    Canvas.DrawString(Parent.X + X, Parent.Y + Y, Hint, Theme.ForegroundHint);
                }
                if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
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
                Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Theme.Background);
            }
        }
    }
}