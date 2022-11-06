using PrismGL3D.Numerics;
using PrismGL2D;

namespace PrismGL3D.Raycaster
{
	public class Engine : Graphics
	{
		public Engine(uint Width, uint Height, uint FPS) : base(Width, Height)
		{
			Player = new();
			this.FPS = FPS;
		}

		#region Methods

		public Vector2 GetMapPosition(List<int[]> Map, Player P)
		{
			return new()
			{
				X = Width / Map.Count / P.Position.X,
				Y = Height / Map[0].Length / P.Position.Y,
			};
		}
		public Vector2 GetMapScale(List<int[]> Map)
		{
			return new()
			{
				X = Width / Map.Count,
				Y = Height / Map[0].Length,
			};
		}

		public void Render(List<int[]> Map)
		{
			Clear();

			Vector2 Scale = GetMapScale(Map);

			for (int X = 0; X < Map.Count; X++)
			{
				for (int Y = 0; Y < Map[X].Length; Y++)
				{
					if (Map[X][Y] == 0)
					{
						continue;
					}

					DrawRectangle((int)(X * Scale.X), (int)(Y * Scale.Y), (uint)Scale.X, (uint)Scale.Y, 0, Color.White);
				}
			}

			DrawAngledLine((int)Player.Position.X, (int)Player.Position.Y, (int)Player.Angle, 60, Color.GoogleYellow);
			DrawFilledCircle((int)Player.Position.X, (int)Player.Position.Y, 5, Color.Green);
		}

		#endregion

		#region Fields

		public Player Player;
		public uint FPS;

		#endregion
	}
}