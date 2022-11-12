using PrismGL2D;

namespace PrismUI.Controls
{
	public class CheckBox : Control
	{
		public CheckBox(bool IsToggled) : base(Config.Scale, Config.Scale)
		{
			this.IsToggled = IsToggled;
		}
		public CheckBox() : base(Config.Scale, Config.Scale) { }

		public bool IsToggled;

		public override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			if (IsToggled)
			{
				DrawFilledRectangle(2, 2, Width - 4, Height - 4, Config.Radius, Config.AccentColor);
			}

			G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
		}
	}
}