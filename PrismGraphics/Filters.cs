namespace PrismGraphics
{
	/// <summary>
	/// The <see cref="Filters"/> class, used to apply advanced filter effects to a graphics canvas.
	/// Only use this on the base <see cref="Graphics"/> class to avoid memory issues.
	/// </summary>
	public static class Filters
	{
		/// <summary>
		/// Rotates the image to the desired angle.
		/// </summary>
		/// <param name="Angle">Angle to rotate in.</param>
		/// <param name="G">The canvas to filter.</param>
		/// <returns>Filtered canvas image.</returns>
		public static Graphics Rotate(double Angle, Graphics G)
		{
			// Create temporary buffer.
			Graphics Result = new(G.Width, G.Height);

			for (int X = 0; X < G.Width; X++)
			{
				for (int Y = 0; Y < G.Height; Y++)
				{
					int X2 = (int)(Math.Cos(Angle) * X - Math.Sin(Angle) * Y);
					int Y2 = (int)(Math.Sin(Angle) * X + Math.Cos(Angle) * Y);

					Result[X2, Y2] = G[X, Y];
				}
			}

			// Return filtered image.
			return Result;
		}

		/// <summary>
		/// Re-scales the image to the desired size.
		/// </summary>
		/// <param name="Width">New width to scale to.</param>
		/// <param name="Height">New height to scale to.</param>
		/// <param name="G">The canvas to filter.</param>
		/// <returns>Filtered canvas image.</returns>
		/// <exception cref="NotImplementedException">Thrown if scale method does not exist.</exception>
		public static Graphics Scale(ushort Width, ushort Height, Graphics G)
		{
			// Out of bounds check.
			if (Width <= 0 || Height <= 0 || Width == G.Width || Height == G.Height)
			{
				return G;
			}

			// Create a temporary buffer.
			Graphics Result = new(Width, Height);

			// Find the scale ratios.
			double XRatio = (double)G.Width / Width;
			double YRatio = (double)G.Height / Height;

			for (uint Y = 0; Y < Height; Y++)
			{
				double PY = Math.Floor(Y * YRatio);

				for (uint X = 0; X < Width; X++)
				{
					double PX = Math.Floor(X * XRatio);

					Result[Y * Width + X] = G[(uint)((PY * G.Width) + PX)];
				}
			}

			// Return filtered image.
			return Result;
		}

		/// <summary>
		/// Applies a basic anti-aliasing filter to the graphics layer.
		/// Warning: This method is somewhat slow.
		/// </summary>
		/// <param name="G">The canvas to filter.</param>
		/// <returns>Filtered canvas image.</returns>
		public static Graphics ApplyAA(Graphics G)
		{
			// Create temporary graphics buffer.
			Graphics Result = new(G.Width, G.Height);

			// Loop over all pixels.
			for (uint I = (uint)(G.Width + 1); I < G.Size - G.Width - 1; I++)
			{
				// Skip the left and right edges of the frame buffer.
				if (I % G.Width == 0 || I % G.Width == G.Width - 1)
				{
					continue;
				}

				Color Average = G[I] / 3; // Center point.

				Average += G[I - G.Width] / 6; // Top.
				Average += G[I + G.Width] / 6; // Bottom.
				Average += G[I - 1] / 6; // Right.
				Average += G[I + 1] / 6; // Left.

				// Draw the average on to the buffer.
				Result[I] = Average;
			}

			// Return filtered image.
			return Result;
		}
	}
}