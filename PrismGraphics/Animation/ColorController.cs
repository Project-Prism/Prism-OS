namespace PrismGraphics.Animation;

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
		Alpha = new(Source.A, Target.A, Duration, Mode);
		Red = new(Source.R, Target.R, Duration, Mode);
		Green = new(Source.G, Target.G, Duration, Mode);
		Blue = new(Source.B, Target.B, Duration, Mode);
	}

	#region Properties

	/// <summary>
	/// A bool to change if the animation is looping/continuous.
	/// </summary>
	public bool IsContinuous
	{
		get
		{
			return Alpha.IsContinuous;
		}
		set
		{
			Alpha.IsContinuous = value;
			Red.IsContinuous = value;
			Green.IsContinuous = value;
			Blue.IsContinuous = value;
		}
	}

	/// <summary>
	/// A boolean to tell if the animation has finished.
	/// </summary>
	public bool IsFinished => Alpha.IsFinished;

	/// <summary>
	/// The animation mode used for color transition.
	/// </summary>
	public AnimationMode Mode
	{
		get
		{
			return Alpha.Mode;
		}
		set
		{
			Alpha.Mode = value;
			Red.Mode = value;
			Green.Mode = value;
			Blue.Mode = value;
		}
	}

	/// <summary>
	/// The total elapsed time of the animation.
	/// </summary>
	public float ElapsedTime
	{
		get
		{
			return Alpha.ElapsedTime;
		}
		set
		{
			Alpha.ElapsedTime = value;
			Red.ElapsedTime = value;
			Green.ElapsedTime = value;
			Blue.ElapsedTime = value;
		}
	}

	/// <summary>
	/// The duration in which to play the animation over.
	/// </summary>
	public TimeSpan Duration
	{
		get
		{
			return Alpha.Duration;
		}
		set
		{
			Alpha.Duration = value;
			Red.Duration = value;
			Green.Duration = value;
			Blue.Duration = value;
		}
	}

	/// <summary>
	/// The current color defined by the animation.
	/// </summary>
	public Color Current
	{
		get
		{
			return ((byte)Alpha.Current, (byte)Red.Current, (byte)Green.Current, (byte)Blue.Current);
		}
		set
		{
			Alpha.Current = value.A;
			Red.Current = value.R;
			Green.Current = value.G;
			Blue.Current = value.B;
		}
	}

	/// <summary>
	/// The source color of the animation.
	/// </summary>
	public Color Source
	{
		get
		{
			return ((byte)Alpha.Source, (byte)Red.Source, (byte)Green.Source, (byte)Blue.Source);
		}
		set
		{
			Alpha.Source = value.A;
			Red.Source = value.R;
			Green.Source = value.G;
			Blue.Source = value.B;
		}
	}

	/// <summary>
	/// The target color of the animation.
	/// </summary>
	public Color Target
	{
		get
		{
			return ((byte)Alpha.Target, (byte)Red.Target, (byte)Green.Target, (byte)Blue.Target);
		}
		set
		{
			Alpha.Target = value.A;
			Red.Target = value.R;
			Green.Target = value.G;
			Blue.Target = value.B;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Resets the progress and plays the new values if new ones were set.
	/// </summary>
	public void Reset()
	{
		Alpha.Reset();
		Red.Reset();
		Green.Reset();
		Blue.Reset();
	}

	#endregion

	#region Fields

	/// <summary>
	/// A color controler for the animation.
	/// </summary>
	private readonly AnimationController Alpha, Red, Green, Blue;

	#endregion
}