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

		#region Operators

		/// <summary>
		/// Used to multiply <see cref="Vector3"/> by a <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="V">Input vector.</param>
		/// <param name="M">Input matrix.</param>
		/// <returns>Output vector.</returns>
		public static Triangle operator *(Triangle T, Matrix4x4 M)
		{
			Vector3 Output1 = new()
			{
				X = T.P1.X * M.M11 + T.P1.Y * M.M21 + T.P1.Z * M.M31 + M.M41,
				Y = T.P1.X * M.M12 + T.P1.Y * M.M22 + T.P1.Z * M.M32 + M.M42,
				Z = T.P1.X * M.M13 + T.P1.Y * M.M23 + T.P1.Z * M.M33 + M.M43,
			};
			Vector3 Output2 = new()
			{
				X = T.P2.X * M.M11 + T.P2.Y * M.M21 + T.P2.Z * M.M31 + M.M41,
				Y = T.P2.X * M.M12 + T.P2.Y * M.M22 + T.P2.Z * M.M32 + M.M42,
				Z = T.P2.X * M.M13 + T.P2.Y * M.M23 + T.P2.Z * M.M33 + M.M43,
			};
			Vector3 Output3 = new()
			{
				X = T.P3.X * M.M11 + T.P3.Y * M.M21 + T.P3.Z * M.M31 + M.M41,
				Y = T.P3.X * M.M12 + T.P3.Y * M.M22 + T.P3.Z * M.M32 + M.M42,
				Z = T.P3.X * M.M13 + T.P3.Y * M.M23 + T.P3.Z * M.M33 + M.M43,
			};

			float W1 = T.P1.X * M.M14 + T.P1.Y * M.M24 + T.P1.Z * M.M34 + M.M44;
			float W2 = T.P2.X * M.M14 + T.P2.Y * M.M24 + T.P2.Z * M.M34 + M.M44;
			float W3 = T.P3.X * M.M14 + T.P3.Y * M.M24 + T.P3.Z * M.M34 + M.M44;

			if (W1 != 0.0)
			{
				Output1.X /= W1;
				Output1.Y /= W1;
				Output1.Z /= W1;
			}
			if (W2 != 0.0)
			{
				Output2.X /= W2;
				Output2.Y /= W2;
				Output2.Z /= W2;
			}
			if (W3 != 0.0)
			{
				Output3.X /= W3;
				Output3.Y /= W3;
				Output3.Z /= W3;
			}

			return new()
			{
				P1 = Output1,
				P2 = Output2,
				P3 = Output3,
				Color = T.Color,
			};
		}

		#endregion

		#region Methods
		
		/// <summary>
		/// Applies Z0 to the triangle.
		/// </summary>
		/// <param name="Z0">The perspective value.</param>
		/// <returns>A triangle with perspective.</returns>
		public Triangle ApplyPerspective(float Z0)
		{
			float Cache1 = Z0 / (Z0 + this.P1.Z);
			float Cache2 = Z0 / (Z0 + this.P2.Z);
			float Cache3 = Z0 / (Z0 + this.P3.Z);

			Vector3 P1 = new()
			{
				X = this.P1.X * Cache1,
				Y = this.P1.Y * Cache1,
				Z = this.P1.Z,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X * Cache2,
				Y = this.P2.Y * Cache2,
				Z = this.P2.Z,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X * Cache3,
				Y = this.P3.Y * Cache3,
				Z = this.P3.Z,
			};

			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
				Color = Color
			};
		}

		/// <summary>
		/// Rotates the triangle.
		/// </summary>
		/// <param name="Rotation">The rotation to rotate the triangle by.</param>
		/// <returns>A rotated triangle.</returns>
		public Triangle Rotate(Vector3 Rotation)
		{
			float CosRotX = (float)Math.Cos(Rotation.X);
			float CosRotY = (float)Math.Cos(Rotation.Y);
			float CosRotZ = (float)Math.Cos(Rotation.Z);
			float SinRotX = (float)Math.Sin(Rotation.X);
			float SinRotY = (float)Math.Sin(Rotation.Y);
			float SinRotZ = (float)Math.Sin(Rotation.Z);

			Vector3 P1 = new()
			{
				X = this.P1.X * (CosRotZ * CosRotY) + this.P1.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P1.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P1.X * (SinRotZ * CosRotY) + this.P1.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P1.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P1.X * (-SinRotY) + this.P1.Y * (CosRotY * SinRotX) + this.P1.Z * (CosRotY * CosRotX),
			};
			Vector3 P2 = new()
			{
				X = this.P2.X * (CosRotZ * CosRotY) + this.P2.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P2.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P2.X * (SinRotZ * CosRotY) + this.P2.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P2.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P2.X * (-SinRotY) + this.P2.Y * (CosRotY * SinRotX) + this.P2.Z * (CosRotY * CosRotX),
			};
			Vector3 P3 = new()
			{
				X = this.P3.X * (CosRotZ * CosRotY) + this.P3.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + this.P3.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX),
				Y = this.P3.X * (SinRotZ * CosRotY) + this.P3.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + this.P3.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX),
				Z = this.P3.X * (-SinRotY) + this.P3.Y * (CosRotY * SinRotX) + this.P3.Z * (CosRotY * CosRotX),
			};

			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
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
			Vector3 P1 = new()
			{
				X = this.P1.X + Translation.X,
				Y = this.P1.Y + Translation.Y,
				Z = this.P1.Z + Translation.Z,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X + Translation.X,
				Y = this.P2.Y + Translation.Y,
				Z = this.P2.Z + Translation.Z,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X + Translation.X,
				Y = this.P3.Y + Translation.Y,
				Z = this.P3.Z + Translation.Z,
			};

			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
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
			Vector3 P1 = new()
			{
				X = this.P1.X + (Width / 2),
				Y = this.P1.Y + (Height / 2),
				Z = this.P1.Z,
			};
			Vector3 P2 = new()
			{
				X = this.P2.X + (Width / 2),
				Y = this.P2.Y + (Height / 2),
				Z = this.P2.Z,
			};
			Vector3 P3 = new()
			{
				X = this.P3.X + (Width / 2),
				Y = this.P3.Y + (Height / 2),
				Z = this.P3.Z,
			};
			return new()
			{
				P1 = P1,
				P2 = P2,
				P3 = P3,
				Color = Color
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
		/// Color of the triangle.
		/// </summary>
		public Color Color;

		#endregion
	}
}