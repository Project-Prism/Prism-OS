using PrismAPI.Graphics.Animation;

namespace PrismAPI.UI
{
    public class Animation
    {
		public Animation(int X, int Y, int Width, int Height)
		{
			AnimateHeight = new(0, Height, new(0, 0, 5), AnimationMode.Ease);
			AnimateWidth = new(0, Width, new(0, 0, 5), AnimationMode.Ease);
			AnimateX = new(0, X, new(0, 0, 5), AnimationMode.Ease);
			AnimateY = new(0, Y, new(0, 0, 5), AnimationMode.Ease);
		}

		public void Play()
		{
			AnimateHeight.Play();
			AnimateWidth.Play();
			AnimateX.Play();
			AnimateY.Play();
		}

		public void Pause()
		{
			AnimateHeight.Pause();
			AnimateWidth.Pause();
			AnimateX.Pause();
			AnimateY.Pause();
		}

		public void Stop()
		{
			AnimateHeight.Stop();
			AnimateWidth.Stop();
			AnimateX.Stop();
			AnimateY.Stop();
		}

		public AnimationController AnimateHeight;
		public AnimationController AnimateWidth;
		public AnimationController AnimateX;
		public AnimationController AnimateY;
	}
}