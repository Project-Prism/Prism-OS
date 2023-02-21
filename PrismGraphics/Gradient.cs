using PrismGraphics.Animation;

namespace PrismGraphics
{
	/// <summary>
	/// Gradient class, used for generating gradients.
	/// INCOMPLETE/BROKEN: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
	/// </summary>
	public unsafe class Gradient : Graphics
	{
		/// <summary>
		/// Create a new instance of the <see cref="Gradient"/> class.
		/// </summary>
		/// <param name="Width">Width (in pixels) of the gradient.</param>
		/// <param name="Height">Height (in pixels) of the gradient.</param>
		/// <param name="Start">The starting color.</param>
		/// <param name="End">The end color.</param>
		public Gradient(ushort Width, ushort Height, Color Start, Color End) : base(Width, Height)
		{
			for (int I = 0; I < Height; I++)
			{
				DrawFilledRectangle(0, I, Width, 1, 0, Common.Lerp(Start, End, 1.0f / Height * I));
			}
		}

		#region Methods

		/// <summary>
		/// Masks the gradient over anything in the input panel that isn't alpha.
		/// </summary>
		/// <param name="G">The input canvas to mask.</param>
		/// <returns>A masked canvas.</returns>
		public Graphics MaskAlpha(Graphics G)
		{
			// Get a scaled version if the gradient is smaller or bigger than the input.
			Gradient Scaled = (Gradient)Scale(G.Width, G.Height);

			// Create a temporary buffer.
			Graphics Temp = new(G.Width, G.Height);

			// Loop over every pixel.
			for (uint I = 0; I < G.Size; I++)
			{
				// Skip if pixel is alpha.
				if (G[I].A == 0)
				{
					continue;
				}

				// Set gradient pixel.
				Temp[I] = Scaled[I];
			}

			return Temp;
		}

		#endregion
	}
}