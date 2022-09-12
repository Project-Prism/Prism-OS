namespace PrismGL2D.UI
{
	public class ImageButton : Button
	{
		public ImageButton()
		{
			Source = new(0, 0);
		}

		public Graphics Source { get; set; }

		public override void OnDrawEvent(Graphics Buffer)
		{
			base.OnDrawEvent(this);

			if (Source.Width != Width || Source.Height != Height)
			{
				Source = Source.Resize(Width, Height);
			}

			DrawImage(0, 0, Source, true);
			DrawString((int)(Source.Width + 15), (int)(Height / 2), Text, Font, Theme.Foreground);

			Buffer.DrawImage(X, Y, Source, true);
		}
	}
}