using PrismAPI.Graphics.Fonts;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

public class Label : Control
{
	#region Constructors

	public Label(string Contents) : base(0, 0, ThemeStyle.None)
	{
		// Initialize core string for safety.
		InternalContents = string.Empty;

		this.Contents = Contents;
	}

	#endregion

	#region  Properties

	public string Contents
	{
		get => InternalContents;
		set
		{
			Width = Font.Fallback.MeasureString(Contents);
			Height = (ushort)((Font.Fallback.Size * Contents.Count(C => C == '\n')) + 1);
			InternalContents = value;
		}
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		Canvas.DrawString(X, Y, Contents, default, Color.White);
	}

	#endregion

	#region Fields

	internal string InternalContents;

	#endregion
}