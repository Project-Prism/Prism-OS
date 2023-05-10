namespace PrismAPI.Graphics.Animation;

/// <summary>
/// The <see cref="ColorController"/> class, used to add basic ease transitions to colors
/// See: <seealso cref="AnimationController"/>
/// </summary>
public class ColorController
{
	/// <summary>
	/// Creates a new instance of the <see cref="ColorController"/> class.
	/// </summary>
	/// <param name="Source">The starting value.</param>
	/// <param name="Target">The value to end at.</param>
	/// <param name="Duration">The duration to play the animation over.</param>
	/// <param name="Mode">The ease animation mode.</param>
	public ColorController(Color Source, Color Target, TimeSpan Duration, AnimationMode Mode)
	{
		// Set-up all controllers for each color channel.
		this.Source = Source;
		this.Target = Target;

		Controller = new(0f, 1f, Duration, Mode);
	}

	#region Properties

	/// <summary>
	/// The current color defined by the animation.
	/// </summary>
	public Color Current => Color.Lerp(Source, Target, Controller.Current);

	#endregion

	#region Fields

	/// <summary>
	/// The controller to control the color interpolation.
	/// </summary>
	public AnimationController Controller;

	/// <summary>
	/// The source color of the animation.
	/// </summary>
	public Color Source;

	/// <summary>
	/// The target color of the animation.
	/// </summary>
	public Color Target;

	#endregion
}