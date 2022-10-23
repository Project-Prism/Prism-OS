using PrismUI.Structure;
using Cosmos.System;
using Cosmos.Core;
using PrismTools;
using PrismGL2D;

namespace PrismUI.Controls
{
	public class Frame : Control
	{
		public Frame(uint Width, uint Height, string Title) : base(Width, Height)
		{
			Y = (int)((VBE.getModeInfo().height / 2) - (Height / 2) + (Frames.Count * Config.Scale));
			X = (int)((VBE.getModeInfo().width / 2) - (Width / 2) + (Frames.Count * Config.Scale));
			Text = Title;
			Generate();
		}
		public Frame(string Title) : base((uint)(VBE.getModeInfo().width / 2), (uint)(VBE.getModeInfo().height / 2))
		{
			Y = (int)(Height - (Height / 2) + (Frames.Count * Config.Scale));
			X = (int)(Width - (Width / 2) + (Frames.Count * Config.Scale));
			Text = Title;
			Generate();
		}

		// Window Manager Variables
		public static List<Frame> Frames { get; set; } = new();
		public static bool Dragging { get; set; } = false;

		internal override void OnKey(ConsoleKeyInfo Key)
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

			base.OnKey(Key);
		}
		internal override void OnDraw(Graphics G)
		{ // tbh i do not really understand my logic behind this, it needs to be re-done eventualy but for the time being, it works okay enough.
			base.OnDraw(G);

			for (int I = 0; I < Controls.Count; I++)
			{
				if (Controls[I].IsEnabled)
				{
					if (Controls[I].CanInteract && CanInteract)
					{
						if (MouseManager.X >= X + Controls[I].X && MouseManager.X <= X + Controls[I].X + Controls[I].Width && MouseManager.Y >= Y + Controls[I].Y && MouseManager.Y <= Y + Controls[I].Y + Controls[I].Height && (this == Frames[^1] || !NeedsFront || !CanInteract))
						{
							if (MouseManager.MouseState != MouseState.None)
							{
								Controls[I].ClickState = ClickState.Clicked;
							}
							else if (Controls[I].ClickState == ClickState.Clicked)
							{
								Controls[I].ClickState = ClickState.Neutral;
								Controls[I].OnClickEvent(X - (int)MouseManager.X, Y - (int)MouseManager.Y, MouseManager.LastMouseState);
							}
							else
							{
								Controls[I].ClickState = ClickState.Hovering;
							}
						}
						else
						{
							Controls[I].ClickState = ClickState.Neutral;
						}
					}
					Controls[I].OnDraw(this);
				}
			}

			G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));

			// Run intaraction after drawing to prevent border from becoming de-synced
			if (CanDrag)
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
		}
		internal void DrawTitle()
		{
			if (HasBorder)
			{
				DrawFilledRectangle(0, 0, Width, Config.Scale, Config.Radius, Config.AccentColor);
				DrawString((int)(Width / 2), (int)(Config.Scale / 2), Text, Config.Font, Config.ForeColor, true);
			}
		}

		#region Methods

		/// <summary>
		/// Generate generic properties.
		/// </summary>
		private void Generate()
		{
			Controls.Add(new Button(Config.Scale, Config.Scale)
			{
				X = (int)(Width - Config.Scale),
				HasBorder = false,
				Text = "X",
				OnClickEvent = (int X, int Y, MouseState State) => { Close(); },
			});
			OnDrawEvent = (Graphics G) => DrawTitle();
			NeedsFront = true;
			CanDrag = true;
			Frames.Add(this);
		}
		/// <summary>
		/// Draw the frame onto a buffer.
		/// </summary>
		/// <param name="G">Buffer to draw to.</param>
		public void Update(Graphics G)
		{
			if (Frames[^1] == this && CanType && Keyboard.TryReadKey(out ConsoleKeyInfo Key))
			{
				OnKey(Key);
			}
			OnDraw(G);
		}
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
		/// Check to see if the window must be selected in order to interact with it.
		/// </summary>
		public bool NeedsFront;
		/// <summary>
		/// The temporary variable to check if the windoww is moving already.
		/// </summary>
		public bool IsMoving;
		/// <summary>
		/// Check to see if the frame can be moved.
		/// </summary>
		public bool CanDrag;
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