using PrismAPI.Tools.Extentions;
using PrismAPI.UI.Config;
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
		foreach (Control W in Widgets)
		{
			// Check if the widget is even enabled - skip if not.
			if (!W.IsEnabled)
            {
                continue;
            }

			// Check if the mouse is hovering over the control.
			if (MouseEx.IsMouseWithin(W.X, W.Y, W.Width, W.Height))
			{
				// Set the cursor's status to hovering.
				W.Status = CursorStatus.Hovering;

				// Check if a click (any kind) has been detected.
				if (MouseManager.MouseState != MouseManager.LastMouseState)
				{
					// Assign new click state before click method.
					W.Status = CursorStatus.Clicked;

					// Execute the control's click method.
					W.OnClick((int)MouseManager.X, (int)MouseManager.Y, MouseManager.LastMouseState);
				}
			}
			else
			{
				// Set the control's status to idle - nothing is happening.
				W.Status = CursorStatus.Idle;
			}

			// Update the widget onto the screen body.
			W.Update(Canvas);
		}

		foreach (Window W in Windows)
		{
			if (MouseManager.MouseState == MouseState.Left)
			{
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
			}
			else
			{
				IsDragging = false;
				W.IsMoving = false;
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