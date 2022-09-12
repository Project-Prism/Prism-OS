using Cosmos.System;

namespace PrismGL2D.UI
{
	public class Frame : Control
	{
		public Frame()
		{
			HasBorder = true;
		}

		// Window Manager Variables
		public static List<Frame> Windows { get; set; } = new();
		public static bool Dragging { get; set; } = false;

		// Class Variables
		public List<Control> Elements { get; set; } = new();
		public bool Draggable { get; set; } = true;
		public bool Moving { get; set; } = false;
		public int IX { get; set; } = 0;
		public int IY { get; set; } = 0;

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
						Windows.Remove(this);
						Windows.Insert(Windows.Count, this);
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

			if (!IsHidden)
			{
				DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.Background);

				// Draw Title Bar
				if (HasBorder)
				{
					DrawFilledRectangle(0, 0, (int)Width, 20, (int)Theme.Radius, Theme.Accent);
					DrawString((int)(Width / 2), 10, Text, Font, Theme.Text, true);
				}

				foreach (Control C in Elements)
				{
					if (!C.IsHidden)
					{
						if (MouseManager.X >= X + C.X && MouseManager.X <= X + C.X + C.Width && MouseManager.Y >= Y + C.Y && MouseManager.Y <= Y + C.Y + C.Height && (this == Windows[^1] || !Draggable))
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
	}
}