using PrismAPI.Tools.Extentions;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public class Window : Control
{
	#region Constructors

	public Window(int X, int Y, ushort Width, ushort Height, string Title) : base(X, Y, Width, Height, ThemeStyle.None)
	{
		// Initialize the control list.
		ShelfControls = new();
		Controls = new();

		// Initialize the window's buffers.
		TitleShelf = new(Width, 32);
		WindowBody = new(Width, Height);

		// Initialize the window's title.
		this.Title = Title;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Processes all of the controls in a list and renders them onto a target;
	/// </summary>
	/// <param name="X">The X offset position for mouse targeting.</param>
	/// <param name="Y">The Y offset position for mouse targeting.</param>
	/// <param name="Controls">The controls to process.</param>
	/// <param name="Target">The target to render on.</param>
	internal static void Process(int X, int Y, List<Control> Controls, Canvas Target, ConsoleKeyInfo? Key)
	{
		// Loop over all of the controls to process.
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
				// Assign new click state before click method.
				C.Status = MouseEx.IsClicked ? CursorStatus.Clicked : CursorStatus.Hovering;

				// Check if a click (any kind) has been detected - Only run if button hasn't already been clicked last frame.
				if (MouseEx.IsClickFired)
				{
					C.OnClick(X - (int)MouseManager.X, Y - (int)MouseManager.Y, MouseManager.LastMouseState);
				}
			}
			else
			{
				// Set the control's status to idle and continue - nothing is happening.
				C.Status = CursorStatus.Idle;
			}

			// Execute the control's key method if it was detected.
			if (Key != null)
			{
				C.OnKey(Key.Value);
			}

			// Update the control onto the window body.
			C.Update(Target);
		}
	}

	/// <summary>
	/// Updates the window and it's controls, then coppies itself to the target canvas.
	/// </summary>
	/// <param name="Canvas">The canvas to render on.</param>
	public override void Update(Canvas Canvas)
	{
		// Resize if needed.
		TitleShelf.Width = Width;
		WindowBody.Height = Height;
		WindowBody.Width = Width;

		// Draw the window back panel.
		TitleShelf.Clear(Foreground + 32);
		WindowBody.Clear(Background);

		// Try to read the current key - null if no key is pressed.
		ConsoleKeyInfo? Key = KeyboardEx.ReadKey();

		// Process all controls for the window body and shelf.
		Process(X, Y - 32, ShelfControls, TitleShelf, Key);
		Process(X, Y, Controls, WindowBody, Key);

		// Draw with background color because title bar is already dark.
		TitleShelf.DrawString(TitleShelf.Width / 2, TitleShelf.Height / 2, Title, default, Background, true);

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