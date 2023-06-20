using PrismAPI.Graphics.Fonts;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

public class Label : Control
{
	#region Constructors

	public Label(int X, int Y, string Contents) : base(X, Y, 0, 0, ThemeStyle.None)
	{
		// Initialize core string for safety.
		InternalContents = string.Empty;

		// Make the initial background transparent.
		Background.A = 0;

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
		// Skip updating if there is nothing to do.
		//if (InternalContents == LastContents)
		//{
		//	return;
		//}

		// Set last string object.
		//LastContents = InternalContents;

		// Don't draw directly to canvas to allow for text clipping.
		Clear(Background);
		DrawString(0, 0, Contents, default, Foreground);
		Canvas.DrawImage(X, Y, this, Background.A != 255);
	}

	#endregion

	#region Fields

	internal string InternalContents;
	//internal string LastContents;

	#endregion
}