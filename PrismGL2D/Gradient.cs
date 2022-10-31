namespace PrismGL2D
{
	/// <summary>
	/// Gradient class, used for generating gradients.
	/// INCOMPLETE/BROKEN: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
	/// </summary>
	public class Gradient : Graphics
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
					this[Y, X] = Lerp(Colors[0], Colors[1], Normalize(Y, 0, Height));
				}
			}
		}

		/// <summary>
		/// Normalizes a value to be inbetween 0.0 and 1.1.
		/// </summary>
		/// <param name="Value">Index inbetween Min and Max.</param>
		/// <param name="Minimum">Minimum value.</param>
		/// <param name="Maximum">Maximum value.</param>
		/// <returns>Narmalized value.</returns>
		private static double Normalize(long Minimum, long Maximum, long Value)
		{
			if (Value >= Maximum)
			{
				return 1.0;
			}
			if (Value <= Minimum)
			{
				return 0.0;
			}

			return (Value - Minimum) / (Maximum - Minimum);
		}
		/// <summary>
		/// Lerp from one point in a gradient to another.
		/// </summary>
		/// <param name="C1">Color 1 to lerp.</param>
		/// <param name="C2">Color 2 to lerp.</param>
		/// <param name="Value">0.0-1.0</param>
		/// <returns>Mixed color at index.</returns>
		internal static Color Lerp(Color C1, Color C2, double Value)
		{
			if (Value < 0.0 || Value > 1.0)
			{
				throw new IndexOutOfRangeException("'Value' expects 0.0-1.0.");
			}

			return C1 + (C2 - C1) * Value;
		}
		/// <summary>
		/// Lerp from one point in a gradient to another.
		/// </summary>
		/// <param name="Colors">Colors to lerp.</param>
		/// <param name="Value">0.0-1.0</param>
		/// <returns>Mixed color at index.</returns>
		internal static Color Lerp(Color[] Colors, double Value)
		{
			int stopLength = 1 / (Colors.Length - 1);
			double valueRatio = Value / stopLength;
			int stopIndex = (int)Math.Floor(valueRatio);

			if (stopIndex == (Colors.Length - 1))
			{
				return Colors[^1];
			}
			double stopFraction = valueRatio % 1;
			return Lerp(Colors[stopIndex], Colors[stopIndex + 1], stopFraction);
		}
	}
}