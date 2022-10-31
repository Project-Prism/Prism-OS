using PrismGL3D.Numerics;

namespace PrismGL3D.Raycaster
{
	public class Player
	{
		public Player()
		{
			RotationSpeed = 0.002;
			Position = new(1.5, 5);
			Speed = 0.004;
			Angle = 0.0;
		}

		#region Methods

		public void Movement(ConsoleKeyInfo Key, double Delta)
		{
			double SinA = Math.Sin(Angle);
			double CosB = Math.Cos(Angle);

			double Speed = this.Speed * Delta;

			double SpeedSin = Speed * SinA;
			double SpeedCos = Speed * CosB;

			double DX = 0.0, DY = 0.0;

			switch (Key.Key)
			{
				case ConsoleKey.W:
					DX += SpeedCos;
					DY += SpeedSin;
					break;
				case ConsoleKey.S:
					DX += -SpeedCos;
					DY += -SpeedSin;
					break;
				case ConsoleKey.A:
					DX += SpeedCos;
					DY += -SpeedSin;
					break;
				case ConsoleKey.D:
					DX += -SpeedCos;
					DY += SpeedSin;
					break;
				case ConsoleKey.RightArrow:
					Angle += RotationSpeed * Delta;
					break;
				case ConsoleKey.LeftArrow:
					Angle -= RotationSpeed * Delta;
					break;
			}

			Position.X += DX;
			Position.Y += DY;
			Angle %= Math.PI * 2;
		}

		#endregion

		#region Fields

		public double RotationSpeed;
		public Vector2 Position;
		public double Speed;
		public double Angle;

		#endregion
	}
}