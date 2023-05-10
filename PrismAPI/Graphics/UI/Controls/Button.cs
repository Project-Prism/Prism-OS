using PrismAPI.Tools;

namespace PrismAPI.Graphics.UI.Controls;

/// <summary>
/// The <see cref="Button"/> class, used to represent the image state(s) of a clickable button.
/// </summary>
public class Button : Control, IDisposable
{
	/// <summary>
	/// Creates a new instance of the <see cref="Button"/> class, Which is abstracted from the <see cref="Control"/> class.
	/// </summary>
	/// <param name="Width">The Width of the button.</param>
	/// <param name="Height">The Height of the button.</param>
	/// <param name="Radius">The radius/curvature of the button.</param>
	/// <param name="Text">The text shown in the button.</param>
	/// <param name="OnClick">The method fired when the button is pressed.</param>
	public Button(int X, int Y, ushort Width, ushort Height, ushort Radius, string Text, Action OnClick) : base(X, Y, Width, Height, Radius)
	{
		ButtonHovering = new(Width, Height);
		ButtonClicked = new(Width, Height);
		ButtonIdle = new(Width, Height);
		StartColor = new("#EAEAEA");
		EndColor = new("#979797");
		this.OnClick = OnClick;
		this.Text = Text;
	}

	#region Properties

	public override Canvas MainImage
	{
		get
		{
			// Get the offset values.
			int OffsetX = Window == null ? 0 : Window.X;
			int OffsetY = Window == null ? 0 : Window.Y;

			// Check if the mouse is within the control.
			if (MouseEx.IsMouseWithin(OffsetX + X, OffsetY + Y, Width, Height))
			{
				if (MouseEx.IsClickPressed())
				{
					// Check if a click event should fire.
					if (MouseEx.IsClickReady())
					{
						// Invoke the click method.
						OnClick.Invoke();
					}

					// Return the clicked state of the button.
					return ButtonClicked;
				}

				// Return the hovering state of the button.
				return ButtonHovering;
			}

			// Return the idle state of the button.
			return ButtonIdle;
		}
	}

	#endregion

	#region Methods

	public override void Render()
	{
		// Set new sizes if needed.
		ButtonHovering.Height = Height;
		ButtonHovering.Width = Width;
		ButtonClicked.Height = Height;
		ButtonClicked.Width = Width;
		ButtonIdle.Height = Height;
		ButtonIdle.Width = Width;

		// Clear each image to be transparent.
		ButtonHovering.Clear(Color.Transparent);
		ButtonClicked.Clear(Color.Transparent);
		ButtonIdle.Clear(Color.Transparent);

		// Draw the base of the button.
		ButtonHovering.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);
		ButtonClicked.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);
		ButtonIdle.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

		// Generate gradients for each button.
		Canvas GradientHovering = Gradient.GetGradient(Width, Height, StartColor - 32f, EndColor);
		Canvas GradientClicked = Gradient.GetGradient(Width, Height, EndColor, StartColor);
		Canvas GradientIdle = Gradient.GetGradient(Width, Height, StartColor, EndColor);

		// Mask each button image to apply the gradient & draw it onto the images.
		ButtonHovering.DrawImage(0, 0, Filters.MaskAlpha(ButtonHovering, GradientHovering));
		ButtonClicked.DrawImage(0, 0, Filters.MaskAlpha(ButtonClicked, GradientClicked));
		ButtonIdle.DrawImage(0, 0, Filters.MaskAlpha(ButtonIdle, GradientIdle));

		// Free old gradients & colors.
		GradientHovering.Dispose();
		GradientClicked.Dispose();
		GradientIdle.Dispose();

		// Draw the text onto the images.
		ButtonHovering.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
		ButtonClicked.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
		ButtonIdle.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);

		// Re-render the parent window.
		Window?.Render();
	}

	public void Dispose()
	{
		ButtonHovering.Dispose();
		ButtonClicked.Dispose();
		ButtonIdle.Dispose();

		GC.SuppressFinalize(this);
	}

	#endregion

	#region Fields

	private readonly Canvas ButtonHovering;
	private readonly Canvas ButtonClicked;
	private readonly Canvas ButtonIdle;
	public Action OnClick;
	public Color StartColor;
	public Color EndColor;
	public string Text;

	#endregion
}