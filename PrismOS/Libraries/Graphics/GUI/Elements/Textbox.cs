using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Textbox : Element
    {
        public string Hint;
        public string Text => PText.ToString();
        public List<char> PText = new();
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
                        PText.Remove(PText[PText.Count]);
                    }
                    else
                    {
                        PText.Add(Key.KeyChar);
                    }
                }

                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Background);
                Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Foreground);
            }
        }
    }
}