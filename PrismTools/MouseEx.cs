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
	}
}