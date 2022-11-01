using PrismGL3D.Types;
using PrismGL3D;
using PrismGL2D;
using PrismUI;

namespace PrismOS.Apps
{
	public class GFXTest : Window
	{
		public GFXTest() : base(300, 300, nameof(GFXTest))
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