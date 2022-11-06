using PrismGL3D.Raycaster;
using PrismGL2D;
using PrismUI;

namespace PrismOS.Apps
{
	public class Doom : Window
	{
		public Doom() : base("Doom")
		{
			Engine = new(Width, Height - Config.Scale, 60);

			OnDrawEvent = (Graphics G) => Next();
			OnKeyEvent = (ConsoleKeyInfo Key) => Engine.Player.Movement(Key, 1.0);
		}

		#region Methods

		public void Next()
		{
			Engine.Render(Map);
			DrawImage(0, (int)Config.Scale, Engine, false);
		}

		#endregion

		#region Fields

		public List<int[]> Map = new()
		{
			new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
		};
		public Engine Engine;

		#endregion
	}
}