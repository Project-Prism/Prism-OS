using PrismUI.Controls;
using PrismGL3D.Types;
using PrismGL3D;
using PrismGL2D;

namespace PrismOS.Apps
{
	public class Test3D : Frame
	{
		public Test3D() : base(300, 300, nameof(Test3D))
		{
			Engine = new(Width, Height - Config.Scale, 90);
			Cube = Mesh.GetCube(300, 300, 100);
			Engine.Objects.Add(Cube);

			OnDrawEvent = (Graphics G) => Next(G);
		}

		public Engine Engine;
		public Mesh Cube;

		public void Next(Graphics G)
		{
			Cube.TestLogic(0.01);
			Engine.Render();
			G.DrawImage(0, (int)Config.Scale, Engine, false);
		}
	}
}