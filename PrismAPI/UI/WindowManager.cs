using PrismAPI.Tools.Extentions;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public static class WindowManager
{
	#region Constructors

	static WindowManager()
	{
		Widgets = new();
		Windows = new();
	}

	#endregion

	#region Methods

	public static void Update(Canvas Canvas)
	{
		// Process all widgets.
		Window.Process(0, 0, Widgets, Canvas, null);

		foreach (Window W in Windows)
		{
			if (MouseManager.MouseState != MouseState.Left)
			{
				IsDragging = false;
				W.IsMoving = false;
			}

			if (MouseEx.IsMouseWithin(W.X, W.Y - 32, W.Width, 32) && !W.IsMoving && !IsDragging)
				{
				// Move this window to first priority.
				Windows.Remove(W);
				Windows.Insert(Windows.Count, W);

				W.IX = (int)MouseManager.X - W.X;
				W.IY = (int)MouseManager.Y - W.Y;
				IsDragging = true;
				W.IsMoving = true;
			}

			if (W.IsMoving)
			{
				W.X = (int)MouseManager.X - W.IX;
				W.Y = (int)MouseManager.Y - W.IY;
			}

			W.Update(Canvas);
		}
	}

	#endregion

	#region Fields

	public static List<Control> Widgets;
	public static List<Window> Windows;

	internal static bool IsDragging;

	#endregion
}