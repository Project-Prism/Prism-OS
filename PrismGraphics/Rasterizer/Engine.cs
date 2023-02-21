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
		// To-Do: Implement Camera Rotation
		public Engine(ushort Width, ushort Height, float FOV) : base(Width, Height)
		{
			this.Height = Height;
			this.Width = Width;
			this.FOV = FOV;

			Sensitivity = 0.025f;
			SkyColor = Color.GoogleBlue;
			Objects = new();
			Camera = new();
			Gravity = 1.0f;
		}

		#region Methods

		// E.Objects[^1].Rotation = new()
		// {
		//	X = C.Width / C.Height* (MouseManager.Y* E.Sensitivity),
		//	Y = -(C.Width / C.Height* (MouseManager.X* E.Sensitivity)),
		// };

		public void Render()
		{
			float Z0 = (float)(Width / 2 / Math.Tan((FOV + Zoom) / 2 * 0.0174532925)); // 0.0174532925 == pi / 180

			Clear(SkyColor);

			// Calculate Objects
			foreach (Mesh M in Objects)
			{
				if (M.HasPhysics)
				{
					M.Step(Gravity, 1.0f);
				}

				foreach (Triangle T in M.Triangles.ToArray())
				{
					Triangle Rotated = T.Rotate(M.Rotation);
					Triangle Translated = Rotated.Translate(M.Position + Camera.Position);
					Triangle Perspective = Translated.ApplyPerspective(Z0);
					Triangle Centered = Perspective.Center(Width, Height);

					// Check if the triangle doesn't need to be drawn.
					if (Centered.GetNormal() < 0)
					{
						DrawFilledTriangle(Centered);
					}
				}
			}
		}

		#endregion

		#region Fields

		public List<Mesh> Objects;
		public float Sensitivity;
		public Color SkyColor;
		public Camera Camera;
		public float Gravity;
		public float Zoom;
		public float FOV;

		#endregion
	}
}