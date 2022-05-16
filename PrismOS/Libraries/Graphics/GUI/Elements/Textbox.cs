namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Textbox : Element
    {
        public Color Background = Color.White, Foreground = Color.Black;
        public string Text = "";

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible)
            {
                if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
                {
                    if (Key.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                    {
                        Text.Remove(Text.Length - 1);
                    }
                    else
                    {
                        Text += Key.KeyChar.ToString();
                    }
                }

                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Background);
                Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Foreground);
            }
        }
    }
}