namespace PrismGraphics.Animators
{
	/// <summary>
	/// The class used to create color fading animations.
	/// WIP : Not working properly!
	/// </summary>
	public class FadeControler
	{
		/// <summary>
		/// Creates a new instance of the <see cref="FadeControler"/> class.
		/// </summary>
		/// <param name="BeginColor">The color to start fading from.</param>
		/// <param name="TargetColor">The color to fade to.</param>
		/// <param name="Duration">The duration to fade the colors.</param>
		/// <param name="Mode">The mode to fade the colors.</param>
		public FadeControler(Color BeginColor, Color TargetColor, TimeSpan Duration, FadeMode Mode = FadeMode.Linear)
		{
			// Assign public API fields.
			this.BeginColor = BeginColor;
			this.TargetColor = TargetColor;
			this.Duration = Duration;
			this.Mode = Mode;
			IsEnabled = false;

			// Assign private API fields.
			StepA = (TargetColor.A - BeginColor.A) / (Duration.Milliseconds * 1000000);
			StepR = (TargetColor.R - BeginColor.R) / (Duration.Milliseconds * 1000000);
			StepG = (TargetColor.G - BeginColor.G) / (Duration.Milliseconds * 1000000);
			StepB = (TargetColor.B - BeginColor.B) / (Duration.Milliseconds * 1000000);
			A = BeginColor.A;
			R = BeginColor.R;
			G = BeginColor.G;
			B = BeginColor.B;

			// Adjust step deltas based on fade mode.
			switch (Mode)
			{
				case FadeMode.FastInSlowOut:
					Duration /= 2;
					StepA *= 2;
					StepR *= 2;
					StepG *= 2;
					StepB *= 2;
					break;
				case FadeMode.FastOutSlowIn:
					Duration *= 2;
					StepA /= 2;
					StepR /= 2;
					StepG /= 2;
					StepB /= 2;
					break;
			}

			// Create an interupt timer.
			Cosmos.HAL.Global.PIT.RegisterTimer(new(Next, (ulong)((TargetColor - BeginColor).Brightness / (Duration.TotalMilliseconds * 1000000)), true));
		}

		#region Properties

		/// <summary>
		/// The current color of the fade, will always be present.
		/// </summary>
		public Color ResultColor
		{
			get
			{
				return ((byte)A, (byte)R, (byte)G, (byte)B);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Restarts the interpolation, starts it if it was stopped.
		/// </summary>
		public void Restart()
		{
			// Reset the result color value.
			A = BeginColor.A;
			R = BeginColor.R;
			G = BeginColor.G;
			B = BeginColor.B;

			// Enable the animator.
			IsEnabled = true;
		}

		/// <summary>
		/// Resets the interpolation, does not change playing state.
		/// </summary>
		public void Reset()
		{
			// Reset the result color value.
			A = BeginColor.A;
			R = BeginColor.R;
			G = BeginColor.G;
			B = BeginColor.B;
		}

		/// <summary>
		/// Starts the interpolation, doesn't reset the interpolation.
		/// </summary>
		public void Start()
		{
			IsEnabled = true;
		}

		/// <summary>
		/// Pauses the interpolation, doesn't reset the interpolation.
		/// </summary>
		public void Pause()
		{
			IsEnabled = false;
		}

		/// <summary>
		/// Stops the interpolation, resets the interpolation and pauses it.
		/// </summary>
		public void Stop()
		{
			// Reset the result color value.
			A = BeginColor.A;
			R = BeginColor.R;
			G = BeginColor.G;
			B = BeginColor.B;

			// Disable the animator.
			IsEnabled = false;
		}

		/// <summary>
		/// Updates the color to the next of the interpolation.
		/// </summary>
		private void Next()
		{
			// Return if no operation needs to be done.
			if (!IsEnabled)
			{
				return;
			}
			if (ResultColor == TargetColor)
			{
				IsEnabled = false;
				return;
			}

			A += (byte)StepA;
			R += (byte)StepR;
			G += (byte)StepG;
			B += (byte)StepB;
		}

		#endregion

		#region Fields

		// Public API fields.
		public Color BeginColor;
		public Color TargetColor;
		public TimeSpan Duration;
		public FadeMode Mode;
		public bool IsEnabled;

		// Private API fields.
		private readonly double StepA;
		private readonly double StepR;
		private readonly double StepG;
		private readonly double StepB;
		private double A, R, G, B;

		#endregion
	}
}