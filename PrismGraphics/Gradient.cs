using PrismGraphics.Animation;
using System.Numerics;

namespace PrismGraphics
{
	/// <summary>
	/// Gradient class, used for generating gradients.
	/// INCOMPLETE/BROKEN: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
	/// </summary>
	public unsafe class Gradient : Graphics
	{
		/// <summary>
		/// Create a new instance of the <see cref="Gradient"/> class.
		/// </summary>
		/// <param name="Width">Width (in pixels) of the gradient.</param>
		/// <param name="Height">Height (in pixels) of the gradient.</param>
		/// <param name="Start">The starting color.</param>
		/// <param name="End">The end color.</param>
		public Gradient(ushort Width, ushort Height, Color Start, Color End) : base(Width, Height)
		{
			for (int I = 0; I < Height; I++)
			{
				DrawFilledRectangle(0, I, Width, 1, 0, Common.Lerp(Start, End, 1.0f / Height * I));
			}
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Gradient"/> class.
		/// BROKEN.
		/// </summary>
		/// <param name="Width">Width (in pixels) of the gradient.</param>
		/// <param name="Height">Height (in pixels) of the gradient.</param>
		/// <param name="Colors">The colors to generate in the canvas.</param>
		public Gradient(ushort Width, ushort Height, Color[] Colors) : base(Width, Height)
		{
			for (float I = 0; I < Height; I++)
			{
				// Get the percent/normal.
				float UV = I / (Height + 1f);

				// Get apropriate colors for the index.
				Color Line1 = Colors[(int)(UV * (Colors.Length - 1))];
				Color Line2 = Colors[(int)((UV * (Colors.Length - 1)) + 1)];

				// Get the color percent/normal for this row.
				float HC = Height / 5;
				float UC = I % HC / HC;

				// Fill the line length-wise for fast operation.
				DrawFilledRectangle(0, (int)I, Width, 1, 0, Common.Lerp(Line1, Line2, UC));
			}
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Gradient"/> class using a time based shader.
		/// </summary>
		/// <param name="Width">Width (in pixels) of the gradient.</param>
		/// <param name="Height">Height (in pixels) of the gradient.</param>
		/// <param name="ElapsedMS">The total time passed in the gradient.</param>
		public Gradient(ushort Width, ushort Height, uint ElapsedMS) : base(Width, Height)
		{
			// Create resolution vector.
			Vector2 Resolution = new(Width, Height);

			// Loop over all pixels.
			for (int X = 0; X < Width; X++)
			{
				for (int Y = 0; Y < Height; Y++)
				{
					Internal[Y * Width + X] = TimeShader(new(X, Y), Resolution, ElapsedMS);
				}
			}
		}

		#region Methods

		/// <summary>
		/// A shader to draw a time-based gradient. Time measured in MS.
		/// </summary>
		/// <param name="X">The X point in the gradient.</param>
		/// <param name="Y">The Y point in the gradient.</param>
		/// <param name="Width">The Width of the gradient.</param>
		/// <param name="Height">The Height of the gradient.</param>
		/// <param name="ElapsedMS">The time passed in the gradient.</param>
		public static uint TimeShader(Vector2 Coord, Vector2 Resolution, uint ElapsedMS)
		{
			// Normalized pixel coordinates (from 0 to 1)
			Vector2 UV = Coord / Resolution;

			// Time varying pixel color
			float ColX = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UV.X);
			float ColY = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UV.Y + 2);
			float ColZ = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UV.Y + 4);

			// Output to screen
			return Color.GetPacked(255, ColX * 255f, ColY * 255f, ColZ * 255f);
		}

		/// <summary>
		/// Masks the gradient over anything in the input panel that isn't alpha.
		/// </summary>
		/// <param name="G">The input canvas to mask.</param>
		/// <returns>A masked canvas.</returns>
		public Graphics MaskAlpha(Graphics G)
		{
			// Get a scaled version if the gradient is smaller or bigger than the input.
			Gradient Scaled = (Gradient)Filters.Scale(G.Width, G.Height, this);

			// Create a temporary buffer.
			Graphics Temp = new(G.Width, G.Height);

			// Loop over every pixel.
			for (uint I = 0; I < G.Size; I++)
			{
				// Skip if pixel is alpha.
				if (G[I].A == 0)
				{
					continue;
				}

				// Set gradient pixel.
				Temp[I] = Scaled[I];
			}

			return Temp;
		}

		#endregion
	}
}