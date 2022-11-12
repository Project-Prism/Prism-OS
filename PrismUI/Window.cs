using PrismUI.Structure;
using PrismUI.Controls;
using Cosmos.System;
using Cosmos.Core;
using PrismTools;
using PrismGL2D;

namespace PrismUI
{
	public class Window : Control
	{
		public Window(uint Width, uint Height, string Title, bool IsDraggable = true) : base(Width, Height)
		{
			Y = (int)((VBE.getModeInfo().height / 2) - (Height / 2) + (Windows.Count * Config.Scale));
			X = (int)((VBE.getModeInfo().width / 2) - (Width / 2) + (Windows.Count * Config.Scale));
			Generate(Title, IsDraggable);
		}
		public Window(string Title, bool IsDraggable = true) : base((uint)(VBE.getModeInfo().width / 2), (uint)(VBE.getModeInfo().height / 2))
		{
			Y = (int)(Height - (Height / 2) + (Windows.Count * Config.Scale));
			X = (int)(Width - (Width / 2) + (Windows.Count * Config.Scale));
			Generate(Title, IsDraggable);
		}

		#region Methods

		public override void OnKey(ConsoleKeyInfo Key)
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
		public override void OnDraw(Graphics G)
		{ // tbh i do not really understand my logic behind this, it needs to be re-done eventualy but for the time being, it works okay enough.
			base.OnDraw(G);

			for (int I = 0; I < Controls.Count; I++)
			{
				if (!Controls[I].CanInteract || !CanInteract)
				{
					continue;
				}
				if (!Controls[I].IsEnabled)
				{
					continue;
				}

				if (MouseEx.IsMouseWithin(X + Controls[I].X, Y + Controls[I].Y, Controls[I].Width, Controls[I].Height) && (this == Windows[^1] || !NeedsFront || !CanInteract))
				{
					if (MouseManager.MouseState != MouseState.None)
					{
						Controls[I].ClickState = ClickState.Clicked;
					}
					else if (Controls[I].ClickState == ClickState.Clicked)
					{
						Controls[I].ClickState = ClickState.Neutral;
						Controls[I].OnClickEvent((int)MouseManager.X - X, (int)MouseManager.Y - Y, MouseManager.LastMouseState);
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

			G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
		}

		/// <summary>
		/// Generate generic properties.
		/// </summary>
		private void Generate(string Title, bool IsDraggable)
		{
			if (IsDraggable)
			{
				Controls.Add(new DragPanel(Width, Config.Scale, this));
				Controls.Add(new Button(Config.Scale, Config.Scale)
				{
					X = (int)(Width - Config.Scale),
					HasBorder = false,
					Text = "X",
					OnClickEvent = (int X, int Y, MouseState State) => { Close(); },
				});
			}

			NeedsFront = true;
			Text = Title;

			Windows.Add(this);
		}

		/// <summary>
		/// Activates the frame.
		/// </summary>
		public void Select()
		{
			if (Windows[^1] == this)
			{
				return;
			}

			Windows.Remove(this);
			Windows.Add(this);
		}

		/// <summary>
		/// Close the frame.
		/// </summary>
		public void Close()
		{
			Windows.Remove(this);
			Dispose();
		}

		#endregion

		#region Fields

		// Window Manager Variables
		public static List<Window> Windows { get; set; } = new();
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

		#endregion
	}
}