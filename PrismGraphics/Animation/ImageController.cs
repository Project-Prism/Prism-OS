namespace PrismGraphics.Animation;

/// <summary>
/// The image fade controller class, used to fade image brightness.
/// </summary>
public class ImageController
{
	/// <summary>
	/// Creates a new instance of the <see cref="ImageController"/> class.
	/// </summary>
	/// <param name="Image">The source image.</param>
	/// <param name="Source">The starting brightness. (Between 0.0 and 1.0)</param>
	/// <param name="Target">The target brightness. (Between 0.0 and 1.0)</param>
	/// <param name="Duration">The duration of the animation.</param>
	/// <param name="Mode">The mode of the animation.</param>
	public ImageController(Graphics Image, float Source, float Target, TimeSpan Duration, AnimationMode Mode)
	{
		Animator = new(Source, Target, Duration, Mode);
		this.Source = Image;
	}

	#region Properties

	public Graphics Current
	{
		get
		{
			Graphics Temp = new(Source.Width, Source.Height);

			for (uint I = 0; I < Source.Size; I++)
			{
				Temp[I] = (Color.Normalize(Source[I]) * Animator.Current * 255f).ARGB;
			}

			return Temp;
		}
	}

	#endregion

	#region Fields

	public AnimationController Animator;
	public Graphics Source;

	#endregion
}