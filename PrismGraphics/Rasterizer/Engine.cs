using System.Numerics;

namespace PrismGraphics.Rasterizer
{
	/// <summary>
	/// The <see cref="Engine"/> class, used to rasterize 3D shapes on a 2D canvas.
	/// See also: <seealso cref="Triangle"/>
	/// See also: <seealso cref="Camera"/>
	/// See also: <seealso cref="Light"/>
	/// See also: <seealso cref="Mesh"/>
	/// </summary>
	public class Engine : Graphics
	{
		public Engine(ushort Width, ushort Height, float FOV) : base(Width, Height)
		{
			this.Height = Height;
			this.Width = Width;

			SkyColor = Color.GoogleBlue;
			Lights = new();
			Objects = new();
			Camera = new(FOV);
			Gravity = 1f;
			Zoom = 0f;
		}

		#region Methods

		public void Render()
		{
			// Create the camera rotation matrix.
			Quaternion CameraQ = Camera.GetRotationQuaternion();

			float Z0 = (float)(Width / 2 / Math.Tan((Camera.FOV + Zoom) / 2 * 0.0174532925)); // 0.0174532925 == pi / 180

			// Set the sky color.
			Clear(SkyColor);

			// Calculate Objects
			foreach (Mesh M in Objects)
			{
				// Check if the mesh has physics.
				if (M.HasPhysics)
				{
					// Apply physics.
					M.Step(Gravity, 1.0f);
				}

				// Create a rotation matrix.
				Matrix4x4 Rotation = M.GetRotationMatrix();

				foreach (Triangle T in M.Triangles)
				{
					Triangle Temp = T.Transform(Rotation);
					Temp = Temp.Translate(M.Position);
					Temp = Temp.Transform(CameraQ);
					Temp = Temp.Translate(Camera.Position);
					Temp = Temp.ApplyPerspective(Z0);
					Temp = Temp.Center(Width, Height);

					// Normalize lighting & apply ambiance.
					Temp.Color = Temp.Color.Normalize();
					Temp.Color *= Camera.Ambient;

					// Check if the triangle doesn't need to be drawn.
					if (Temp.GetNormal() < 0)
					{
						DrawFilledTriangle(Temp);
					}
				}
			}
		}

		#endregion

		#region Fields

		public List<Light> Lights;
		public List<Mesh> Objects;
		public Color SkyColor;
		public Camera Camera;
		public float Gravity;
		public float Zoom;

		#endregion
	}
}