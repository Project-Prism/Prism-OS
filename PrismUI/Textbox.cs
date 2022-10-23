using PrismGL2D;

namespace PrismUI
{
    public class TextBox : Control
    {
        public TextBox(uint Width, uint Height) : base(Width, Height)
		{
            OnKeyEvent = (ConsoleKeyInfo Key) => { Text += Key.KeyChar; };
            CanInteract = false;
            Hint = "";
		}

        public string Hint;

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString(0, 0, Text.Length == 0 ? Hint : Text, Config.Font, Config.GetForeground(false, false));

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}