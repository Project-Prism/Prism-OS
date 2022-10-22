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
			Cube = new(300, 300, 100);
			Engine.Objects.Add(Cube);

			OnDrawEvent = (Graphics G) => Next(G);
		}

		public Engine Engine;
		public Cube Cube;

		public void Next(Graphics G)
		{
			Cube.TestLogic(0.01);
			Engine.Render();
			G.DrawImage(0, (int)Config.Scale, Engine, false);
		}
	}
}