using PrismGL2D;

namespace PrismUI
{
	public class Crosshair : Control
	{
		public Crosshair()
		{
			HasBackground = false;
		}

		public override void OnDrawEvent(Graphics Graphics)
		{
			base.OnDrawEvent(Graphics);

			DrawFilledRectangle(0, (int)Height / 2 - (int)Config.Scale, Width, Height / 2 + Config.Scale, 0, Color.White);
			DrawFilledRectangle((int)Width / 2 - (int)Config.Scale, 0, Height, Width / 2 + Config.Scale, 0, Color.White);

			Graphics.DrawImage(X - ((int)Width / 2), Y - ((int)Height / 2), this, true);
		}
	}
}