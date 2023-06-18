using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls
{
	public class Drawable : Control
	{
		#region Constructors

		public Drawable(int X, int Y, ushort Width, ushort Height) : base(X, Y, Width, Height, ThemeStyle.None) { }

		#endregion

		#region Methods

		public override void Update(Canvas Canvas)
		{
			Canvas.DrawImage(X, Y, this, EnableTransparency);
		}

		#endregion

		#region Fields

		public bool EnableTransparency;

		#endregion
	}
}