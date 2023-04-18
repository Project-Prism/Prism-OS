using System.Numerics;
using System.Drawing;

namespace PrismOS
{
	public static class Map
	{
		public static List<Vector2> Remap(List<Vector2> Map, ushort Width, ushort Height)
		{
			Rectangle Bounds = GetBounds(Map);
			List<Vector2> Temp = new();

			foreach (Vector2 V in Map)
			{
				short Y = (short)(Height - (Math.Max(Bounds.Y, Math.Min(V.Y, Bounds.Height)) - Bounds.Y) * (
						Height - 30) / (Bounds.Height - Bounds.Y) - 30);

				short X = (short)((Math.Max(Bounds.X, Math.Min(V.X, Bounds.Width)) - Bounds.X) * (
						Width - 30) / (Bounds.Width - Bounds.X) + 30);

				Temp.Add(new Vector2(X, Y));
			}

			return Temp;
		}

		public static Rectangle GetBounds(List<Vector2> Vertecies)
		{
			Rectangle Bounds = new();

			for (int I = 0; I < Vertecies.Count; I++)
			{
				if (Vertecies[I].X > Bounds.Width)
				{
					Bounds.Width = (int)Vertecies[I].X;
				}
				if (Vertecies[I].Y > Bounds.Height)
				{
					Bounds.Height = (int)Vertecies[I].Y;
				}
				if (Vertecies[I].X < Bounds.X)
				{
					Bounds.X = (int)Vertecies[I].X;
				}
				if (Vertecies[I].Y < Bounds.Y)
				{
					Bounds.Y = (int)Vertecies[I].Y;
				}
			}

			return Bounds;
		}
	}
}