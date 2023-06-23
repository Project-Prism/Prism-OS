using PrismAPI.Graphics.Fonts;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI.Controls;

public class Textbox : Control
{
	#region  Constructors

	public Textbox(int X, int Y, string Hint, string Text) : base(X, Y, 0, 0, ThemeStyle.None)
	{
		// Initialize core strings for safety.
		InternalHint = string.Empty;
		InternalText = string.Empty;

		// Set-up the OnKey event.
		OnKey = new(ProcessKey);

		// Initialize the core string setters - this sets the control's size;
		this.Hint = Hint;
		this.Text = Text;
	}

	#endregion

	#region  Properties

	public string Hint
	{
		get => InternalHint;
		set
		{
			Width = Math.Max(Font.Fallback.MeasureString(value), Width);
			Height = Math.Max((ushort)(Font.Fallback.Size * (value.Count(C => C == '\n') + 1)), Height);
			InternalHint = value;
		}
	}

	public string Text
	{
		get => InternalText;
		set
		{
			Width = Math.Max(Font.Fallback.MeasureString(value), Width);
			Height = Math.Max((ushort)(Font.Fallback.Size * (value.Count(C => C == '\n') + 1)), Height);
			InternalText = value;
		}
	}

	#endregion

	#region Methods

	private void ProcessKey(ConsoleKeyInfo Key)
	{
		switch (Key.Key)
		{
			case ConsoleKey.Backspace:
				InternalText = InternalText[0..^2];
				break;
			default:
				InternalText += Key.KeyChar;
				break;
		}

		// Re-assign to start resize;
		Text = InternalText;
	}

	public override void Update(Canvas Canvas)
	{
		Clear();

		DrawFilledRectangle(0, 0, Width, Height, Radius, Background - 16);

		// Draw a green accent if the control is active.
		if (IsActive)
		{
			DrawRectangle(0, Height - (Height / 8), Width, (ushort)(Height / 8), 0, Accent);
		}

		if (string.IsNullOrEmpty(InternalText))
		{
			DrawString(Width / 2, Height / 2, InternalHint, default, Background - 32, true);
		}
		else
		{
			DrawString(Width / 2, Height / 2, InternalText, default, Foreground, true);
		}

		Canvas.DrawImage(X, Y, this, Radius != 0);
	}

	#endregion

	#region Fields

	private string InternalHint;
	private string InternalText;
	public bool IsActive;

	#endregion
}