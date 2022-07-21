using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Textbox : Control
    {
        public string Hint = "";
        public string Text = "";

        public override void Update(Window Parent)
        {
            if (Visible)
            {
                if (Parent == Window.Windows[^1] && Cosmos.System.KeyboardManager.TryReadKey(out var Key))
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

                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, Parent.Theme.Background);
                FrameBuffer.DrawString(Width / 2, Height / 2, (Text.Length == 0 ? Hint : Text), Parent.Theme.Font, Parent.Theme.Foreground, true);
                FrameBuffer.DrawRectangle(0, 0, Width - 1, Height - 1, Parent.Theme.Radius, Parent.Theme.Foreground);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }
        }
    }
}