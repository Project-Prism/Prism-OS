using PrismGraphics;
using PrismNumerics;

namespace PrismGraphics3D.Types
{
	public class Mesh
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

		// https://docs.fileformat.com/3d/obj/
		public static Mesh FromObject(byte[] Object, double Scale = 1.0)
		{
			StreamReader Stream = new(new MemoryStream(Object));

			// Local cache of verts
			List<Vector3> Vertexes = new();
			Mesh Mesh = new();

			while (!Stream.EndOfStream)
			{
				string? S = Stream.ReadLine();
				if (S == null)
				{
					return Mesh;
				}
				string[] Line = S.Split(' ');

				switch (Line[0])
				{
					case "v":
						Vector3 VResult = new();

						VResult.X = double.Parse(Line[1]) * Scale;
						VResult.Y = double.Parse(Line[2]) * Scale;
						VResult.Z = double.Parse(Line[3]) * Scale;

						Vertexes.Add(VResult);
						break;
					case "f":
						Triangle TResult = new();

						TResult.P1 = Vertexes[(int)(int.Parse(Line[1]) * Scale) - 1];
						TResult.P2 = Vertexes[(int)(int.Parse(Line[2]) * Scale) - 1];
						TResult.P3 = Vertexes[(int)(int.Parse(Line[3]) * Scale) - 1];
						TResult.Color = Color.White;

						Mesh.Triangles.Add(TResult);
						break;
				}
			}

			Mesh.Position.Z = 400;

			return Mesh;
		}
		public static Mesh FromObject(string PathTo, double Scale = 1.0)
		{
			return FromObject(File.ReadAllBytes(PathTo), Scale);
		}
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
			//Rotation.Y += (DateTime.Now - LTime).TotalSeconds;
			//LTime = DateTime.Now;
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