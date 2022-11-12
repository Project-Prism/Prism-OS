using PrismGL2D;

namespace PrismUI.Controls
{
	public class Button : Control
    {
        public Button(uint Width, uint Height) : base(Width, Height) { }
        public Button(string Text) : base(0, 0)
		{
            Width = Font.Fallback.MeasureString(Text) + Config.Scale;
            Height = Config.Scale;
            this.Text = Text;
		}

        public override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString((int)(Width / 2), (int)(Height / 2), Text, Config.Font, Config.GetForeground(this), true);

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}