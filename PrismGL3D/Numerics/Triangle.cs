using PrismGL2D;

namespace PrismGL3D.Numerics
{
	public struct Triangle
	{
		public Triangle(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3, Color Color)
		{
			P1 = new(X1, Y1, Z1);
			P2 = new(X2, Y2, Z2);
			P3 = new(X3, Y3, Z3);
			T1 = P1;
			T2 = P2;
			T3 = P3;
			this.Color = Color;
		}
		public Triangle(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3)
		{
			P1 = new(X1, Y1, Z1);
			P2 = new(X2, Y2, Z2);
			P3 = new(X3, Y3, Z3);
			T1 = P1;
			T2 = P2;
			T3 = P3;
			Color = Color.White;
		}
		public Triangle(Vector3 P1, Vector3 P2, Vector3 P3, Color Color)
		{
			this.P1 = P1;
			this.P2 = P2;
			this.P3 = P3;
			T1 = P1;
			T2 = P2;
			T3 = P3;
			this.Color = Color;
		}
		public Triangle(Vector3 P1, Vector3 P2, Vector3 P3)
		{
			this.P1 = P1;
			this.P2 = P2;
			this.P3 = P3;
			T1 = P1;
			T2 = P2;
			T3 = P3;
			Color = Color.White;
		}

		#region Methods

		/// <summary>
		/// Center the trangle in the screen.
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <returns>Centered triangle.</returns>
		public Triangle Center(uint Width, uint Height)
		{
			Vector3 P1 = new()
			{
				X = this.P1.X + (Width / 2),
				Y = this.P1.Y + (Height / 2),
				Z = this.P1.Z,
				U = this.P1.U,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X + (Width / 2),
				Y = this.P2.Y + (Height / 2),
				Z = this.P2.Z,
				U = this.P2.U,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X + (Width / 2),
				Y = this.P3.Y + (Height / 2),
				Z = this.P3.Z,
				U = this.P3.U,
			};
			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
				T1 = T1,
				T2 = T2,
				T3 = T3,
				Color = Color
			};
		}
		/// <summary>
		/// Applies Z0 to the triangle.
		/// </summary>
		/// <param name="Z0"></param>
		/// <returns>a triangle with perspective.</returns>
		public Triangle ApplyPerspective(double Z0)
		{
			double Cache1 = Z0 / (Z0 + this.P1.Z);
			double Cache2 = Z0 / (Z0 + this.P2.Z);
			double Cache3 = Z0 / (Z0 + this.P3.Z);

			Vector3 P1 = new()
			{
				X = this.P1.X * Cache1,
				Y = this.P1.Y * Cache1,
				Z = this.P1.Z,
				U = this.P1.U * Cache1,
				V = this.P1.V * Cache1,
				W = this.P1.W * Cache1
			};
			Vector3 P2 = new()
			{
				X = this.P2.X * Cache2,
				Y = this.P2.Y * Cache2,
				Z = this.P2.Z,
				U = this.P2.U * Cache2,
				V = this.P2.V * Cache2,
				W = this.P2.W * Cache2
			};
			Vector3 P3 = new()
			{
				X = this.P3.X * Cache3,
				Y = this.P3.Y * Cache3,
				Z = this.P3.Z,
				U = this.P3.U * Cache3,
				V = this.P3.V * Cache3,
				W = this.P3.W * Cache3
			};
			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
				T1 = T1,
				T2 = T2,
				T3 = T3,
				Color = Color
			};
		}
		/// <summary>
		/// Rotates the triangle.
		/// </summary>
		/// <param name="Rotation"></param>
		/// <returns>A rotated triangle.</returns>
		public Triangle Rotate(Vector3 Rotation)
		{
			double CosRotX = Math.Cos(Rotation.X);
			double CosRotY = Math.Cos(Rotation.Y);
			double CosRotZ = Math.Cos(Rotation.Z);
			double SinRotX = Math.Sin(Rotation.X);
			double SinRotY = Math.Sin(Rotation.Y);
			double SinRotZ = Math.Sin(Rotation.Z);

			Vector3 P1 = new()
			{
				X = this.P1.X * (CosRotZ * CosRotY) + this.P1.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P1.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P1.X * (SinRotZ * CosRotY) + this.P1.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P1.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P1.X * (-SinRotY) + this.P1.Y * (CosRotY * SinRotX) + this.P1.Z * (CosRotY * CosRotX),
				U = this.P1.U,
				V = this.P1.V,
				W = this.P1.W,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X * (CosRotZ * CosRotY) + this.P2.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P2.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P2.X * (SinRotZ * CosRotY) + this.P2.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P2.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P2.X * (-SinRotY) + this.P2.Y * (CosRotY * SinRotX) + this.P2.Z * (CosRotY * CosRotX),
				U = this.P2.U,
				V = this.P2.V,
				W = this.P2.W,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X * (CosRotZ * CosRotY) + this.P3.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P3.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P3.X * (SinRotZ * CosRotY) + this.P3.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P3.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P3.X * (-SinRotY) + this.P3.Y * (CosRotY * SinRotX) + this.P3.Z * (CosRotY * CosRotX),
				U = this.P3.U,
				V = this.P3.V,
				W = this.P3.W,
			};
			return new()
			{
				P1 = P1, P2 = P2, P3 = P3,
				T1 = T1, T2 = T2, T3 = T3,
				Color = Color,
			};
		}
		/// <summary>
		/// Translates or "moves" the triangle based on the input translation.
		/// </summary>
		/// <param name="Translation"></param>
		/// <returns>Translated triangle.</returns>
		public Triangle Translate(Vector3 Translation)
		{
			Vector3 P1 = new()
			{
				X = this.P1.X + Translation.X,
				Y = this.P1.Y + Translation.Y,
				Z = this.P1.Z + Translation.Z,
				U = this.P1.U,
				V = this.P1.V,
				W = this.P1.W,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X + Translation.X,
				Y = this.P2.Y + Translation.Y,
				Z = this.P2.Z + Translation.Z,
				U = this.P2.U,
				V = this.P2.V,
				W = this.P2.W,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X + Translation.X,
				Y = this.P3.Y + Translation.Y,
				Z = this.P3.Z + Translation.Z,
				U = this.P3.U,
				V = this.P3.V,
				W = this.P3.W,
			};
			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
				T1 = T1,
				T2 = T2,
				T3 = T3,
				Color = Color,
			};
		}

		/// <summary>
		/// Gets the normal value of the triangle, it will not be rendered if Normal < 0.
		/// </summary>
		/// <returns>Normal of the triangle.</returns>
		public double GetNormal()
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
		/// Testing
		/// </summary>
		public Vector3 T1, T2, T3;
		/// <summary>
		/// Color of the triangle.
		/// </summary>
		public Color Color;

		#endregion
	}
}