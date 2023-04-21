namespace PrismGraphics.Animation;

/// <summary>
/// This is an enum to keep a list of all the possible fade types.
/// </summary>
public enum AnimationMode
{
	/// <summary>
	/// This mode transitions from the starting value normally but begins to bounce the closer it gets to the end.
	/// </summary>
	BounceOut,
	/// <summary>
	/// This mode transitions from the starting value while bouncing but begins to transition normally the closer it gets to the end.
	/// </summary>
	BounceIn,
	/// <summary>
	/// This mode is a combo of <see cref="BounceIn"/> and <see cref="BounceOut"/>.
	/// </summary>
	Bounce,
	/// <summary>
	/// This mode fades a value in fast but slows down the closer it gets to the target value.
	/// </summary>
	EaseOut,
	/// <summary>
	/// This mode fades a value in slow but speeds the closer it gets to the target value.
	/// </summary>
	EaseIn,
	/// <summary>
	/// This mode is a combo of <see cref="EaseIn"/> and <see cref="EaseOut"/>.
	/// </summary>
	Ease,
	/// <summary>
	/// This mode is a smooth lear update method that has no speed differences.
	/// </summary>
	Linear,
}