namespace PrismGraphics.Animators
{
	/// <summary>
	/// This is an enum to keep a list of all the possible fade types.
	/// </summary>
	public enum FadeMode
	{
		/// <summary>
		/// This mode fades a color in fast but slows down the closer it gets to the target color.
		/// </summary>
		FastInSlowOut,
		FastOutSlowIn,
		Linear,
	}
}