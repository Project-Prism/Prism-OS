using Cosmos.System;

namespace PrismGL2D.UI
{
	public class Frame : Control
	{
		public Frame()
		{
			Controls = new();
			HasBorder = true;
		}

		// Window Manager Variables
		public static List<Frame> Frames { get; set; } = new();
		public static bool Dragging { get; set; } = false;

		public override void OnKeyEvent(ConsoleKeyInfo Key)
		{
			switch (Key.Key)
			{
				case ConsoleKey.Enter:
					if (AcceptButton != null)
					{
						AcceptButton.OnKeyEvent(Key);
					}
					break;
				case ConsoleKey.Escape:
					if (CancelButton != null)
					{
						CancelButton.OnKeyEvent(Key);
					}
					break;
			}

			base.OnKeyEvent(Key);
		}

		public override void OnDrawEvent(Graphics Buffer)
		{
			base.OnDrawEvent(this);

			if (Draggable)
			{
				if (MouseManager.MouseState == MouseState.Left)
				{
					if (MouseManager.X >= X && MouseManager.X <= X + Width && MouseManager.Y >= Y && MouseManager.Y <= Y + 20 && !Moving && !Dragging)
					{
						Dragging = true;
						Select();
						Moving = true;
						IX = (int)MouseManager.X - X;
						IY = (int)MouseManager.Y - Y;
					}
				}
				else
				{
					Dragging = false;
					Moving = false;
				}
				if (Moving)
				{
					X = (int)MouseManager.X - IX;
					Y = (int)MouseManager.Y - IY;
				}
			}

			if (Enabled)
			{
				DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.Background);

				// Draw Title Bar
				if (HasBorder)
				{
					DrawFilledRectangle(0, 0, (int)Width, 20, (int)Theme.Radius, Theme.Accent);
					DrawString((int)(Width / 2), 10, Text, Font, Theme.Text, true);
				}

				foreach (Control C in Controls)
				{
					if (C.Enabled)
					{
						if (MouseManager.X >= X + C.X && MouseManager.X <= X + C.X + C.Width && MouseManager.Y >= Y + C.Y && MouseManager.Y <= Y + C.Y + C.Height && (this == Frames[^1] || !Draggable))
						{
							C.IsHovering = true;

							if (MouseManager.MouseState != MouseState.None)
							{
								C.IsPressed = true;
							}
							else if (C.IsPressed)
							{
								C.IsPressed = false;
								C.OnClickEvent(X - (int)MouseManager.X, Y - (int)MouseManager.Y, MouseManager.LastMouseState);
							}
						}
						else
						{
							C.IsHovering = false;
						}

						C.OnDrawEvent(this);
					}
				}

				// Draw Surrounding Rectangle
				if (HasBorder)
				{
					DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Foreground);
				}

				Buffer.DrawImage(X, Y, this, Theme.Radius != 0);
			}
		}

		#region Methods

		/// <summary>
		/// Activates the frame.
		/// </summary>
		public void Select()
		{
			Frames.Remove(this);
			Frames.Add(this);
		}

		/// <summary>
		/// Close the frame.
		/// </summary>
		public void Close()
		{
			Frames.Remove(this);
		}

		#endregion

		#region Fields

		/// <summary>
		/// The controls inside of the frame.
		/// </summary>
		public List<Control> Controls;
		/// <summary>
		/// The button that is fired when the Enter key is pressed.
		/// </summary>
		public Button? AcceptButton;
		/// <summary>
		/// The button that is fired when ESC is pressed.
		/// </summary>
		public Button? CancelButton;
		/// <summary>
		/// Tells the OS wether to show the app in the task bar.
		/// </summary>
		public bool ShowInTaskbar;
		/// <summary>
		/// Check to see if the frame is draggable.
		/// </summary>
		public bool Draggable;
		/// <summary>
		/// The temporary variable to check if the windoww is moving already.
		/// </summary>
		internal bool Moving;
		/// <summary>
		/// The temporary X offset for moving windows.
		/// </summary>
		internal int IX;
		/// <summary>
		/// The temporary Y offset for moving windows.
		/// </summary>
		internal int IY;

		#endregion
	}
}