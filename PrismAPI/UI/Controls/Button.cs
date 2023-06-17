﻿using PrismAPI.UI.Config;
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
	public Button(int X, int Y, ushort Width, ushort Height, ushort Radius, string Text, ThemeStyle Theme) : base(Width, Height, Theme)
	{
		StartColor = new(255, 234, 234, 234);
		EndColor = new(255, 151, 151, 151);
		MidColor = new(255, 119, 119, 119);
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
	}

	private void DrawHolo()
	{
		DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

		Gradient Overlay = Status switch
		{
			CursorStatus.Hovering => new(Width, Height, MidColor, EndColor),
			CursorStatus.Clicked => new(Width, Height, EndColor, StartColor),
			CursorStatus.Idle => new(Width, Height, StartColor, EndColor),
			_ => new(Width, Height, Color.Red, Color.BloodOrange),
		};

		DrawImage(0, 0, Filters.MaskAlpha(this, Overlay), Radius != 0);
		DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
	}

	#endregion

	#region Fields

	public Color StartColor;
	public Color EndColor;
	public Color MidColor;
	public string Text;

	#endregion
}