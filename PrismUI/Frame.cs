using Cosmos.System;
using Cosmos.Core;
using PrismGL2D;

namespace PrismUI
{
	public class Frame : Control
	{
		public Frame(uint Width, uint Height, string Title)
		{
			this.Height = Height;
			this.Width = Width;
			Y = (int)((VBE.getModeInfo().height / 2) - (Height / 2) + (Frames.Count * Config.Scale));
			X = (int)((VBE.getModeInfo().width / 2) - (Width / 2) + (Frames.Count * Config.Scale));

			Icon = new(Config.Scale, Config.Scale);
			Icon.Clear(Color.Red);
			Controls = new();
			Text = Title;
			Frames.Add(this);
		}
		public Frame (string Title)
		{
			Height = (uint)(VBE.getModeInfo().height / 2);
			Width = (uint)(VBE.getModeInfo().width / 2);
			Y = (int)(Height - (Height / 2) + (Frames.Count * Config.Scale));
			X = (int)(Width - (Width / 2) + (Frames.Count * Config.Scale));

			Icon = new(Config.Scale, Config.Scale);
			Icon.Clear(Color.Red);
			Controls = new();
			Text = Title;
			Frames.Add(this);
		}
		public Frame()
		{
			Height = (uint)(VBE.getModeInfo().height / 2);
			Width = (uint)(VBE.getModeInfo().width / 2);
			Y = (int)(Height - (Height / 2) + (Frames.Count * Config.Scale));
			X = (int)(Width - (Width / 2) + (Frames.Count * Config.Scale));

			Icon = new(Config.Scale, Config.Scale);
			Icon.Clear(Color.Red);
			Controls = new();
			Text = "Frame 1";
			Frames.Add(this);
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

		public override void OnDrawEvent(Control C)
		{
			OnDrawEvent(C);
		}
		public void OnDrawEvent(Graphics G)
		{ // tbh i do not really understand my logic behind this, it needs to be re-done eventualy but for the time being, it works okay enough.
			base.OnDrawEvent(this);

			if (CanInteract)
			{
				if (MouseManager.MouseState == MouseState.Left)
				{
					if (MouseManager.X >= X && MouseManager.X <= X + Width && MouseManager.Y >= Y && MouseManager.Y <= Y + Config.Scale && !IsMoving && !Dragging)
					{
						Dragging = true;
						Select();
						IsMoving = true;
						IX = (int)MouseManager.X - X;
						IY = (int)MouseManager.Y - Y;
					}
				}
				else
				{
					Dragging = false;
					IsMoving = false;
				}
				if (IsMoving)
				{
					X = (int)MouseManager.X - IX;
					Y = (int)MouseManager.Y - IY;
				}
			}
			if (IsEnabled)
			{
				// Draw Title Bar
				if (HasBorder)
				{
					DrawFilledRectangle(0, 0, Width, Config.Scale, Config.Radius, Config.AccentColor);
					DrawString((int)(Width / 2), (int)(Config.Scale / 2), Text, Config.Font, Config.GetForeground(false, false), true);
				}

				foreach (Control C in Controls)
				{
					if (C.IsEnabled)
					{
						if (C.CanInteract)
						{
							if (MouseManager.X >= X + C.X && MouseManager.X <= X + C.X + C.Width && MouseManager.Y >= Y + C.Y && MouseManager.Y <= Y + C.Y + C.Height && (this == Frames[^1] || !CanInteract))
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
						}

						C.OnDrawEvent(this);
					}
				}

				// Draw Surrounding Rectangle
				if (HasBorder)
				{
					DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.GetForeground(false, false));
				}

				G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
			}
		}

		#region Methods

		/// <summary>
		/// Shows the control.
		/// </summary>
		public override void Show()
		{
			Frames.Add(this);
		}
		/// <summary>
		/// Hides the control.
		/// </summary>
		public override void Hide()
		{
			Frames.Remove(this);
		}
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
		/// The icon to be displayed in the taskbar if ShowInTaskbar is true.
		/// </summary>
		public Graphics Icon;
		/// <summary>
		/// The temporary variable to check if the windoww is moving already.
		/// </summary>
		public bool IsMoving;
		/// <summary>
		/// Buffer X for dragging.
		/// </summary>
		public int IX;
		/// <summary>
		/// Buffer Y for dragging.
		/// </summary>
		public int IY;

		#endregion
	}
}