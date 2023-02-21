namespace PrismGraphics.Animation
{
	/// <summary>
	/// This is an enum to keep a list of all the possible fade types.
	/// </summary>
	public enum AnimationMode
	{
		/// <summary>
		/// This mode fades a color in fast but slows down the closer it gets to the target color.
		/// </summary>
		EaseOut,
		EaseIn,
		Ease,
	}
}