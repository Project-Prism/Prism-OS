using PrismGL2D;

namespace PrismUI
{
	public class Crosshair : Control
	{
		public Crosshair(uint Width, uint Height) : base(Width, Height)
		{
			HasBackground = false;
		}

		internal override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			DrawFilledRectangle(0, (int)Height / 2 - (int)Config.Scale, Width, Height / 2 + Config.Scale, 0, Color.White);
			DrawFilledRectangle((int)Width / 2 - (int)Config.Scale, 0, Height, Width / 2 + Config.Scale, 0, Color.White);

			if (HasBorder)
			{
				G.DrawRectangle(X - 1, Y - 1, Width + 2, Height + 2, Config.Radius, Config.GetForeground(false, false));
			}

			G.DrawImage(X - ((int)Width / 2), Y - ((int)Height / 2), this, true);
		}
	}
}