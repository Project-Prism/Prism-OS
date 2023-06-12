using PrismAPI.Graphics.Fonts;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

public class Label : Control
{
	#region Constructors

	public Label(string Contents) : base(Font.Fallback.MeasureString(Contents), (ushort)((Font.Fallback.Size * Contents.Count(C => C == '\n')) + 1))
	{
		this.Contents = Contents;
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		Canvas.DrawString(X, Y, Contents, default, Color.White);
	}

	#endregion

	#region Fields

	public string Contents;

	#endregion
}