namespace PrismGL3D.Numerics
{
	public class Vector2
	{
		public Vector2(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}
		public Vector2() { }

		#region Operators

		public static Vector2 operator +(Vector2 V1, Vector2 V2) => new(V1.X + V2.X, V1.Y + V2.Y);
		public static Vector2 operator -(Vector2 V1, Vector2 V2) => new(V1.X - V2.X, V1.Y - V2.Y);
		public static Vector2 operator *(Vector2 V1, Vector2 V2) => new(V1.X * V2.X, V1.Y * V2.Y);
		public static Vector2 operator /(Vector2 V1, Vector2 V2) => new(V1.X / V2.X, V1.Y / V2.Y);

		public static Vector2 operator +(Vector2 V1, double V) => new(V1.X + V, V1.Y + V);
		public static Vector2 operator -(Vector2 V1, double V) => new(V1.X - V, V1.Y - V);
		public static Vector2 operator *(Vector2 V1, double V) => new(V1.X * V, V1.Y * V);
		public static Vector2 operator /(Vector2 V1, double V) => new(V1.X / V, V1.Y / V);

		#endregion

		#region Fields

		public double X, Y;

		#endregion
	}
}