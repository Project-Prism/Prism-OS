using PrismUI.Controls;
using Cosmos.System;
using PrismGL2D;

namespace PrismOS.Apps
{
	public class Snake : Frame
	{
		public Snake() : base(400, 400, "Snake!")
		{
			RestartButton = new(128, Config.Scale)
			{
				Y = (int)Font.Fallback.Size,
				Text = "re-try",
				OnClickEvent = (int X, int Y, MouseState State) => { IsRunning = true; Controls.Remove(RestartButton); }
			};
			Objects = new()
			{
				new()
				{
					ObjectType = ObjectType.Snake,
					Name = "Head",
					X = (int)Width / 2,
					Y = (int)Height / 2,
				},
				new()
				{
					ObjectType = ObjectType.Food,
					Name = "Apple",
					X = Random.Next(1, (int)((Width - 1) / Config.Scale)),
					Y = Random.Next(1, (int)((Height - 1) / Config.Scale)),
				},
			};
			Random = new();
			OnDrawEvent = (Graphics G) => Next();
			Image = new(Width, Height - Config.Scale);
			Image.Y = (int)Config.Scale;
			Controls.Add(Image);
		}

		#region Structure

		public enum ObjectType
		{
			Snake,
			Food,
		}
		public class Object
		{
			public ObjectType ObjectType;
			public string Name;
			public int X;
			public int Y;
		}

		#endregion

		#region Methods

		public void Next()
		{
			if (IsRunning)
			{
				switch (System.Console.ReadKey().Key)
				{
					case ConsoleKey.W:
						if (Direction != 1)
						{
							Direction = 0;
						}
						break;
					case ConsoleKey.S:
						if (Direction != 0)
						{
							Direction = 0;
						}
						break;
					case ConsoleKey.A:
						if (Direction != 3)
						{
							Direction = 2;
						}
						break;
					case ConsoleKey.D:
						if (Direction != 2)
						{
							Direction = 3;
						}
						break;
				}

				if ((DateTime.Now - LastUpdate).TotalSeconds >= 0.75)
				{
					LastUpdate = DateTime.Now;
					if (Objects[1].ObjectType == ObjectType.Food && Objects[1].X == Objects[0].X && Objects[1].Y == Objects[0].Y)
					{
						Eat();
						Objects[^1].X = Objects[^2].X;
						Objects[^1].Y = Objects[^2].Y;
					}
					if (Objects.Count > 3)
					{
						for (int i = Objects.Count - 1; i > 3; i--)
						{
							if (Objects[i].X == Objects[0].X && Objects[i].Y == Objects[0].Y)
							{
								OnDeath();
							}
							Objects[i].X = Objects[i - 1].X;
							Objects[i].Y = Objects[i - 1].Y;
						}
						Objects[3].X = Objects[0].X;
						Objects[3].Y = Objects[0].Y;
					}

					switch (Direction)
					{
						case 0:
							if (Objects[0].Y == 0) { OnDeath(); }
							Objects[0].Y--;
							break;
						case 1:
							if (Objects[0].Y == Height / Config.Scale) { OnDeath(); }
							Objects[0].Y++;
							break;
						case 2:
							if (Objects[0].X == 0) { OnDeath(); }
							Objects[0].X--;
							break;
						case 3:
							if (Objects[0].X == Width / Config.Scale) { OnDeath(); }
							Objects[0].X++;
							break;
					}

					for (int I = 0; I < Objects.Count; I++)
					{
						int OX = (int)(Objects[I].X * Config.Scale);
						int OY = (int)(Objects[I].Y * Config.Scale);

						if (Objects[I].ObjectType == ObjectType.Snake)
						{
							Image.DrawFilledRectangle(OX, OY, Config.Scale, Config.Scale, Config.Radius, Color.GoogleYellow);
						}
						if (Objects[I].ObjectType == ObjectType.Food)
						{
							Image.DrawFilledRectangle(OX, OY, Config.Scale, Config.Scale, Config.Radius, Color.Red);
						}
					}
				}
			}
			else
			{
				DrawString(0, 0, $"Game Over!\nScore: {Score}", Font.Fallback, Color.White);
			}
		}
		public void OnDeath()
		{
			IsRunning = false;
			Controls.Add(RestartButton);
		}
		public void Eat()
		{
			Objects.Add(new()
			{
				ObjectType = ObjectType.Snake,
				Name = "Body",
				X = 0,
				Y = 0,
			});
			Score++;
			Objects[1].X = Random.Next(1, ((int)Width - 1) / (int)Config.Scale);
			Objects[1].Y = Random.Next(1, ((int)Height - 1) / (int)Config.Scale);
		}

		#endregion

		#region Fields

		public Button RestartButton;
		public List<Object> Objects;
		public DateTime LastUpdate;
		public bool IsRunning;
		public byte Direction;
		public Random Random;
		public Image Image;
		public uint Score;

		#endregion
	}
}