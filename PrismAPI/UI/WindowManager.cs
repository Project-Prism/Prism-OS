using PrismAPI.UI.Config;
using PrismAPI.Graphics;

namespace PrismAPI.UI;

public static class WindowManager
{
	#region Constructors

	static WindowManager()
	{
		Windows = new();
	}

	#endregion

	#region Methods

	public static void Update(Canvas Canvas)
	{
		foreach (Window W in Windows)
		{
			W.Update(Canvas, CursorStatus.Idle);
		}
	}

	#endregion

	#region Fields

	public static List<Window> Windows;

	#endregion
}