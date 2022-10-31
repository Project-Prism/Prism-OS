using PrismUI.Controls;
using Cosmos.System;

namespace PrismOS.Apps
{
	public class TickTackToe : Frame
	{
		public TickTackToe() : base("TickTackToe")
		{
			Random = new();
			Score = 0;

			for (int X = 0; X < 3; X++)
			{
				for (int Y = 0; Y < 3; Y++)
				{
					Button B = new(Width / 3, (Height - Config.Scale) / 3)
					{
						X = (int)(Width / 3 * X),
						Y = (int)((Height - Config.Scale) / 3 * Y + Config.Scale),
					};
					B.OnClickEvent = (int X, int Y, MouseState State) =>
					{
						if (B.Text.Length != 0)
						{
							return;
						}
						B.Text = "X";

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
						while (Controls[RIndex = Random.Next(1, 9)].Text.Length != 0) ;
						Controls[RIndex].Text = "O";
					};
					Controls.Add(B);
				}
			}
		}

		public Random Random;
		public uint Score;

		public bool RunCheck(string Name)
		{
			// Diagonal
			if (Controls[1].Text == Name && Controls[4].Text == Name && Controls[7].Text == Name)
			{
				return true;
			}
			if (Controls[2].Text == Name && Controls[5].Text == Name && Controls[8].Text == Name)
			{
				return true;
			}
			if (Controls[3].Text == Name && Controls[6].Text == Name && Controls[9].Text == Name)
			{
				return true;
			}

			// Vertical
			if (Controls[1].Text == Name && Controls[2].Text == Name && Controls[3].Text == Name)
			{
				return true;
			}
			if (Controls[4].Text == Name && Controls[5].Text == Name && Controls[6].Text == Name)
			{
				return true;
			}
			if (Controls[7].Text == Name && Controls[8].Text == Name && Controls[9].Text == Name)
			{
				return true;
			}

			// Top left to bottom right
			if (Controls[1].Text == Name && Controls[5].Text == Name && Controls[9].Text == Name)
			{
				return true;
			}

			// Top right to bottom left
			if (Controls[3].Text == Name && Controls[5].Text == Name && Controls[7].Text == Name)
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

			for (int I = 1; I < Controls.Count; I++)
			{
				Controls[I].Text = "";
			}
		}
		public bool IsFull()
		{
			for (int I = 1; I < Controls.Count; I++)
			{
				if (Controls[I].Text.Length == 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}