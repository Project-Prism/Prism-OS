using PrismUI.Structure;
using PrismGL2D;

namespace PrismUI.Controls
{
	public class Tooltip : Control
	{
		public Tooltip(Control Parent, string Text) : base(0, 0)
		{
			this.Parent = Parent;
			this.Text = Text;
		}

		#region Methods

		public override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			if (Parent.ClickState != ClickState.Neutral)
			{
				G.DrawFilledRectangle(
					X: (int)(Parent.X + (Parent.Width / 2)),
					Y: (int)(Parent.Y - (Font.Fallback.Size - 32)),
					Width: Font.Fallback.MeasureString(Text),
					Height: Font.Fallback.Size + 16,
					Radius: 0,
					Color: Color.LightBlack);

				G.DrawString(
					X: (int)(Parent.X + (Parent.Width / 2)),
					Y: (int)(Parent.Y - (Font.Fallback.Size - 16)),
					Text: Text,
					Font: Font.Fallback,
					Color: Color.White);
			}
		}

		#endregion

		#region Fields

		private Control Parent;

		#endregion
	}
}