using PrismGL2D;

namespace PrismUI
{
    public class TextBox : Control
    {
        public TextBox(uint Width, uint Height) : base(Width, Height)
		{
            Hint = "";
		}

        public string Hint;

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString((int)(Width / 2), (int)(Height / 2), Text.Length == 0 ? Hint : Text, Config.Font, Config.GetForeground(false, false), true);

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}