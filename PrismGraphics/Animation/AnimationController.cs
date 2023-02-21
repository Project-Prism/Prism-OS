namespace PrismGraphics.Animation
{
	/// <summary>
	/// The <see cref="AnimationController"/> class, used to add basic ease animations or transitions to anything.
	/// See: <seealso href="https://www.febucci.com/2018/08/easing-functions/"/>
	/// </summary>
	public class AnimationController
	{
		/// <summary>
		/// Creates a new instance of the <see cref="AnimationController"/> class.
		/// </summary>
		/// <param name="StartValue">The starting value.</param>
		/// <param name="EndValue">The value to end at.</param>
		/// <param name="Duration">The duration to play the animation over.</param>
		/// <param name="Mode">The ease animation mode.</param>
		public AnimationController(float StartValue, float EndValue, TimeSpan Duration, AnimationMode Mode)
		{
			// Assign internal data.
			FinalValue = StartValue;
			this.StartValue = StartValue;
			this.EndValue = EndValue;
			this.Duration = Duration;
			this.Mode = Mode;

			// Assign the timer.
			Timer = new((object? O) => Next(), null, DelayMS, 0);
		}

		#region Constants

		public const int DelayMS = 50;

		#endregion

		#region Methods

		private static float EaseOut(float T)
		{
			return Flip((float)Math.Pow(Flip(T), 2));
		}

		private static float EaseIn(float T)
		{
			return T * T;
		}

		private static float Ease(float T)
		{
			return Common.Lerp(EaseIn(T), EaseOut(T), T);
		}

		private static float Flip(float X)
		{
			return 1 - X;
		}

		private void Next()
		{
			if (FinalValue == EndValue)
			{
				if (StopWhenFinished)
				{
					return;
				}

				ElapsedTime = 0;
				return;
			}
			
			// Increased the elapsed time.
			ElapsedTime += DelayMS;

			// Set the output value.
			FinalValue = Mode switch
			{
				AnimationMode.EaseOut => Common.Lerp(StartValue, EndValue, EaseOut(ElapsedTime / (float)Duration.TotalMilliseconds)),
				AnimationMode.EaseIn => Common.Lerp(StartValue, EndValue, EaseIn(ElapsedTime / (float)Duration.TotalMilliseconds)),
				AnimationMode.Ease => Common.Lerp(StartValue, EndValue, Ease(ElapsedTime / (float)Duration.TotalMilliseconds)),
				_ => throw new NotImplementedException("That mode isn't implemented!"),
			};
		}

		#endregion

		#region Fields

		public float StartValue, EndValue, FinalValue, ElapsedTime;
		public bool StopWhenFinished;
		public AnimationMode Mode;
		public TimeSpan Duration;
		public Timer Timer;

		#endregion
	}
}