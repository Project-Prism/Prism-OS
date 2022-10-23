using PrismGL2D;

namespace PrismUI.Controls
{
	public class ImageButton : Button
	{
		public ImageButton(Graphics Source, string Text) : base(Source.Width + Config.Scale + Font.Fallback.MeasureString(Text), Source.Height)
		{
			this.Source = Source;
		}
		public ImageButton(uint Width, uint Height) : base(Width, Height)
		{
			Source = new(Height, Width);
		}

		public Graphics Source { get; set; }

		internal override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			if (Source.Width != Width || Source.Height != Height)
			{
				Source = Source.Scale(Width, Height);
			}

			DrawImage(0, 0, Source, true);
			DrawString((int)(Source.Width + 15), (int)(Height / 2), Text, Config.Font, Config.GetForeground(this));

			G.DrawImage(X, Y, Source, Config.ShouldContainAlpha(this));
		}
	}
}