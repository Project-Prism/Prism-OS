using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

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

	public override void Update(Canvas Canvas, CursorStatus Status)
	{
		Clear(Color.Transparent);
		DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

		switch (Status)
		{
			case CursorStatus.Hovering:
				Gradient GradientHovering = new(Width, Height, StartColor - 32f, EndColor);
				DrawImage(0, 0, Filters.MaskAlpha(this, GradientHovering));
				break;
			case CursorStatus.Clicked:
				Gradient GradientClicked = new(Width, Height, EndColor, StartColor);
				DrawImage(0, 0, Filters.MaskAlpha(this, GradientClicked));
				break;
			case CursorStatus.Idle:
				Gradient GradientIdle = new(Width, Height, StartColor, EndColor);
				DrawImage(0, 0, Filters.MaskAlpha(this, GradientIdle));
				break;
		}

		DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
		Canvas.DrawImage(X, Y, this);
	}

	public override void OnClick(uint X, uint Y, MouseState State)
	{
		throw new NotImplementedException();
	}

	#endregion

	#region Fields

	public Color StartColor;
	public Color EndColor;
	public string Text;

	#endregion
}