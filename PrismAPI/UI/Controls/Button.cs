using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

/// <summary>
/// The <see cref="Button"/> class, used to represent the image state(s) of a clickable button.
/// </summary>
public class Button : Control
{
	#region Constructors

	/// <summary>
	/// Creates a new instance of the <see cref="Button"/> class, Which is abstracted from the <see cref="Control"/> class.
	/// </summary>
	/// <param name="Width">The Width of the button.</param>
	/// <param name="Height">The Height of the button.</param>
	/// <param name="Radius">The radius/curvature of the button.</param>
	/// <param name="Text">The text shown in the button.</param>
	public Button(int X, int Y, ushort Width, ushort Height, ushort Radius, string Text) : base(Width, Height)
	{
		StartColor = new("#EAEAEA");
		EndColor = new("#979797");
		this.Radius = Radius;
		this.Text = Text;
		this.X = X;
		this.Y = Y;
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		Clear(Color.Transparent);

		switch (Theme)
		{
			// The material 2.0 design from android 5+.
			case ThemeStyle.Material:
				switch (Status)
				{
					case CursorStatus.Hovering:
						DrawFilledRectangle(0, 0, Width, Height, Radius, Color.LightGray);
						DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
						break;
					case CursorStatus.Clicked:
						DrawFilledRectangle(0, 0, Width, Height, Radius, Color.DeepGray);
						DrawString(Width / 2, Height / 2, Text, default, Color.White, true);
						break;
					case CursorStatus.Idle:
						DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);
						DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
						break;
				}
				break;

			// The Holo theme from android 3.x+.
			case ThemeStyle.Holo:
				// Apply a basic layer to mask.
				DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

				switch (Status)
				{
					case CursorStatus.Hovering:
						Gradient GradientHovering = new(Width, Height, StartColor - 32f, EndColor);
						DrawImage(0, 0, Filters.MaskAlpha(this, GradientHovering), Radius != 0);
						DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
						break;
					case CursorStatus.Clicked:
						Gradient GradientClicked = new(Width, Height, EndColor, StartColor);
						DrawImage(0, 0, Filters.MaskAlpha(this, GradientClicked), Radius != 0);
						DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
						break;
					case CursorStatus.Idle:
						Gradient GradientIdle = new(Width, Height, StartColor, EndColor);
						DrawImage(0, 0, Filters.MaskAlpha(this, GradientIdle), Radius != 0);
						DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
						break;
				}
				break;

			default:
				return;
		}

		Canvas.DrawImage(X, Y, this, Radius != 0);
	}

	#endregion

	#region Fields

	public Color StartColor;
	public Color EndColor;
	public string Text;

	#endregion
}