using PrismAPI.Tools.Extentions;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public class Window : Control
{
	#region Constructors

	public Window(int X, int Y, ushort Width, ushort Height, string Title) : base(Width, Height)
	{
		// Initialize the control list.
		ShelfControls = new();
		Controls = new();

		// Initialize the window's buffers.
		TitleShelf = new(Width, 32);
		WindowBody = new(Width, Height);

		// Initialize the window fields.
		this.Title = Title;
		this.X = X;
		this.Y = Y;
	}

	#endregion

	#region Methods

	public override void Update(Canvas Canvas)
	{
		// Resize if needed.
		TitleShelf.Width = Width;
		WindowBody.Height = Height;
		WindowBody.Width = Width;

		// Draw the window back panel.
		TitleShelf.Clear(Color.DeepGray);
		WindowBody.Clear(Color.White);

		// Draw the cache of each control for the title shelf.
		foreach (Control C in ShelfControls)
		{
			C.Update(TitleShelf);
		}

		// Draw the window's title on the shelf.
		TitleShelf.DrawString(TitleShelf.Width / 2, TitleShelf.Height / 2, Title, default, Color.White, true);

		// Draw the cache of each control.
		foreach (Control C in Controls)
		{
			// Check if the control is even enabled - skip if not.
			if (!C.IsEnabled)
			{
				continue;
			}

			// Check if the mouse is hovering over the control.
			if (MouseEx.IsMouseWithin(X + C.X, Y + C.Y, C.Width, C.Height))
			{
				// Set the cursor's status to hovering.
				C.Status = CursorStatus.Hovering;

				// Check if a click (any kind) has been detected.
				if (MouseManager.MouseState != MouseManager.LastMouseState)
				{
					// Assign new click state before click method.
					C.Status = CursorStatus.Clicked;

					// Execute the control's click method.
					C.OnClick(X - (int)MouseManager.X, Y - (int)MouseManager.Y, MouseManager.LastMouseState);
				}
			}
			else
			{
				// Set the control's status to idle - nothing is happening.
				C.Status = CursorStatus.Idle;
			}

			// Update the control onto the window body.
			C.Update(WindowBody);
		}

		// Draw the window to the buffer.
		Canvas.DrawImage(X, Y - 32, TitleShelf, Radius != 0);
		Canvas.DrawImage(X, Y, WindowBody, Radius != 0);
	}

	#endregion

	#region Fields

	public readonly List<Control> ShelfControls;
	public readonly List<Control> Controls;
	private readonly Canvas TitleShelf;
	private readonly Canvas WindowBody;
	internal bool IsMoving;
	public string Title;
	internal int IX;
	internal int IY;

	#endregion
}