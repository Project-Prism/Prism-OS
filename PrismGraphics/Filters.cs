namespace PrismGraphics;

/// <summary>
/// The <see cref="Filters"/> class, used to apply advanced filter effects to a graphics canvas.
/// Only use this on the base <see cref="Graphics"/> class to avoid memory issues.
/// </summary>
public static class Filters
{
	/// <summary>
	/// Samples/Crops a graphics item.
	/// </summary>
	/// <param name="X">The X position to start at.</param>
	/// <param name="Y">The Y position to start at.</param>
	/// <param name="Width">The Width to sample.</param>
	/// <param name="Height">The Height to sample.</param>
	/// <param name="G">The graphics to sample.</param>
	/// <returns>A sampled image.</returns>
	public static Graphics Sample(int X, int Y, ushort Width, ushort Height, Graphics G)
	{
		// Create temporary graphics object.
		Graphics Temp = new(Width, Height);

		// Loop over each pixel, and loop around when end is reached
		for (int IX = 0; IX < Width; IX++)
		{
			for (int IY = 0; IY < Height; IY++)
			{
				// Get the source coordinates.
				int GX = (X + IX) % G.Width;
				int GY = (Y + IY) % G.Height;

				// Get the looped pixel from the source & copy it to the destionation.
				Temp[IX, IY] = G[GX, GY];
			}
		}

		// Return filtered image.
		return Temp;
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
	/// Masks the gradient over anything in the input surface that isn't alpha.
	/// </summary>
	/// <param name="Input">The input canvas to mask.</param>
	/// <param name="Mask">The mask to use on top of the input.</param>
	/// <returns>A masked canvas.</returns>
	public static Graphics MaskAlpha(Graphics Input, Graphics Mask)
	{
		// Get a scaled version if the gradient is smaller or bigger than the input.
		Graphics Scaled = Scale(Input.Width, Input.Height, Mask);

		// Create a temporary buffer.
		Graphics Temp = new(Input.Width, Input.Height);

		// Loop over every pixel.
		for (uint I = 0; I < Input.Size; I++)
		{
			// Skip if pixel is alpha.
			if (Input[I].A < 255)
			{
				continue;
			}

			// Set gradient pixel.
			Temp[I] = Scaled[I];
		}

		return Temp;
	}

	/// <summary>
	/// Rotates the image to the desired angle.
	/// </summary>
	/// <param name="Angle">Angle to rotate in.</param>
	/// <param name="G">The canvas to filter.</param>
	/// <returns>Filtered canvas image.</returns>
	public static Graphics Rotate(double Angle, Graphics G)
	{
		// Define temporary canvas object.
		Graphics Result;

		// Check if rotation can be done faster.
		switch (Angle % 360)
		{
			case 360:
			case 0:
				return G;
			case 90:
				Result = new(G.Height, G.Width);

				// Loop over each pixel...
				for (int X = 0; X < G.Width; X++)
				{
					for (int Y = 0; Y < G.Height; Y++)
					{
						// Just swap X and Y for the effect.
						Result[Y, X] = G[X, Y];
					}
				}

				return Result;
			case -90:
				Result = new(G.Height, G.Width);

				// Loop over each pixel...
				for (int X = 0; X < G.Width; X++)
				{
					for (int Y = 0; Y < G.Height; Y++)
					{
						// Just swap X and Y for the effect.
						Result[-Y, -X] = G[X, Y];
					}
				}

				return Result;
			case 180:
				Result = new(G.Width, G.Height);

				// Loop over each pixel...
				for (uint I = 0; I < G.Size; I++)
				{
					// Copy the pixels in reverse order.
					Result[G.Size - I] = G[I];
				}

				return Result;
		}

		// Create temporary buffer.
		Result = new(G.Width, G.Height);

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