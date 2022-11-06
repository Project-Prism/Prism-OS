using Cosmos.System;
using PrismTools;
using PrismGL2D;

namespace PrismUI.Controls
{
	public class DragPanel : Control
	{
		public DragPanel(uint Width, uint Height, Window Parent) : base(Width, Height)
		{
			this.Parent = Parent;
			CanInteract = false;
			HasBorder = false;
		}

		#region Methods

		internal override void OnDraw(Graphics G)
		{
			base.OnDraw(G);

			if (MouseManager.MouseState == MouseState.Left)
			{
				if (MouseManager.LastMouseState != MouseState.Left && MouseEx.IsMouseWithin(Parent.X + X, Parent.Y + Y, Width, Height) && !IsMoving && !IsDragging)
				{
					IsDragging = true;
					IsMoving = true;
					IX = (int)MouseManager.X - (Parent.X + X);
					IY = (int)MouseManager.Y - (Parent.Y + Y);
				}
			}
			else
			{
				IsDragging = false;
				IsMoving = false;
			}
			if (IsMoving)
			{
				Parent.X = (int)MouseManager.X - IX;
				Parent.Y = (int)MouseManager.Y - IY;
				Parent.Select();
			}

			DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.AccentColor);
			DrawString((int)(Width / 2), (int)(Height / 2), Parent.Text, Config.Font, Config.ForeColor, true);

			G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
		}

		#endregion

		#region Fields

		public static bool IsDragging { get; set; }
		private readonly Window Parent;

		/// <summary>
		/// The temporary variable to check if the drag panel is moving already.
		/// </summary>
		internal bool IsMoving;
		/// <summary>
		/// Buffer X for dragging.
		/// </summary>
		internal int IX;
		/// <summary>
		/// Buffer Y for dragging.
		/// </summary>
		internal int IY;

		#endregion
	}
}