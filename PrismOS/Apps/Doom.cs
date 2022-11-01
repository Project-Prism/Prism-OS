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

		public List<bool[]> Map = new()
		{
			new bool[] { true, true, true, true, true, true, true, true, true, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, false, false, false, false, false, false, false, false, true },
			new bool[] { true, true, true, true, true, true, true, true, true, true },
		};
		public Engine Engine;

		#endregion
	}
}