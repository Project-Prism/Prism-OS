using PrismUI.Controls.Buttons;
using Cosmos.System;
using PrismUI;

namespace PrismOS.Apps
{
	public class TickTackToe : Window
	{
		public TickTackToe() : base("TickTackToe")
		{
			Random = new();
			Score = 0;

			int OCount = Controls.Count;
			int H3 = (int)Height / 3;
			int W3 = (int)Width / 3;

			Controls.Add(B11 = new((uint)W3, (uint)H3)
			{
				X = 0,
				Y = (int)Config.Scale,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B11.Text.Length != 0)
					{
						return;
					}
					B11.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B12 = new((uint)W3, (uint)H3)
			{
				X = W3,
				Y = (int)Config.Scale,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B12.Text.Length != 0)
					{
						return;
					}
					B12.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B13 = new((uint)W3, (uint)H3)
			{
				X = W3 * 2,
				Y = (int)Config.Scale,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B13.Text.Length != 0)
					{
						return;
					}
					B13.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B21 = new((uint)W3, (uint)H3)
			{
				X = 0,
				Y = (int)Config.Scale + H3,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B21.Text.Length != 0)
					{
						return;
					}
					B21.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B22 = new((uint)W3, (uint)H3)
			{
				X = W3,
				Y = (int)Config.Scale + H3,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B22.Text.Length != 0)
					{
						return;
					}
					B22.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B23 = new((uint)W3, (uint)H3)
			{
				X = W3 * 2,
				Y = (int)Config.Scale + H3,
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B23.Text.Length != 0)
					{
						return;
					}
					B23.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B31 = new((uint)W3, (uint)H3)
			{
				X = 0,
				Y = (int)Config.Scale + (H3 * 2),
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B31.Text.Length != 0)
					{
						return;
					}
					B31.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B32 = new((uint)W3, (uint)H3)
			{
				X = W3,
				Y = (int)Config.Scale + (H3 * 2),
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B32.Text.Length != 0)
					{
						return;
					}
					B32.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});
			Controls.Add(B33 = new((uint)W3, (uint)H3)
			{
				X = W3 * 2,
				Y = (int)Config.Scale + (H3 * 2),
				OnClickEvent = (int X, int Y, MouseState State) =>
				{
					if (B33.Text.Length != 0)
					{
						return;
					}
					B33.Text = "X";

					if (RunCheck("O"))
					{
						Reset(false);
						return;
					}
					if (RunCheck("X"))
					{
						Reset(true);
						return;
					}
					if (IsFull())
					{
						Reset(false);
						return;
					}

					int RIndex = 0;
					while (Controls[RIndex = Random.Next(OCount, OCount + 8)].Text.Length != 0) ;
					Controls[RIndex].Text = "O";
				}
			});

		}

		#region Methods

		public bool RunCheck(string Name)
		{
			// Diagonal
			if (B11.Text == Name && B12.Text == Name && B13.Text == Name)
			{
				return true;
			}
			if (B21.Text == Name && B22.Text == Name && B23.Text == Name)
			{
				return true;
			}
			if (B31.Text == Name && B32.Text == Name && B33.Text == Name)
			{
				return true;
			}

			// Vertical
			if (B11.Text == Name && B12.Text == Name && B13.Text == Name)
			{
				return true;
			}
			if (B21.Text == Name && B22.Text == Name && B23.Text == Name)
			{
				return true;
			}
			if (B31.Text == Name && B32.Text == Name && B33.Text == Name)
			{
				return true;
			}

			// Top left to bottom right
			if (B11.Text == Name && B22.Text == Name && B33.Text == Name)
			{
				return true;
			}

			// Top right to bottom left
			if (B31.Text == Name && B22.Text == Name && B13.Text == Name)
			{
				return true;
			}

			return false;
		}
		public void Reset(bool AddScore)
		{
			if (AddScore)
			{
				Score++;
				Text = $"TickTackToe - {Score} Points";
			}

			B11.Text = string.Empty;
			B12.Text = string.Empty;
			B13.Text = string.Empty;
			B21.Text = string.Empty;
			B22.Text = string.Empty;
			B23.Text = string.Empty;
			B31.Text = string.Empty;
			B32.Text = string.Empty;
			B33.Text = string.Empty;
		}
		public bool IsFull()
		{
			if (B11.Text.Length == 0 || B12.Text.Length == 0 || B13.Text.Length == 0)
			{
				return false;
			}
			if (B21.Text.Length == 0 || B22.Text.Length == 0 || B23.Text.Length == 0)
			{
				return false;
			}
			if (B31.Text.Length == 0 || B32.Text.Length == 0 || B33.Text.Length == 0)
			{
				return false;
			}

			return true;
		}

		#endregion

		#region Fields

		public Button B11, B12, B13;
		public Button B21, B22, B23;
		public Button B31, B32, B33;
		public Random Random;
		public uint Score;

		#endregion
	}
}