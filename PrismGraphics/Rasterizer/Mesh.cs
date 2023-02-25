using System.Numerics;

namespace PrismGraphics.Rasterizer
{
	public class Mesh
	{
		public Mesh()
		{
			Triangles = new();
			Position = new();
			Rotation = new();
			Velocity = new();
			Force = new();

			HasCollision = true;
			HasPhysics = false;
			Mass = 1.0f;
		}

		#region Methods

		// https://docs.fileformat.com/3d/obj/
		public static Mesh FromObject(byte[] Object, float Scale = 1.0f)
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

						VResult.X = float.Parse(Line[1]) * Scale;
						VResult.Y = float.Parse(Line[2]) * Scale;
						VResult.Z = float.Parse(Line[3]) * Scale;

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
		public static Mesh FromObject(string PathTo, float Scale = 1.0f)
		{
			return FromObject(File.ReadAllBytes(PathTo), Scale);
		}
		public static Mesh GetPlane(float Width, float Length)
		{
			return GetCube(Width, 1, Length);
		}
		public static Mesh GetCube(float Width, float Height, float Length)
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

		public void TestLogic(float ElapsedTime)
		{
			//Rotation.Y += (DateTime.Now - LTime).TotalSeconds;
			//LTime = DateTime.Now;
			Rotation.X += ElapsedTime;
		}

		public void Step(float Gravity, float DT)
		{
			// Increment gravity.
			Force.Y += Mass * Gravity;
			Velocity.Y += Force.Y / Mass * DT;
			Position.Y += Velocity.Y * DT;
			Force = Vector3.Zero;
		}

		#endregion

		#region Fields

		public List<Triangle> Triangles;
		public Vector3 Position;
		public Vector3 Rotation;
		public Vector3 Velocity;
		public bool HasCollision;
		public bool HasPhysics;
		public Vector3 Force;
		public float Mass;

		#endregion
	}
}