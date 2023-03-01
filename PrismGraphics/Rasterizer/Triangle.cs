using System.Numerics;

namespace PrismGraphics.Rasterizer
{
	public class Triangle
	{
		public Triangle(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3, Color Color)
		{
			// Assign current points.
			P1 = new(X1, Y1, Z1);
			P2 = new(X2, Y2, Z2);
			P3 = new(X3, Y3, Z3);

			// Assign color value.
			this.Color = Color;
		}
		public Triangle(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3)
		{
			// Assign current points.
			P1 = new(X1, Y1, Z1);
			P2 = new(X2, Y2, Z2);
			P3 = new(X3, Y3, Z3);

			// Assign color value.
			Color = Color.White;
		}
		public Triangle(Vector3 P1, Vector3 P2, Vector3 P3, Color Color)
		{
			// Assign current points.
			this.P1 = P1;
			this.P2 = P2;
			this.P3 = P3;

			// Assign color value.
			this.Color = Color;
		}
		public Triangle(Vector3 P1, Vector3 P2, Vector3 P3)
		{
			// Assign current points.
			this.P1 = P1;
			this.P2 = P2;
			this.P3 = P3;

			// Assign color value.
			Color = Color.White;
		}
		public Triangle()
		{
			Color = Color.White;
			P1 = new();
			P2 = new();
			P3 = new();
		}

		#region Methods

		/// <summary>
		/// Applies Z0 to the triangle.
		/// </summary>
		/// <param name="Z0">The perspective value.</param>
		/// <returns>A triangle with perspective.</returns>
		public Triangle ApplyPerspective(float Z0)
		{
			float Cache1 = Z0 / (Z0 + P1.Z);
			float Cache2 = Z0 / (Z0 + P2.Z);
			float Cache3 = Z0 / (Z0 + P3.Z);

			Vector3 M1 = new(Cache1, Cache1, 1);
			Vector3 M2 = new(Cache2, Cache2, 1);
			Vector3 M3 = new(Cache3, Cache3, 1);

			return new()
			{
				P1 = Vector3.Multiply(P1, M1),
				P2 = Vector3.Multiply(P2, M2),
				P3 = Vector3.Multiply(P3, M3),
				Color = Color
			};
		}

		/// <summary>
		/// Transforms the triangle with the standard vector transformation formula.
		/// </summary>
		/// <param name="Transformation">The Quaternion which with to do the transformation with.</param>
		/// <returns>The transformed triangle.</returns>
		public Triangle Transform(Quaternion Transformation)
		{
			return new()
			{
				P1 = Vector3.Transform(P1, Transformation),
				P2 = Vector3.Transform(P2, Transformation),
				P3 = Vector3.Transform(P3, Transformation),
				Color = Color,
			};
		}

		/// <summary>
		/// Transforms the triangle with the standard vector transformation formula.
		/// </summary>
		/// <param name="Transformation">The Matrix which with to do the transformation with.</param>
		/// <returns>The transformed triangle.</returns>
		public Triangle Transform(Matrix4x4 Transformation)
		{
			return new()
			{
				P1 = Vector3.Transform(P1, Transformation),
				P2 = Vector3.Transform(P2, Transformation),
				P3 = Vector3.Transform(P3, Transformation),
				Color = Color,
			};
		}

		/// <summary>
		/// Translates or "moves" the triangle based on the input translation.
		/// </summary>
		/// <param name="Translation">The translation to move the triangle by.</param>
		/// <returns>Translated triangle.</returns>
		public Triangle Translate(Vector3 Translation)
		{
			return new()
			{
				P1 = Vector3.Add(P1, Translation),
				P2 = Vector3.Add(P2, Translation),
				P3 = Vector3.Add(P3, Translation),
				Color = Color,
			};
		}

		/// <summary>
		/// Center the trangle in the screen.
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <returns>Centered triangle.</returns>
		public Triangle Center(uint Width, uint Height)
		{
			return new()
			{
				P1 = Vector3.Add(P1, new(Width / 2, Height / 2, 0)),
				P2 = Vector3.Add(P2, new(Width / 2, Height / 2, 0)),
				P3 = Vector3.Add(P3, new(Width / 2, Height / 2, 0)),
				Color = Color
			};
		}

		/// <summary>
		/// Gets the vector normal, used or ray-traced lighting.
		/// </summary>
		/// <returns>The vector normal of the triangle.</returns>
		public Vector3 GetVectorNormal()
		{
			Vector3 Direction = Vector3.Cross(P2 - P1, P3 - P1);
			return Vector3.Normalize(Direction);
		}

		/// <summary>
		/// Gets the normal value of the triangle, it will not be rendered if Normal < 0.
		/// </summary>
		/// <returns>Normal of the triangle.</returns>public double GetNormal()
		public float GetNormal()
		{
			return
				(P2.X - P1.X) *
				(P3.Y - P1.Y) -
				(P2.Y - P1.Y) *
				(P3.X - P1.X);
		}

		#endregion

		#region Fields

		/// <summary>
		/// A point of the triangle.
		/// </summary>
		public Vector3 P1, P2, P3;

		/// <summary>
		/// A light point of the triangle.
		/// </summary>
		public Vector3 L1, L2, L3;

		/// <summary>
		/// Color of the triangle.
		/// </summary>
		public Color Color;

		#endregion
	}
}