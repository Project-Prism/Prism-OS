using PrismGL2D;

namespace PrismUI.Controls
{
	public class Crosshair : Control
	{
		public Crosshair(uint Width, uint Height) : base(Width, Height)
		{
			HasBackground = false;
			HasBorder = false;
		}

		public override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			DrawFilledRectangle(0, (int)Height / 2 - (int)Config.Scale, Width, Height / 2 + Config.Scale, 0, Color.White);
			DrawFilledRectangle((int)Width / 2 - (int)Config.Scale, 0, Height, Width / 2 + Config.Scale, 0, Color.White);

			G.DrawImage(X - ((int)Width / 2), Y - ((int)Height / 2), this, true);
		}
	}
}