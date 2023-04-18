using System.Numerics;
using System.Drawing;

namespace PrismOS
{
	public class Map
	{
		public Map()
		{
			Vertecies = new();
		}

		#region Methods

		public static Map Remap(Map Map, ushort Width, ushort Height)
		{
			Map M = new();
			Rectangle Bounds = Map.GetBounds();

			foreach (Vector2 V in Map.Vertecies)
			{
				short Y = (short)(Height - (Math.Max(Bounds.Y, Math.Min(V.Y, Bounds.Height)) - Bounds.Y) * (
						Height - 30) / (Bounds.Height - Bounds.Y) - 30);

				short X = (short)((Math.Max(Bounds.X, Math.Min(V.X, Bounds.Width)) - Bounds.X) * (
						Width - 30) / (Bounds.Width - Bounds.X) + 30);

				M.Vertecies.Add(new Vector2(X, Y));
			}

			return M;
		}

		public Rectangle GetBounds()
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

		#endregion

		#region Fields

		public List<Vector2> Vertecies;

		#endregion
	}
}