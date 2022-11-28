using PrismTools;
using PrismGL2D;

namespace PrismUI
{
	public static class WindowManager
	{
		#region Methods

		public static void OnUpdate(Graphics G)
		{
			bool CallKeyEvent = KeyboardEx.TryReadKey(out ConsoleKeyInfo Key);
			foreach (Window F in Windows)
			{
				if (!F.IsEnabled)
				{
					continue;
				}

				if (CallKeyEvent && Windows[^1] == F && F.CanType)
				{
					F.OnKey(Key);
				}
				F.OnDraw(G);
			}
		}

		#endregion

		#region Fields

		public static List<Window> Windows { get; set; } = new();

		#endregion
	}
}