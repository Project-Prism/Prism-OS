using PrismGraphics;

namespace PrismUI
{
	public class Button
	{
		public Button(ushort Width, ushort Height, ushort Radius, string Text, Action OnClick)
		{
			ButtonHovering = new(Width, Height);
			ButtonClicked = new(Width, Height);
			ButtonIdle = new(Width, Height);
			this.OnClick = OnClick;
			this.Radius = Radius;
			this.Height = Height;
			this.Width = Width;
			this.Text = Text;
		}

		#region Methods

		public void Render()
		{
			// Define start & end colors for gradients.
			Color StartColor = "#EAEAEA";
			Color EndColor = "#979797";

			// Clear each image to be transparent.
			ButtonHovering.Clear(Color.Transparent);
			ButtonClicked.Clear(Color.Transparent);
			ButtonIdle.Clear(Color.Transparent);

			// Draw the base of the button.
			ButtonHovering.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);
			ButtonClicked.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);
			ButtonIdle.DrawFilledRectangle(0, 0, Width, Height, Radius, Color.White);

			// Generate gradients for each button.
			Gradient GradientHovering = new(Width, Height, StartColor - 32f, EndColor);
			Gradient GradientClicked = new(Width, Height, EndColor, StartColor);
			Gradient GradientIdle = new(Width, Height, StartColor, EndColor);

			// Mash each button image to apply the gradient & draw it onto the images.
			ButtonHovering.DrawImage(0, 0, Filters.MaskAlpha(ButtonHovering, GradientHovering));
			ButtonClicked.DrawImage(0, 0, Filters.MaskAlpha(ButtonClicked, GradientClicked));
			ButtonIdle.DrawImage(0, 0, Filters.MaskAlpha(ButtonIdle, GradientIdle));

			// Draw the text onto the images.
			ButtonHovering.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
			ButtonClicked.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
			ButtonIdle.DrawString(Width / 2, Height / 2, Text, default, Color.Black, true);
		}

		#endregion

		#region Fields

		public Graphics ButtonHovering;
		public Graphics ButtonClicked;
		public Graphics ButtonIdle;
		public Action OnClick;
		public ushort Radius;
		public ushort Height;
		public ushort Width;
		public string Text;
		public int X;
		public int Y;

		#endregion
	}
}