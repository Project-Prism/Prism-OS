using PrismGL3D.Numerics;
using PrismGL2D;

namespace PrismGL3D.Types
{
	public struct Mesh
	{
		public Mesh()
		{
			Triangles = new();
			Position = new();
			Rotation = new();

			Velocity = new(0, 0, 0, 0, 0, 0);
			Force = new(0, 0, 0, 0, 0, 0);
			HasCollision = true;
			HasPhysics = false;
			Mass = 1.0;
		}

		#region Methods

		public static Mesh GetPlane(double Width, double Length)
		{
			return GetCube(Width, 1, Length);
		}
		public static Mesh GetCube(double Width, double Height, double Length)
		{
			return new()
			{
				Position = new(0, 0, 400),

				Triangles = new()
				{
					// South
					new(-(Width / 2), -(Height / 2), -(Length / 2), -(Width / 2), Height / 2, -(Length / 2), Width / 2, Height / 2, -(Length / 2), Color.White),
					new(-(Width / 2), -(Height / 2), -(Length / 2), Width / 2, Height / 2, -(Length / 2), Width / 2, -(Height / 2), -(Length / 2), Color.White),

					// East
					new(Width / 2, -(Height / 2), -(Length / 2), Width / 2, Height / 2, -(Length / 2), Width / 2, Height / 2, Length / 2, Color.CoolGreen),
					new(Width / 2, -(Height / 2), -(Length / 2), Width / 2, Height / 2, Length / 2, Width / 2, -(Height / 2), Length / 2, Color.CoolGreen),

					// North
					new(Width / 2, -(Height / 2), Length / 2, Width / 2, Height / 2, Length / 2, -(Width / 2), Height / 2, Length / 2, Color.White),
					new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), Height / 2, Length / 2, -(Width / 2), -(Height / 2), Length / 2, Color.White),

					// West
					new(-(Width / 2), -(Height / 2), Length / 2, -(Width / 2), Height / 2, Length / 2, -(Width / 2), Height / 2, -(Length / 2), Color.CoolGreen),
					new(-(Width / 2), -(Height / 2), Length / 2, -(Width / 2), Height / 2, -(Length / 2), -(Width / 2), -(Height / 2), -(Length / 2), Color.CoolGreen),

					// Top
					new(-(Width / 2), Height / 2, -(Length / 2), -(Width / 2), Height / 2, Length / 2, Width / 2, Height / 2, Length / 2, Color.White),
					new(-(Width / 2), Height / 2, -(Length / 2), Width / 2, Height / 2, Length / 2, Width / 2, Height / 2, -(Length / 2), Color.White),

					// Bottom
					new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), -(Length / 2), Color.White),
					new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), -(Length / 2), Width / 2, -(Height / 2), -(Length / 2), Color.White),
				},
			};
		}
		public void TestLogic(double ElapsedTime)
		{
			Rotation.Y += ElapsedTime;
		}
		public void Step(double Gravity, double DT)
		{
			Force.Y += Mass * Gravity;
			Velocity.Y += Force.Y / Mass * DT;
			Position.Y += Velocity.Y * DT;
			Force = new(0, 0, 0, 0, 0, 0);
		}

		#endregion

		#region Fields

		public List<Triangle> Triangles { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Velocity { get; set; }
		public bool HasCollision { get; set; }
		public bool HasPhysics { get; set; }
		public Vector3 Force { get; set; }
		public double Mass { get; set; }

		#endregion
	}
}