using PrismGL2D;

namespace PrismUI.Controls
{
    public class TextBox : Control
    {
        public TextBox(uint Width, uint Height) : base(Width, Height)
		{
            OnKeyEvent = (ConsoleKeyInfo Key) =>
            {
                switch (Key.Key)
				{
                    case ConsoleKey.Enter:
                        Text += "\r\n";
                        break;
                    case ConsoleKey.Backspace:
                        Text = Text[..^2];
                        break;
                    default:
                        Text += Key.KeyChar;
                        break;
				}
            };
            CanInteract = false;
            Hint = "";
		}

        public string Hint;

        public override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString(0, 0, Text.Length == 0 ? Hint : Text, Config.Font, Config.ForeColor);

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}