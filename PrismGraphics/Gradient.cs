namespace PrismGraphics
{
	/// <summary>
	/// Gradient class, used for generating gradients.
	/// INCOMPLETE/BROKEN: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
	/// </summary>
	public unsafe class Gradient : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Gradient"/> class.
		/// </summary>
		/// <param name="Width">Width of the gradient.</param>
		/// <param name="Height">Height of the gradient.</param>
		/// <param name="Colors">Colors to use.</param>
		public Gradient(uint Width, uint Height, Color[] Colors) : base(Width, Height)
		{
			for (int Y = 0; Y < Height; Y++)
			{
				for (int X = 0; X < Width; X++)
				{
					this[X, Y] = InterpolateColors(Colors, 1 / Height * Y);
				}
			}
		}

		/// <summary>
		/// Interpolate between several colors in a normal going from 0.0 to 1.0.
		/// </summary>
		/// <param name="Colors">Colors to interpolate.</param>
		/// <param name="Normal">Index between 0.0 and 1.1.</param>
		/// <returns>Interpolated color.</returns>
		internal static Color InterpolateColors(Color[] Colors, double Normal)
		{ 
			double valueRatio = Normal / (1 / (Colors.Length - 1));
			double stopIndex = Math.Floor(valueRatio);
			if (stopIndex == (Colors.Length - 1))
			{
				return Colors[^1];
			}
			double stopFraction = valueRatio % 1;
			return InterpolateColor(Colors[(int)stopIndex], Colors[(int)stopIndex + 1], stopFraction);
		}
		/// <summary>
		/// Interpolate between two colors in a normal going from 0.0 to 1.0.
		/// </summary>
		/// <param name="C1">Color #1.</param>
		/// <param name="C2">Color #1.</param>
		/// <param name="Normal">Index between 0.0 and 1.0.</param>
		/// <returns>Interpolated color.</returns>
		internal static Color InterpolateColor(Color C1, Color C2, double Normal)
		{
			if (Normal > 1)
			{
				return C2;
			}
			if (Normal < 0)
			{
				return C1;
			}

			return new()
			{
				A = (byte)(C1.A + (C2.A - C1.A) * Normal),
				R = (byte)(C1.R + (C2.R - C1.R) * Normal),
				G = (byte)(C1.G + (C2.G - C1.G) * Normal),
				B = (byte)(C1.B + (C2.B - C1.B) * Normal),
			};
		}
	}
}