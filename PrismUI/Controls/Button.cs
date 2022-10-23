using PrismGL2D;

namespace PrismUI.Controls
{
	public class Button : Control
    {
        public Button(uint Width, uint Height) : base(Width, Height) { }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString((int)(Width / 2), (int)(Height / 2), Text, Config.Font, Config.GetForeground(this), true);

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}