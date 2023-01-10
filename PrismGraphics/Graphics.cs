// using System.Runtime.InteropServices;
using Cosmos.Core.Memory;
using Cosmos.Core;

// Note: waiting for a PR to be merged.
namespace PrismGraphics
{
	/// <summary>
	/// Graphics class, used for 2D rasterizing.
	/// </summary>
	public unsafe class Graphics : IDisposable
	{
		/// <summary>
		/// Creates a new instance of <see cref="Graphics"/>.
		/// </summary>
		/// <param name="Width">Width of the canvas.</param>
		/// <param name="Height">Height of the canvas.</param>
		public Graphics(uint Width, uint Height)
		{
			_Width = Width;
			_Height = Height;

			if (Width != 0 && Height != 0)
			{
				// Internal = (uint*)NativeMemory.Alloc(Size * 4);
				Internal = (uint*)Heap.Alloc(Size * 4);
			}
		}

		#region Fields

		public uint* Internal { get; set; }
		public uint Height
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

				_Height = value;

				if (_Width != 0)
				{
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
		}
		public uint Width
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

				_Width = value;

				if (_Height != 0)
				{
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
		}
		public uint Size
		{
			get
			{
				return _Width * _Height;
			}
		}

		private uint _Height;
		private uint _Width;

		#endregion

		#region Pixel

		/// <summary>
		/// Indexer to set or get a color value at the X and Y position.
		/// </summary>
		/// <param name="X">X position of the pixel.</param>
		/// <param name="Y">Y position of the pixel.</param>
		/// <returns>The pixel color at X and Y.</returns>
		public Color this[long X, long Y]
		{
			get
			{
				return Internal[Y * Width + X];
			}
			set
			{
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
		public Color this[long Index]
		{
			get
			{
				return Internal[Index];
			}
			set
			{
				// Blend 2 colors together if the new color has alpha.
				if (value.A < 255)
				{
					value = Color.AlphaBlend(this[Index], value);
				}

				Internal[Index] = value.ARGB;
			}
		}

		#endregion

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
		public void DrawFilledRectangle(long X, long Y, long Width, long Height, long Radius, Color Color)
		{
			// Just clear the screen if the color fills the screen.
			if (X == 0 && Y == 0 && Width == this.Width && Height == this.Height && Radius == 0 && Color.A == 255)
			{
				Clear(Color);
				return;
			}

			// Crop the coords and fill the right blocks of memory.
			if (Radius == 0 && Color.A == 255)
			{
				if (X < 0)
				{
					Width -= (uint)Math.Abs(X);
					X = 0;
				}
				if (Y < 0)
				{
					Height -= (uint)Math.Abs(Y);
					Y = 0;
				}
				if (X + Width >= this.Width)
				{
					Width -= (uint)X;
				}
				if (Y + Height >= this.Height)
				{
					Height -= (uint)Y;
				}
				for (int IY = 0; IY < Height; IY++)
				{
					MemoryOperations.Fill(Internal + ((Y + IY) * this.Width + X), Color.ARGB, (int)Width);
				}
				return;
			}

			// Fastest alpha supporting rectangle.
			if (Radius == 0)
			{
				for (long IX = X; IX < X + Width; IX++)
				{
					for (long IY = Y; IY < Y + Height; IY++)
					{
						this[IX, IY] = Color;
					}
				}
			}

			// Circular rectangle.
			else
			{
				DrawFilledCircle(X + Radius, Y + Radius, Radius, Color);
				DrawFilledCircle(X + Width - Radius - 1, Y + Radius, Radius, Color);

				DrawFilledCircle(X + Radius, Y + Height - Radius - 1, Radius, Color);
				DrawFilledCircle(X + Width - Radius - 1, Y + Height - Radius - 1, Radius, Color);

				DrawFilledRectangle(X + Radius, Y, Width - Radius * 2, Height, 0, Color);
				DrawFilledRectangle(X, Y + Radius, Width, Height - Radius * 2, 0, Color);
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
		public void DrawRectangle(long X, long Y, long Width, long Height, long Radius, Color Color)
		{ // This is essentialy just incomprehensible garbage that should never be touched, don't ever worry about it as i have worked out the corrdinates already and have verified it to be 100% correct.

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
		public void DrawRectangleGrid(long X, long Y, long BlockCountX, long BlockCountY, long BlockSize, Color BlockType1, Color BlockType2)
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
		public void DrawFilledTriangle(long X1, long Y1, long X2, long Y2, long X3, long Y3, Color Color, bool UseAntiAliasing = false)
		{
			// 28.4 fixed-point coordinates
			Y1 = (long)Math.Round(16.0f * Y1);
			Y2 = (long)Math.Round(16.0f * Y2);
			Y3 = (long)Math.Round(16.0f * Y3);

			X1 = (long)Math.Round(16.0f * X1);
			X2 = (long)Math.Round(16.0f * X2);
			X3 = (long)Math.Round(16.0f * X3);

			// Deltas
			long DX12 = X1 - X2;
			long DX23 = X2 - X3;
			long DX31 = X3 - X1;

			long DY12 = Y1 - Y2;
			long DY23 = Y2 - Y3;
			long DY31 = Y3 - Y1;

			// Fixed-point deltas
			long FDX12 = DX12 << 4;
			long FDX23 = DX23 << 4;
			long FDX31 = DX31 << 4;

			long FDY12 = DY12 << 4;
			long FDY23 = DY23 << 4;
			long FDY31 = DY31 << 4;

			// Bounding rectangle
			long MinX = (Math.Min(Math.Min(X1, X2), X3) + 0xF) >> 4;
			long MaxX = (Math.Max(Math.Max(X1, X2), X3) + 0xF) >> 4;
			long MinY = (Math.Min(Math.Min(Y1, Y2), Y3) + 0xF) >> 4;
			long MaxY = (Math.Max(Math.Max(Y1, Y2), Y3) + 0xF) >> 4;

			// Half-edge constants
			long C1 = DY12 * X1 - DX12 * Y1;
			long C2 = DY23 * X2 - DX23 * Y2;
			long C3 = DY31 * X3 - DX31 * Y3;

			// Correct for fill convention
			if (DY12 < 0 || (DY12 == 0 && DX12 > 0)) C1++;
			if (DY23 < 0 || (DY23 == 0 && DX23 > 0)) C2++;
			if (DY31 < 0 || (DY31 == 0 && DX31 > 0)) C3++;

			long CY1 = C1 + DX12 * (MinY << 4) - DY12 * (MinX << 4);
			long CY2 = C2 + DX23 * (MinY << 4) - DY23 * (MinX << 4);
			long CY3 = C3 + DX31 * (MinY << 4) - DY31 * (MinX << 4);

			for (long Y = MinY; Y < MaxY; Y++)
			{
				long CX1 = CY1;
				long CX2 = CY2;
				long CX3 = CY3;

				for (long X = MinX; X < MaxX; X++)
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

			if (UseAntiAliasing)
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
		public void DrawTriangle(long X1, long Y1, long X2, long Y2, long X3, long Y3, Color Color, bool UseAntiAliasing = false)
		{
			DrawLine(X1, Y1, X2, Y2, Color, UseAntiAliasing);
			DrawLine(X1, Y1, X3, Y3, Color, UseAntiAliasing);
			DrawLine(X2, Y2, X3, Y3, Color, UseAntiAliasing);
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
		public void DrawFilledCircle(long X, long Y, long Radius, Color Color)
		{
			if (Radius == 0)
			{
				return;
			}
			
			if (Color.A == 255)
			{ // This method fills the length of the circle from the correct X and Length values for every Y pixel in the circle, it uses memcpy to make it fast.
				long R2 = Radius * Radius;
				for (long IY = -Radius; IY <= Radius; IY++)
				{
					uint IX = (uint)(Math.Sqrt(R2 - IY * IY) + 0.5);
					uint* Offset = Internal + (Width * (Y + IY)) + X - IX;

					MemoryOperations.Fill(Offset, Color.ARGB, (int)IX * 2);
				}
			}
			else
			{
				for (long IX = -Radius; IX < Radius; IX++)
				{
					long Height = (long)Math.Sqrt((Radius * Radius) - (IX * IX));

					for (long IY = -Height; IY < Height; IY++)
					{
						this[IX + X, IY + Y] = Color;
					}
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
		public void DrawCircle(long X, long Y, long Radius, Color Color)
		{
			long IX = 0, IY = Radius, DP = 3 - 2 * Radius;

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
		public void DrawQuadraticBezierLine(long X1, long Y1, long X2, long Y2, long X3, long Y3, Color Color, int N = 6)
		{
			// X2 and Y2 is where the curve should bend to.
			if (N > 0)
			{
				long X12 = (X1 + X2) / 2;
				long Y12 = (Y1 + Y2) / 2;
				long X23 = (X2 + X3) / 2;
				long Y23 = (Y2 + Y3) / 2;
				long X123 = (X12 + X23) / 2;
				long Y123 = (Y12 + Y23) / 2;

				DrawQuadraticBezierLine(X1, Y1, X12, Y12, X123, Y123, Color, N - 1);
				DrawQuadraticBezierLine(X123, Y123, X23, Y23, X3, Y3, Color, N - 1);
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
		public void DrawCubicBezierLine(long X1, long Y1, long X2, long Y2, long X3, long Y3, long X4, long Y4, Color Color)
		{
			for (double U = 0.0; U <= 1.0; U += 0.0001)
			{
				double Power3V1 = (1 - U) * (1 - U) * (1 - U);
				double Power2V1 = (1 - U) * (1 - U);
				double Power3V2 = U * U * U;
				double Power2V2 = U * U;

				double XU = Power3V1 * X1 + 3 * U * Power2V1 * X2 + 3 * Power2V2 * (1 - U) * X3 + Power3V2 * X4;
				double YU = Power3V1 * Y1 + 3 * U * Power2V1 * Y2 + 3 * Power2V2 * (1 - U) * Y3 + Power3V2 * Y4;
				
				this[(long)XU, (long)YU] = Color;
			}
		}

		/// <summary>
		/// Draws a line from point A (X1, Y1) to point B (X2, Y2)
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X positoin 2.</param>
		/// <param name="Y2">Y position 2.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAntiAlias">Enable or disable the use of anti-aliasing.</param>
		public void DrawLine(long X1, long Y1, long X2, long Y2, Color Color, bool UseAntiAlias = false)
		{
			long DX = Math.Abs(X2 - X1), SX = X1 < X2 ? 1 : -1;
			long DY = Math.Abs(Y2 - Y1), SY = Y1 < Y2 ? 1 : -1;
			long err = (DX > DY ? DX : -DY) / 2;

			while (X1 != X2 || Y1 != Y2)
			{
				this[X1, Y1] = Color;

				if (UseAntiAlias)
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
				long E2 = err;
				if (E2 > -DX) { err -= DY; X1 += SX; }
				if (E2 < DY) { err += DX; Y1 += SY; }
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
		public void DrawAngledLine(long X, long Y, int Angle, long Radius, Color Color)
		{
			long IX = (long)(Radius * Math.Cos(Math.PI * Angle / 180));
			long IY = (long)(Radius * Math.Sin(Math.PI * Angle / 180));
			DrawLine(X, Y, X + IX, Y + IY, Color);
		}

		#endregion

		#region Arc

		/// <summary>
		/// Draws a filled arc at the center of X and Y with a radius.
		/// </summary>
		/// <param name="X">X position.</param>
		/// <param name="Y">Y position.</param>
		/// <param name="Radius">Radius of the arc.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="StartAngle">Angle at which to start.</param>
		/// <param name="EndAngle">Angle at which to end.</param>
		public void DrawFilledArc(long X, long Y, long Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			if (Radius == 0)
			{
				return;
			}

			for (uint I = 0; I < Radius; I++)
			{
				DrawArc(X, Y, I, Color, StartAngle, EndAngle);
			}
		}

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
		public void DrawArc(long X, long Y, long Width, long Height, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
			{
				double Angle1 = Math.PI * Angle / 180;

				long IX = (long)(Width * Math.Cos(Angle1));
				long IY = (long)(Height * Math.Sin(Angle1));

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
		public void DrawArc(long X, long Y, long Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
			{
				double Angle1 = Math.PI * Angle / 180;

				long IX = (long)(Radius * Math.Cos(Angle1));
				long IY = (long)(Radius * Math.Sin(Angle1));

				this[X + IX, Y + IY] = Color;
			}
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
		public void DrawImage(long X, long Y, Graphics Image, bool Alpha = true)
		{
			// Basic null check.
			if (Image == null)
			{
				throw new NullReferenceException("Cannot draw a null image file.");
			}

			// Fastest copy-only draw method, fills the whole buffer.
			if (!Alpha && X == 0 && Y == 0 && Image.Width == Width && Image.Height == Height)
			{
				Buffer.MemoryCopy(Internal, Image.Internal, Size * 4, Size * 4);
				return;
			}

			// Fastest cropped draw method.
			if (!Alpha)
			{
				uint TWidth = Image.Width;
				uint THeight = Image.Height;
				uint PadX = 0;
				uint PadY = 0;

				if (X < 0)
				{
					PadX = (uint)Math.Abs(X);
					TWidth -= PadX;
				}
				if (Y < 0)
				{
					PadY = (uint)Math.Abs(Y);
					THeight -= PadY;
				}
				if (X + Image.Width >= Width)
				{
					TWidth = Width - (uint)X;
				}
				if (Y + Image.Height >= Height)
				{
					THeight = Height - (uint)Y;
				}

				for (uint IY = 0; IY < THeight; IY++)
				{
					Buffer.MemoryCopy(Internal + PadX + X + ((PadY + Y + IY) * Width), Image.Internal + PadX + ((PadY + IY) * Image.Width), TWidth, TWidth);
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
		public void DrawString(long X, long Y, string Text, Font Font, Color Color, bool Center = false)
		{
			// Basic null check.
			if (Text == null || Text.Length == 0)
			{
				return;
			}

			string[] Lines = Text.Split('\n');

			// Loop Through Each Line Of Text
			for (int Line = 0; Line < Lines.Length; Line++)
			{
				// Advanced Calculations To Determine Position
				long IX = X - (Center ? (Font.MeasureString(Text) / 2) : 0);
				long IY = Y + (Font.Size * Line) - (Center ? Font.Size * Lines.Length / 2 : 0);

				if (IY > Height)
				{
					return;
				}

				// Loop Though Each Char In The Line
				for (int Char = 0; Char < Lines[Line].Length; Char++)
				{
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
		public uint DrawChar(long X, long Y, char Char, Font Font, Color Color, bool Center)
		{
			// Get the index of the char in the font.
			uint Index = (uint)Font.DefaultCharset.IndexOf(Char);

			if (Char == ' ')
			{
				return Font.Size2;
			}
			if (Char == '\t')
			{
				return Font.Size2 * 4;
			}
			if (Index < 0)
			{
				return Font.Size2;
			}
			if (Center)
			{
				X -= Font.Size16;
				Y -= Font.Size8;
			}

			uint MaxX = 0;
			uint SizePerFont = Font.Size * Font.Size8 * Index;

			for (int h = 0; h < Font.Size; h++)
			{
				for (int aw = 0; aw < Font.Size8; aw++)
				{
					for (int ww = 0; ww < 8; ww++)
					{
						if ((Font.Binary[SizePerFont + (h * Font.Size8) + aw] & (0x80 >> ww)) != 0)
						{
							int Max = (aw * 8) + ww;

							this[X + Max, Y + h] = Color;

							if (Max > MaxX)
							{
								MaxX = (uint)Max;
							}
						}
					}
				}
			}

			return MaxX;
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
					long X2 = (long)(Math.Cos(Angle) * X - Math.Sin(Angle) * Y);
					long Y2 = (long)(Math.Sin(Angle) * X + Math.Cos(Angle) * Y);

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
		public Graphics Scale(uint Width, uint Height)
		{
			// Out of bounds check.
			if (Width <= 0 || Height <= 0 || Width == this.Width || Height == this.Height)
			{
				return this;
			}

			// Create temporary buffer.
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
					FB[Y * Width + X] = Internal[(long)((PY * this.Width) + PX)];
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
	}
}