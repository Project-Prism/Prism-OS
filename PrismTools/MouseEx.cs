using Cosmos.System;

namespace PrismTools
{
	public static class MouseEx
	{
		public static bool IsMouseWithin(int X, int Y, uint Width, uint Height)
		{
			return
				MouseManager.X >= X && MouseManager.X <= X + Width &&
				MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
		}

		public static bool HasPositionChanged()
		{
			if (MouseManager.Y != BY)
			{
				BY = MouseManager.Y;
				return true;
			}
			if (MouseManager.X != BX)
			{
				BX = MouseManager.Y;
				return true;
			}
			return false;
		}

		private static uint BX;
		private static uint BY;
	}
}