namespace PrismGL2D.UI
{
	public class Crosshair : Control
	{
		public Crosshair()
		{
			Scale = 0;
		}

		public uint Scale { get; set; }

		public override void OnDrawEvent(Graphics Graphics)
		{
			base.OnDrawEvent(Graphics);

			Clear(Color.Transparent);
			DrawFilledRectangle(0, (int)Height / 2 - 15, (int)Width, (int)Height / 2 + 15, 0, Color.White);
			DrawFilledRectangle((int)Width / 2 - 15, 0, (int)Height, (int)Width / 2 + 15, 0, Color.White);

			Graphics.DrawImage(X - ((int)Width / 2), Y - ((int)Height / 2), this, true);
		}
	}
}