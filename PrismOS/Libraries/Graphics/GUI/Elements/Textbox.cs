namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Textbox : Element
    {
        public string Hint;
        public string Text = "";
        public Color Background = Color.White, Foreground = Color.Black;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible)
            {
                if (Text.Length == 0 && Hint.Length != 0)
                {
                    Canvas.DrawString(Parent.X + X, Parent.Y + Y, Hint, Color.LightGray);
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

                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Background);
                Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Foreground);
            }
        }
    }
}