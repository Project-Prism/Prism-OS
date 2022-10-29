using PrismUI.Controls;
using Cosmos.System;
using PrismGL2D;

namespace PrismOS.Apps
{
	public class BallSimulator : Frame
	{
		public BallSimulator() : base(nameof(BallSimulator))
		{
			Circles = new();
			SelectedIndex = -1;

			Random R = new();

			for (int I = 0; I < R.Next(5, 10); I++)
			{
				AddBall(R.Next(0, 100), R.Next(0, 100), R.Next(0, 100));
			}

			OnDrawEvent = (Graphics G) => Next(G);
		}

		#region Structure

		public class Ball
		{
			public Ball(int X, int Y, int Radius = 20)
			{
				this.Radius = Radius;
				Color = Color.White;
				AX = 0;
				AY = 0;
				VX = 0;
				VY = 0;
				this.X = X;
				this.Y = Y;
			}

			public Color Color;
			public int Radius;
			public double AX;
			public double AY;
			public double VX;
			public double VY;
			public double X;
			public double Y;
		}

		#endregion

		#region Methods

		public static bool CirclesOverlap(Ball C1, Ball C2)
		{
			int DistanceX = (int)(C1.X - C2.X);
			int DistanceY = (int)(C1.Y - C2.Y);
			int SumRadius = C1.Radius + C2.Radius;

			return Math.Abs(DistanceX ^ 2 + DistanceY ^ 2) <= (SumRadius ^ 2);
		}
		public static double GetDistance(Ball C1, Ball C2)
		{
			return Math.Sqrt(
				((C1.X - C2.X) * (C1.X - C2.X)) +
				((C1.Y - C2.Y) * (C1.Y - C2.Y)));
		}
		public void Next(Graphics G)
		{
			if (MouseManager.MouseState == MouseState.Left && SelectedIndex == -1)
			{
				for (int I = 0; I < Circles.Count; I++)
				{
					if (MouseManager.X - X > (Circles[I].X - Circles[I].Radius) &&
						MouseManager.X - X < (Circles[I].X + Circles[I].Radius) &&
						MouseManager.Y - Y > (Circles[I].Y - Circles[I].Radius) &&
						MouseManager.Y - Y < (Circles[I].Y + Circles[I].Radius))
					{
						SelectedIndex = I;
						break;
					}
				}
			}
			else if (MouseManager.MouseState != MouseState.Left)
			{
				SelectedIndex = -1;
			}
			else
			{
				Circles[SelectedIndex].X = X + MouseManager.X;
				Circles[SelectedIndex].Y = Y + MouseManager.Y;
			}

			for (int I1 = 0; I1 < Circles.Count; I1++)
			{
				for (int I2 = 0; I2 < Circles.Count; I2++)
				{
					if (I1 == I2)
					{
						continue;
					}

					if (CirclesOverlap(Circles[I1], Circles[I2]))
					{
						double Distance = GetDistance(Circles[I1], Circles[I2]);
						double Overlap = 0.5 * (Distance - Circles[I1].Radius - Circles[I2].Radius);
						double Cache = Overlap * (Circles[I1].X - Circles[I2].X) / Distance;

						Circles[I1].X -= Cache;
						Circles[I1].Y -= Cache;

						Circles[I2].X += Cache;
						Circles[I2].Y += Cache;
					}
				}

				G.DrawCircle(
					X: (int)Circles[I1].X,
					Y: (int)Circles[I1].Y,
					Radius: (uint)Circles[I1].Radius,
					Color: Circles[I1].Color);
			}
		}
		public void AddBall(int X, int Y, int Radius)
		{
			Circles.Add(new Ball(X, Y, Radius));
		}

		#endregion

		#region Fields

		public List<Ball> Circles;
		public int SelectedIndex;

		#endregion
	}
}