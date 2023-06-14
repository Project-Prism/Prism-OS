using PrismAPI.Graphics.Fonts;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

public class Label : Control
{
	#region Constructors

	public Label(int X, int Y, string Contents) : base(0, 0, ThemeStyle.None)
	{
		// Initialize core string for safety.
		InternalContents = string.Empty;

		// Initialize the X and Y position.
		this.X = X;
		this.Y = Y;

		this.Contents = Contents;
	}

	#endregion

	#region  Properties

	public string Contents
	{
		get => InternalContents;
		set
		{
			Width = Font.Fallback.MeasureString(value);
			Height = (ushort)((Font.Fallback.Size * value.Count(C => C == '\n')) + 1);
			InternalContents = value;
		}
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		// Don't draw directly to canvas to allow for text clipping.
		Clear(Color.Transparent);
		DrawString(0, 0, Contents, default, Color.White);
		Canvas.DrawImage(X, Y, this, true);
	}

	#endregion

	#region Fields

	internal string InternalContents;

	#endregion
}