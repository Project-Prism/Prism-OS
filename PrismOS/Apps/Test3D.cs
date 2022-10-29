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
		}

		public Engine Engine;
		public Mesh Cube;

		public override void Update(Graphics G)
		{
			base.Update(G);

			Cube.TestLogic(0.01);
			Engine.Render();
			DrawImage(0, (int)Config.Scale, Engine, false);
		}
	}
}