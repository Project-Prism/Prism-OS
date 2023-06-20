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
	public Button(int X, int Y, ushort Width, ushort Height, ushort Radius, string Text, ThemeStyle Theme) : base(X, Y, Width, Height, Theme)
	{
		// Initialize the android holo theme colors.
		Start = new(255, 234, 234, 234);
		Mid = new(255, 180, 180, 180);
		End = new(255, 151, 151, 151);

		this.Radius = Radius;
		this.Text = Text;
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		Clear(Color.Transparent);

		switch (Theme)
		{
			case ThemeStyle.Material:
				DrawMaterial();
				break;
			case ThemeStyle.Holo:
				DrawHolo();
				break;
		}

		Canvas.DrawImage(X, Y, this, Radius != 0);
	}

	private void DrawMaterial()
	{
		switch (Status)
		{
			case CursorStatus.Hovering:
				DrawFilledRectangle(0, 0, Width, Height, Radius, Background - 32);
				DrawString(Width / 2, Height / 2, Text, default, Foreground, true);
				break;
			case CursorStatus.Clicked:
				DrawFilledRectangle(0, 0, Width, Height, Radius, Foreground);
				DrawString(Width / 2, Height / 2, Text, default, Background, true);
				break;
			case CursorStatus.Idle:
				DrawFilledRectangle(0, 0, Width, Height, Radius, Background);
				DrawString(Width / 2, Height / 2, Text, default, Foreground, true);
				break;
		}
	}

	private void DrawHolo()
	{
		DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

		Gradient Overlay = Status switch
		{
			CursorStatus.Hovering => new(Width, Height, Mid, End),
			CursorStatus.Clicked => new(Width, Height, End, Start),
			CursorStatus.Idle => new(Width, Height, Start, End),
			_ => new(Width, Height, Color.Red, Color.BloodOrange),
		};

		DrawImage(0, 0, Filters.MaskAlpha(this, Overlay), Radius != 0);
		DrawString(Width / 2, Height / 2, Text, default, Foreground, true);
	}

	#endregion

	#region Fields

	// The cached android holo theme colors.
	private readonly Color Start;
	private readonly Color Mid;
	private readonly Color End;

	public string Text;

	#endregion
}