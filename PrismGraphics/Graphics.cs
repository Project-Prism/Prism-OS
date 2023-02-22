// using System.Runtime.InteropServices; - Waiting for PR
using PrismGraphics.Rasterizer;
using PrismGraphics.Fonts;
using Cosmos.Core.Memory;
using Cosmos.Core;

namespace PrismGraphics
{
	/// <summary>
	/// The <see cref="Graphics"/> class, used for rendering content on a 2D surface.
	/// </summary>
	public unsafe class Graphics : IDisposable
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Graphics"/> class..
		/// </summary>
		/// <param name="Width">Width of the canvas.</param>
		/// <param name="Height">Height of the canvas.</param>
		public Graphics(ushort Width, ushort Height)
		{
			this.Width = Width;
			this.Height = Height;
		}

		/// <summary>
		/// Indexer to set or get a color value at the X and Y position.
		/// </summary>
		/// <param name="X">X position of the pixel.</param>
		/// <param name="Y">Y position of the pixel.</param>
		/// <returns>The pixel color at X and Y.</returns>
		public Color this[int X, int Y]
		{
			get
			{
				// Check if coordinates are out of bounds.
				if (X < 0 || Y < 0 || X >= Width || Y >= Height)
				{
					return Color.Black;
				}

				return Internal[Y * Width + X];
			}
			set
			{
				// Check if coordinates are out of bounds.
				if (X < 0 || Y < 0 || X >= Width || Y >= Height)
				{
					return;
				}

				// Blend 2 colors together if the new color has alpha.
				if (value.A < 255)
				{
					value = Color.AlphaBlend(this[X, Y], value);
				}

				Internal[Y * Width + X] = value.ARGB;
			}
		}

		/// <summary>
		/// Indexer to set or get a color value at the specified index.
		/// </summary>
		/// <param name="Index">Index position of the pixel.</param>
		/// <returns>The pixel color at the linear index.</returns>
		public Color this[uint Index]
		{
			get
			{
				// Check if index is out of bounds.
				if (Index >= Size)
				{
					return Color.Black;
				}

				return Internal[Index];
			}
			set
			{
				// Check if index is out of bounds.
				if (Index >= Size)
				{
					return;
				}

				// Blend 2 colors together if the new color has alpha.
				if (value.A < 255)
				{
					value = Color.AlphaBlend(this[Index], value);
				}

				Internal[Index] = value.ARGB;
			}
		}

		#region Properties

		/// <summary>
		/// The total height of the canvas in pixels.
		/// </summary>
		public ushort Height
		{
			get
			{
				return _Height;
			}
			set
			{
				if (_Height == value)
				{
					return;
				}
				if (_Width == 0)
				{
					_Height = value;
					return;
				}

				_Height = value;

				if (Internal == (uint*)0)
				{
					// Internal = (uint*)NativeMemory.Alloc(Size * 4);
					Internal = (uint*)Heap.Alloc(Size * 4);
				}
				else
				{
					// Internal = (uint*)NativeMemory.Realloc(Internal, Size * 4);
					Internal = (uint*)Heap.Realloc((byte*)Internal, Size * 4);
				}
			}
		}

		/// <summary>
		/// The total width of the canvas in pixels.
		/// </summary>
		public ushort Width
		{
			get
			{
				return _Width;
			}
			set
			{
				if (_Width == value)
				{
					return;
				}
				if (_Height == 0)
				{
					_Width = value;
					return;
				}

				_Width = value;

				if (Internal == (uint*)0)
				{
					// Internal = (uint*)NativeMemory.Alloc(Size * 4);
					Internal = (uint*)Heap.Alloc(Size * 4);
				}
				else
				{
					// Internal = (uint*)NativeMemory.Realloc(Internal, Size * 4);
					Internal = (uint*)Heap.Realloc((byte*)Internal, Size * 4);
				}
			}
		}

		/// <summary>
		/// The total area of the canvas in pixels.
		/// </summary>
		public uint Size
		{
			get
			{
				return (uint)(_Width * _Height);
			}
		}

		#endregion

		#region Methods

		#region Rectangle

		/// <summary>
		/// Draws a filled rectangle from X and Y with the specified Width and Height.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Width">Width of the rectangle.</param>
		/// <param name="Height">Height of the rectangle</param>
		/// <param name="Radius">Border radius of the rectangle.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Toggle to enable or disable anti-aliasing.</param>
		public void DrawFilledRectangle(int X, int Y, ushort Width, ushort Height, ushort Radius, Color Color, bool UseAA = false)
		{
			// Quit if nothing needs to be drawn.
			if (X >= this.Width || Y >= this.Height)
			{
				return;
			}
			if (X + Width < 0 || Y + Height < 0)
			{
				return;
			}

			// Fastest cropped draw method.
			if (Color.A == 255)
			{
				// Fastest copy-only draw method, fills the whole buffer.
				if (X == 0 && Y == 0 && Width == this.Width && Height == this.Height)
				{
					Clear(Color);
					return;
				}
				
				// Get the cropped coordinates.
				uint StartX = (uint)Math.Max(X, 0);
				uint StartY = (uint)Math.Max(Y, 0);
				uint EndX = (uint)Math.Min(X + Width, this.Width);
				uint EndY = (uint)Math.Min(Y + Height, this.Height);

				// Get new size after crop.
				uint RHeight = EndY - StartY;
				uint RWidth = EndX - StartX;

				// Calculate destination offset for the starting point
				uint Destination = StartY * this.Width + StartX;

				// Fill the region with the color
				for (uint IY = 0; IY < RHeight; IY++)
				{
					MemoryOperations.Fill(Internal + Destination + (IY * this.Width), Color.ARGB, (int)RWidth);
				}
				return;
			}

			// Fastest alpha supporting rectangle.
			if (Radius == 0)
			{
				for (int IX = X; IX < X + Width; IX++)
				{
					for (int IY = Y; IY < Y + Height; IY++)
					{
						this[IX, IY] = Color;
					}
				}
			}

			// Circular rectangle.
			else
			{
				DrawFilledCircle(X + Radius, Y + Radius, Radius, Color, UseAA);
				DrawFilledCircle(X + Width - Radius - 1, Y + Radius, Radius, Color, UseAA);

				DrawFilledCircle(X + Radius, Y + Height - Radius - 1, Radius, Color, UseAA);
				DrawFilledCircle(X + Width - Radius - 1, Y + Height - Radius - 1, Radius, Color, UseAA);

				DrawFilledRectangle(X + Radius, Y, (ushort)(Width - Radius * 2), Height, 0, Color);
				DrawFilledRectangle(X, Y + Radius, Width, (ushort)(Height - Radius * 2), 0, Color);
			}
		}

		/// <summary>
		/// Draws a non-filled rectangle from X and Y with the specified Width and Height.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Width">Width of the rectangle.</param>
		/// <param name="Height">Height of the rectangle</param>
		/// <param name="Radius">Border radius of the rectangle.</param>
		/// <param name="Color">Color to draw with.</param>
		public void DrawRectangle(int X, int Y, ushort Width, ushort Height, ushort Radius, Color Color)
		{
			// Draw circles to add curvature if needed.
			if (Radius > 0)
			{
				DrawArc(Radius + X, Radius + Y, Radius, Color, 180, 270); // Top left
				DrawArc(X + Width - Radius, Y + Height - Radius, Radius, Color, 0, 90); // Bottom right
				DrawArc(Radius + X, Y + Height - Radius, Radius, Color, 90, 180); // Bottom left
				DrawArc(X + Width - Radius, Radius + Y, Radius, Color, 270, 360);
			}

			DrawLine(X + Radius, Y, X + Width - Radius, Y, Color); // Top Line
			DrawLine(X + Radius, Y + Height, X + Width - Radius, Height + Y, Color); // Bottom Line
			DrawLine(X, Y + Radius, X, Y + Height - Radius, Color); // Left Line
			DrawLine(X + Width, Y + Radius, Width + X, Y + Height - Radius, Color); // Right Line
		}

		/// <summary>
		/// Draws a grid of mixed blocks where each block has a size and count, creates a pattern.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="BlockCountX">Number of blocks in the X axis.</param>
		/// <param name="BlockCountY">Number of blocks in the Y axis.</param>
		/// <param name="BlockSize">Scale of all blocks.</param>
		/// <param name="BlockType1">Color of block type 1.</param>
		/// <param name="BlockType2">Color of block type 2.</param>
		public void DrawRectangleGrid(int X, int Y, ushort BlockCountX, ushort BlockCountY, ushort BlockSize, Color BlockType1, Color BlockType2)
		{
			for (int IX = 0; IX < BlockCountX; IX++)
			{
				for (int IY = 0; IY < BlockCountY; IY++)
				{
					if ((IX + IY) % 2 == 0)
					{
						DrawFilledRectangle(X + IX * BlockSize, Y + IY * BlockSize, BlockSize, BlockSize, 0, BlockType1);
					}
					else
					{
						DrawFilledRectangle(X + IX * BlockSize, Y + IY * BlockSize, BlockSize, BlockSize, 0, BlockType2);
					}
				}
			}
		}

		#endregion

		#region Triangle

		/// <summary>
		/// Draws a filled triangle at the 3 points (X1, Y1), (X2, Y2), (X3, Y3).
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X position 2.</param>
		/// <param name="Y2">Y position 2.</param>
		/// <param name="X3">X position 3.</param>
		/// <param name="Y3">Y position 3.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Toggle to enable or disable anti-aliasing.</param>
		public void DrawFilledTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, bool UseAA = false)
		{
			Y1 = (int)Math.Round(16.0f * Y1);
			Y2 = (int)Math.Round(16.0f * Y2);
			Y3 = (int)Math.Round(16.0f * Y3);

			X1 = (int)Math.Round(16.0f * X1);
			X2 = (int)Math.Round(16.0f * X2);
			X3 = (int)Math.Round(16.0f * X3);

			// Deltas
			int DX12 = X1 - X2;
			int DX23 = X2 - X3;
			int DX31 = X3 - X1;

			int DY12 = Y1 - Y2;
			int DY23 = Y2 - Y3;
			int DY31 = Y3 - Y1;

			// Fixed-point deltas
			int FDX12 = DX12 << 4;
			int FDX23 = DX23 << 4;
			int FDX31 = DX31 << 4;

			int FDY12 = DY12 << 4;
			int FDY23 = DY23 << 4;
			int FDY31 = DY31 << 4;

			// Bounding rectangle
			int MinX = (Math.Min(Math.Min(X1, X2), X3) + 0xF) >> 4;
			int MaxX = (Math.Max(Math.Max(X1, X2), X3) + 0xF) >> 4;
			int MinY = (Math.Min(Math.Min(Y1, Y2), Y3) + 0xF) >> 4;
			int MaxY = (Math.Max(Math.Max(Y1, Y2), Y3) + 0xF) >> 4;

			// Half-edge constants
			int C1 = DY12 * X1 - DX12 * Y1;
			int C2 = DY23 * X2 - DX23 * Y2;
			int C3 = DY31 * X3 - DX31 * Y3;

			// Correct for fill convention
			if (DY12 < 0 || (DY12 == 0 && DX12 > 0)) C1++;
			if (DY23 < 0 || (DY23 == 0 && DX23 > 0)) C2++;
			if (DY31 < 0 || (DY31 == 0 && DX31 > 0)) C3++;

			int CY1 = C1 + DX12 * (MinY << 4) - DY12 * (MinX << 4);
			int CY2 = C2 + DX23 * (MinY << 4) - DY23 * (MinX << 4);
			int CY3 = C3 + DX31 * (MinY << 4) - DY31 * (MinX << 4);

			for (int Y = MinY; Y < MaxY; Y++)
			{
				int CX1 = CY1;
				int CX2 = CY2;
				int CX3 = CY3;

				for (int X = MinX; X < MaxX; X++)
				{
					if (CX1 > 0 && CX2 > 0 && CX3 > 0)
					{
						this[X, Y] = Color;
					}

					CX1 -= FDY12;
					CX2 -= FDY23;
					CX3 -= FDY31;
				}

				CY1 += FDX12;
				CY2 += FDX23;
				CY3 += FDX31;
			}

			if (UseAA)
			{
				DrawTriangle(X1, Y1, X2, Y2, X3, Y3, Color, true);
			}
		}

		/// <summary>
		/// Draws a non-filled triangle at the three points (X1, Y1), (X2, Y2), (X3, Y3).
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X position 2.</param>
		/// <param name="Y2">Y position 2.</param>
		/// <param name="X3">X position 3.</param>
		/// <param name="Y3">Y position 3.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Toggle to enable or disable anti-aliasing.</param>
		public void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, bool UseAA = false)
		{
			DrawLine(X1, Y1, X2, Y2, Color, UseAA);
			DrawLine(X1, Y1, X3, Y3, Color, UseAA);
			DrawLine(X2, Y2, X3, Y3, Color, UseAA);
		}

		/// <summary>
		/// Draws a filled triangle as marked by the triangle class.
		/// </summary>
		/// <param name="Triangle">The triangle coordinates.</param>
		public void DrawFilledTriangle(Triangle Triangle)
		{
			DrawFilledTriangle((int)Triangle.P1.X, (int)Triangle.P1.Y, (int)Triangle.P2.X, (int)Triangle.P2.Y, (int)Triangle.P3.X, (int)Triangle.P3.Y, Triangle.Color);
		}

		/// <summary>
		/// Draws a non-filled triangle as marked by the triangle class.
		/// </summary>
		/// <param name="Triangle">The triangle coordinates.</param>
		public void DrawTriangle(Triangle Triangle)
		{
			DrawTriangle((int)Triangle.P1.X, (int)Triangle.P1.Y, (int)Triangle.P2.X, (int)Triangle.P2.Y, (int)Triangle.P3.X, (int)Triangle.P3.Y, Triangle.Color);
		}

		#endregion

		#region Circle 

		/// <summary>
		/// Draws a filled circle where X and Y are the center of it.
		/// </summary>
		/// <param name="X">Center X of the circle.</param>
		/// <param name="Y">Center Y of the circle.</param>
		/// <param name="Radius">Radius of the circle.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Toggle to use anti-aliasing.</param>
		public void DrawFilledCircle(int X, int Y, ushort Radius, Color Color, bool UseAA = false)
		{
			// Quit if there is nothing to draw.
			if (Radius == 0)
			{
				return;
			}

			// Check if the circle can be drawn fast.
			if (Color.A == 255)
			{
				ushort R2 = (ushort)(Radius * Radius);

				// Loop for each line in the circle.
				for (int IY = -Radius; IY <= Radius; IY++)
				{
					int IX = (int)(Math.Sqrt(R2 - IY * IY) + 0.5);
					uint* Offset = Internal + (Width * (Y + IY)) + X - IX;

					// Clip circle if it is out of bounds
					if (X + Radius >= Width)
					{
						// Reduce length to fit to max width.
						IX -= X + Radius - Width;
					}
					if (X - Radius < 0)
					{
						// Reduce length and offset so that it stays at X = 0
						Offset += Radius - +X;
						IX -= Radius - +X;
					}

					// Fill one line of pixels.
					MemoryOperations.Fill(Offset, Color.ARGB, IX * 2);

					// Check to see if AA is enabled.
					if (UseAA)
					{
						// Set AA pixels.
						this[(uint)Offset - 1] = Color.GetPacked((byte)(Color.A / 2), Color.R, Color.G, Color.B);
						this[(uint)(Offset + IX + 1)] = Color.GetPacked((byte)(Color.A / 2), Color.R, Color.G, Color.B);
					}
				}

				// Be sure to return.
				return;
			}

			// Draw using slow algorithm.
			for (int IX = -Radius; IX < Radius; IX++)
			{
				int Height = (int)Math.Sqrt((Radius * Radius) - (IX * IX));

				for (int IY = -Height; IY < Height; IY++)
				{
					this[IX + X, IY + Y] = Color;
				}
			}
		}

		/// <summary>
		/// Draws a non-filled circle where X and Y are the center of it.
		/// </summary>
		/// <param name="X">Center X of the circle.</param>
		/// <param name="Y">Center Y of the circle.</param>
		/// <param name="Radius">Radius of the circle.</param>
		/// <param name="Color">Color to draw with.</param>
		public void DrawCircle(int X, int Y, ushort Radius, Color Color)
		{
			int IX = 0, IY = Radius, DP = 3 - 2 * Radius;

			while (IY >= IX)
			{
				this[X + IX, Y + IY] = Color;
				this[X - IX, Y + IY] = Color;
				this[X + IX, Y - IY] = Color;
				this[X - IX, Y - IY] = Color;
				this[X + IY, Y + IX] = Color;
				this[X - IY, Y + IX] = Color;
				this[X + IY, Y - IX] = Color;
				this[X - IY, Y - IX] = Color;

				IX++;

				if (DP > 0)
				{
					IY--;
					DP += 4 * (IX - IY) + 10;
				}
				else
				{
					DP += 4 * IX + 6;
				}
			}
		}

		#endregion

		#region Lines

		/// <summary>
		/// Draws a quadratic bezier curve from point A (X1, Y1) to point B (X3, Y3)
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X weight.</param>
		/// <param name="Y2">Y weight.</param>
		/// <param name="X3">X position 2.</param>
		/// <param name="Y3">Y position 2.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="N">Used inside the method only.</param>
		public void DrawQuadraticBezierLine(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, byte N = 6)
		{
			// X2 and Y2 is where the curve should bend to.
			if (N > 0)
			{
				int X12 = (X1 + X2) / 2;
				int Y12 = (Y1 + Y2) / 2;
				int X23 = (X2 + X3) / 2;
				int Y23 = (Y2 + Y3) / 2;
				int X123 = (X12 + X23) / 2;
				int Y123 = (Y12 + Y23) / 2;

				DrawQuadraticBezierLine(X1, Y1, X12, Y12, X123, Y123, Color, (byte)(N - 1));
				DrawQuadraticBezierLine(X123, Y123, X23, Y23, X3, Y3, Color, (byte)(N - 1));
			}
			else
			{
				DrawLine(X1, Y1, X2, Y2, Color);
				DrawLine(X2, Y2, X3, Y3, Color);
			}
		}

		/// <summary>
		/// Draws a cubic bezier curve from point A (X1, Y1) to point B (X4, Y4)
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X weight 1.</param>
		/// <param name="Y2">Y weight 1.</param>
		/// <param name="X3">X weight 2.</param>
		/// <param name="Y3">Y weight 2.</param>
		/// <param name="X4">X position 2.</param>
		/// <param name="Y4">Y position 2.</param>
		/// <param name="Color">Color to draw with.</param>
		public void DrawCubicBezierLine(int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4, Color Color)
		{
			for (double U = 0.0; U <= 1.0; U += 0.0001)
			{
				double Power3V1 = (1 - U) * (1 - U) * (1 - U);
				double Power2V1 = (1 - U) * (1 - U);
				double Power3V2 = U * U * U;
				double Power2V2 = U * U;

				double XU = Power3V1 * X1 + 3 * U * Power2V1 * X2 + 3 * Power2V2 * (1 - U) * X3 + Power3V2 * X4;
				double YU = Power3V1 * Y1 + 3 * U * Power2V1 * Y2 + 3 * Power2V2 * (1 - U) * Y3 + Power3V2 * Y4;

				this[(int)XU, (int)YU] = Color;
			}
		}

		/// <summary>
		/// Draws a line at an angle from X and Y with a circle's radius.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Angle">Angle in degrees.</param>
		/// <param name="Radius">Radius or Length.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Toggle to enable or disable anti-aliasing.</param>
		public void DrawAngledLine(int X, int Y, short Angle, ushort Radius, Color Color, bool UseAA = false)
		{
			int IX = (int)(Radius * Math.Cos(Math.PI * Angle / 180));
			int IY = (int)(Radius * Math.Sin(Math.PI * Angle / 180));

			DrawLine(X, Y, X + IX, Y + IY, Color, UseAA);
		}

		/// <summary>
		/// Draws a line from point A (X1, Y1) to point B (X2, Y2)
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X positoin 2.</param>
		/// <param name="Y2">Y position 2.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAA">Enable or disable the use of anti-aliasing.</param>
		public void DrawLine(int X1, int Y1, int X2, int Y2, Color Color, bool UseAA = false)
		{
			int DX = Math.Abs(X2 - X1), SX = X1 < X2 ? 1 : -1;
			int DY = Math.Abs(Y2 - Y1), SY = Y1 < Y2 ? 1 : -1;
			int err = (DX > DY ? DX : -DY) / 2;

			while (X1 != X2 || Y1 != Y2)
			{
				this[X1, Y1] = Color;

				if (UseAA)
				{
					if (X1 + X2 > Y1 + Y2)
					{
						this[X1 + 1, Y1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
						this[X1 + 1, Y1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
					}
					else
					{
						this[X1, Y1 + 1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
						this[X1, Y1 - 1] = Color.FromARGB((byte)(Color.A / 2), Color.R, Color.G, Color.B);
					}
				}

				int E2 = err;

				if (E2 > -DX) { err -= DY; X1 += SX; }
				if (E2 < DY) { err += DX; Y1 += SY; }
			}
		}

		#endregion

		#region Arc

		/// <summary>
		/// Draws a non-filled arc at the center of X and Y with a Width and a Height.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Width">Width of the arc.</param>
		/// <param name="Height">Height of the arc.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="StartAngle">Angle at which to start.</param>
		/// <param name="EndAngle">Angle at which to end.</param>
		public void DrawArc(int X, int Y, ushort Width, ushort Height, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			// Quit if nothing needs to be drawn.
			if (Width == 0 || Height == 0)
			{
				return;
			}

			for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
			{
				double Angle1 = Math.PI * Angle / 180;

				int IX = (int)Math.Clamp(Width * Math.Cos(Angle1), -Width + 1, Width - 1);
				int IY = (int)Math.Clamp(Height * Math.Sin(Angle1), -Height + 1, Height - 1);

				this[X + IX, Y + IY] = Color;
			}
		}

		/// <summary>
		/// Draws a non-filled arc at the center of X and Y with a radius.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Radius">Radius of the arc.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="StartAngle">Angle at which to start.</param>
		/// <param name="EndAngle">Angle at which to end.</param>
		public void DrawArc(int X, int Y, ushort Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			// Quit if nothing needs to be drawn.
			if (Radius == 0)
			{
				return;
			}

			DrawArc(X, Y, Radius, Radius, Color, StartAngle, EndAngle);
		}

		#endregion

		#region Image

		/// <summary>
		/// Draws an image and X and Y.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Image">Image to draw.</param>
		/// <param name="Alpha">Option to not use alpha, it will be faster if it's disabled.</param>
		/// <exception cref="NullReferenceException">Thrown when input is null.</exception>
		public void DrawImage(int X, int Y, Graphics Image, bool Alpha = true)
		{
			// Basic null/empty check.
			if (Image == null || Image.Width == 0 || Image.Height == 0)
			{
				return;
			}

			// Quit if nothing needs to be drawn.
			if (X + Image.Width < 0 || Y + Image.Height < 0 || X >= Width || Y >= Height)
			{
				return;
			}

			// Fastest cropped draw method.
			if (!Alpha)
			{
				// Fastest copy-only draw method, fills the whole buffer.
				if (X == 0 && Y == 0 && Image.Width == this.Width && Image.Height == this.Height)
				{
					Buffer.MemoryCopy(Internal, Image.Internal, Size * 4, Size * 4);
					return;
				}

				// Get the cropped coordinates.
				uint StartX = (uint)Math.Max(X, 0);
				uint StartY = (uint)Math.Max(Y, 0);
				uint EndX = (uint)Math.Min(X + Image.Width, this.Width);
				uint EndY = (uint)Math.Min(Y + Image.Height, this.Height);

				// Get new size after crop.
				uint Height = EndY - StartY;
				uint Width = EndX - StartX;

				// Calculate destination & source offsets.
				uint Destination = StartY * this.Width + StartX;
				uint Source = (uint)((StartY - Y) * Image.Width + (StartX - X));

				// Draw each line.
				for (uint IY = 0; IY < Height; IY++)
				{
					Buffer.MemoryCopy(Image.Internal + Source, Internal + Destination, Width * 4, Width * 4);

					// Increment the offsets.
					Destination += this.Width;
					Source += Image.Width;
				}
				return;
			}

			// Last resort for alpha images.
			for (int IX = 0; IX < Image.Width; IX++)
			{
				for (int IY = 0; IY < Image.Height; IY++)
				{
					this[X + IX, Y + IY] = Image[IX, IY];
				}
			}
		}

		#endregion

		#region Text

		/// <summary>
		/// Draws a string of text at X and Y.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Text">Text to draw.</param>
		/// <param name="Font">Font to use.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="Center">Option to cented the text at X and Y.</param>
		public void DrawString(int X, int Y, string Text, Font Font, Color Color, bool Center = false)
		{
			// Basic null check.
			if (Text == null || Text.Length == 0)
			{
				return;
			}

			// Quit if nothing needs to be drawn.
			if (X >= Width || Y >= Height)
			{
				return;
			}

			// Allow the use of the 'default' keyword for text drawing.
			if (Font == default)
			{
				Font = Font.Fallback;
			}

			string[] Lines = Text.Split('\n');

			// Loop Through Each Line Of Text
			for (int Line = 0; Line < Lines.Length; Line++)
			{
				// Advanced Calculations To Determine Position
				int IX = X - (Center ? (Font.MeasureString(Text) / 2) : 0);
				int IY = Y + (Font.Size * Line) - (Center ? Font.Size * Lines.Length / 2 : 0);

				// Skip if nothig needs to be drawn.
				if (IY > Height)
				{
					return;
				}

				// Loop Though Each Char In The Line
				for (int Char = 0; Char < Lines[Line].Length; Char++)
				{
					// Skip if nothig needs to be drawn.
					if (IX > Width)
					{
						continue;
					}

					IX += DrawChar(IX, IY, Lines[Line][Char], Font, Color, Center) + 2;
				}
			}
		}

		/// <summary>
		/// Draw a single character at X and Y.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Char">Char to draw.</param>
		/// <param name="Font">Font to use.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="Center">Option to center the char at X and Y.</param>
		/// <returns>Width of the drawn character.</returns>
		public int DrawChar(int X, int Y, char Char, Font Font, Color Color, bool Center)
		{
			// Get the glyph for this char.
			Glyph Temp = Font.GetGlyph(Char);

			// Center the position if needed.
			if (Center)
			{
				Y -= Temp.Height / 2;
				X -= Temp.Width / 2;
			}

			// Return if nothing needs to be done.
			if (Temp.IsEmpty)
			{
				return Temp.Width;
			}

			// Draw all pixels.
			for (int I = 0; I < Temp.Points.Count; I++)
			{
				this[X + Temp.Points[I].X, Y + Temp.Points[I].Y] = Color;
			}

			// Return total width of the glyph.
			return Temp.Width;
		}

		#endregion

		#region Misc

		/// <summary>
		/// Rotates the image to the desired angle.
		/// </summary>
		/// <param name="Angle">Angle to rotate in.</param>
		/// <returns>Rotated image.</returns>
		public Graphics Rotate(double Angle)
		{
			Graphics Result = new(Width, Height);

			for (int X = 0; X < Width; X++)
			{
				for (int Y = 0; Y < Height; Y++)
				{
					int X2 = (int)(Math.Cos(Angle) * X - Math.Sin(Angle) * Y);
					int Y2 = (int)(Math.Sin(Angle) * X + Math.Cos(Angle) * Y);

					Result[X2, Y2] = this[X, Y];
				}
			}

			return Result;
		}

		/// <summary>
		/// Re-scales the image to the desired size.
		/// </summary>
		/// <param name="Width">New width to scale to.</param>
		/// <param name="Height">New height to scale to.</param>
		/// <param name="Mode"></param>
		/// <returns>Scaled image.</returns>
		/// <exception cref="NotImplementedException">Thrown if scale method does not exist.</exception>
		public Graphics Scale(ushort Width, ushort Height)
		{
			// Out of bounds check.
			if (Width <= 0 || Height <= 0 || Width == this.Width || Height == this.Height)
			{
				return this;
			}

			// Create a temporary buffer.
			Graphics FB = new(Width, Height);

			// Find the scale ratios.
			double XRatio = (double)this.Width / Width;
			double YRatio = (double)this.Height / Height;

			for (uint Y = 0; Y < Height; Y++)
			{
				double PY = Math.Floor(Y * YRatio);

				for (uint X = 0; X < Width; X++)
				{
					double PX = Math.Floor(X * XRatio);

					FB[Y * Width + X] = Internal[(uint)((PY * this.Width) + PX)];
				}
			}

			return FB;
		}

		/// <summary>
		/// Clears the canvas with the specified color.
		/// </summary>
		/// <param name="Color">Color to clear the canvas with.</param>
		public void Clear(Color Color)
		{
			MemoryOperations.Fill(Internal, Color.ARGB, (int)Size);
		}

		/// <summary>
		/// Clears the canvas.
		/// </summary>
		public void Clear()
		{
			Clear(Color.Black);
		}

		/// <summary>
		/// Copies the raw pixel data to an address in memory.
		/// </summary>
		/// <param name="Destination">Desination address to copy to.</param>
		public void CopyTo(uint* Destination)
		{
			Buffer.MemoryCopy(Internal, Destination, Size * 4, Size * 4);
		}

		/// <summary>
		/// Dispose of the canvas properly.
		/// </summary>
		public void Dispose()
		{
			// If the buffer is allocated, free it.
			if (Size != 0)
			{
				// NativeMemory.Free(Internal);
				Heap.Free(Internal);
			}

			GC.SuppressFinalize(this);
		}

		#endregion

		#endregion

		#region Fields

		private ushort _Height;
		private ushort _Width;

		// The internal frame buffer.
		public uint* Internal;

		#endregion
	}
}