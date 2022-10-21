using PrismGL3D.Objects;
using PrismGL3D;
using PrismGL2D;
using PrismUI;

namespace PrismOS.Apps
{
	public class Test3D : Frame
	{
		public Test3D() : base(300, 300, nameof(Test3D))
		{
			Engine = new(Width, Height - Config.Scale, 90);
			Image = new(Width, Height - Config.Scale);
			Cube = new(300, 300, 100);

			Engine.Objects.Add(Cube);
			Controls.Add(Image);
			OnDrawEvent = (Graphics G) => Next();
		}

		public Engine Engine;
		public Image Image;
		public Cube Cube;

		public void Next()
		{
			Cube.TestLogic(0.01);
			Engine.Render();
			Image.Source.DrawImage(0, 0, Engine);
		}
	}
}