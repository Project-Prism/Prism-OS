namespace PrismGraphics.Animation;

/// <summary>
/// The commonm operations class, used to store common methods used across all animations.
/// </summary>
public static class Common
{
	/// <summary>
	/// The function used to linearly interpolate between 2 numbers. (64-bit)
	/// </summary>
	/// <param name="StartValue">The number to start with.</param>
	/// <param name="EndValue">The number to end with.</param>
	/// <param name="Index">Any number between 0.0 and 1.0.</param>
	/// <returns>The value between 'StartValue' and 'EndValue' as marked by 'Index'.</returns>
	public static double Lerp(double StartValue, double EndValue, double Index, bool IsClamped = true)
	{
		// Check if clamping is requested.
		if (IsClamped)
		{
			// Ensure 'Index' is between 0.0 and 1.0.
			Index = Math.Clamp(Index, 0.0, 1.0);
		}

		return StartValue + (EndValue - StartValue) * Index;
	}

	/// <summary>
	/// The function used to linearly interpolate between 2 numbers. (32-bit)
	/// </summary>
	/// <param name="StartValue">The number to start with.</param>
	/// <param name="EndValue">The number to end with.</param>
	/// <param name="Index">Any number between 0.0 and 1.0.</param>
	/// <returns>The value between 'StartValue' and 'EndValue' as marked by 'Index'.</returns>
	public static float Lerp(float StartValue, float EndValue, float Index, bool IsClamped = true)
	{
		// Check if clamping is requested.
		if (IsClamped)
		{
			// Ensure 'Index' is between 0.0 and 1.0.
			Index = (float)Math.Clamp(Index, 0.0, 1.0);
		}

		return StartValue + (EndValue - StartValue) * Index;
	}

	/// <summary>
	/// The function to linearly interpolate between 2 colors. (32-bit)
	/// </summary>
	/// <param name="StartValue">The color to start with.</param>
	/// <param name="EndValue">The color to end with.</param>
	/// <param name="Index">Any number between 0.0 and 1.0.</param>
	/// <returns>The value between 'StartValue' and 'EndValue' as marked by 'Index'.</returns>
	public static Color Lerp(Color StartValue, Color EndValue, float Index, bool IsClamped = true)
	{
		// Check if clamping is requested.
		if (IsClamped)
		{
			// Ensure 'Index' is between 0.0 and 1.0.
			Index = (float)Math.Clamp(Index, 0.0, 1.0);
		}

		return new()
		{
			A = (byte)(StartValue.A + (EndValue.A - StartValue.A) * Index),
			R = (byte)(StartValue.R + (EndValue.R - StartValue.R) * Index),
			G = (byte)(StartValue.G + (EndValue.G - StartValue.G) * Index),
			B = (byte)(StartValue.B + (EndValue.B - StartValue.B) * Index),
		};
	}

	/// <summary>
	/// A slightly-advanced function to loop a number around a minimum and max value.
	/// </summary>
	/// <param name="Value">The value to modulate.</param>
	/// <param name="Min">The minimum modulation value.</param>
	/// <param name="Max">The maximum modulation value.</param>
	/// <returns>A modulated value for both forwards and back.</returns>
	public static float Loop(float Value, float Min, float Max)
	{
		float P = Max - Min + 1;

		float Modulated = (Value - Min) % P;

		if (Modulated < 0)
		{
			Modulated += P;
		}

		return Min + Modulated;
	}
}