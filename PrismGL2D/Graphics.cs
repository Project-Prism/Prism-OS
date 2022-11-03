using Cosmos.Core.Memory;
using Cosmos.Core;

namespace PrismGL2D
{
	/// <summary>
	/// Graphics class, used for drawing.
	/// </summary>
	public unsafe class Graphics : IDisposable
	{
		/// <summary>
		/// Creates new instance of <see cref="Graphics"/>.
		/// </summary>
		/// <param name="Width">Width of the canvas.</param>
		/// <param name="Height">Height of the canvas.</param>
		public Graphics(uint Width, uint Height)
		{
			_Width = Width;
			_Height = Height;
			
			if (Width != 0 && Height != 0)
			{
				fixed (uint* P = new uint[Size])
				{
					Internal = P;
				}
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
						Internal = (uint*)Heap.Alloc(Size * 4);
					}
					else
					{
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
						Internal = (uint*)Heap.Alloc(Size * 4);
					}
					else
					{
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
		/// Set a color value at the X and Y position.
		/// </summary>
		/// <param name="X">X position of the pixel.</param>
		/// <param name="Y">Y position of the pixel.</param>
		/// <returns>Pixel color at X and Y.</returns>
		public Color this[uint X, uint Y]
		{
			get
			{
				return this[(int)X, (int)Y];
			}
			set
			{
				this[(int)X, (int)Y] = value;
			}
		}

		/// <summary>
		/// Set a color value at the X and Y position.
		/// </summary>
		/// <param name="X">X position of the pixel.</param>
		/// <param name="Y">Y position of the pixel.</param>
		/// <returns>Pixel color at X and Y.</returns>
		public Color this[int X, int Y]
		{
			get
			{
				if (X >= Width || Y >= Height || X < 0 || Y < 0)
				{
					return Color.Black;
				}
				return Color.FromARGB(Internal[Y * Width + X]);
			}
			set
			{
				if (value.A == 0 || X >= Width || Y >= Height || X < 0 || Y < 0)
				{
					return;
				}
				if (value.A < 255)
				{
					value = Color.AlphaBlend(this[X, Y], value);
				}
				Internal[Y * Width + X] = value.ARGB;
			}
		}

		/// <summary>
		/// Set a color value at the index position.
		/// </summary>
		/// <param name="Index">Index position of the pixel.</param>
		/// <returns>Pixel color at the linear index.</returns>
		public Color this[uint Index]
		{
			get
			{
				return this[(int)Index];
			}
			set
			{
				this[(int)Index] = value;
			}
		}

		/// <summary>
		/// Set a color value at the index position.
		/// </summary>
		/// <param name="Index">Index position of the pixel.</param>
		/// <returns>Pixel color at the linear index.</returns>
		public Color this[int Index]
		{
			get
			{
				if (Index >= Size || Index < 0)
				{
					return Color.Black;
				}

				return Color.FromARGB(Internal[Index]);
			}
			set
			{
				if (value.A == 0 || Index >= Size || Index < 0)
				{
					return;
				}
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
		public void DrawFilledRectangle(int X, int Y, uint Width, uint Height, uint Radius, Color Color)
		{
			if (X == 0 && Y == 0 && Width == this.Width && Height == this.Height && Radius == 0 && Color.A == 255)
			{
				Clear(Color);
				return;
			}
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
			else
			{
				DrawFilledCircle((int)(X + Radius), (int)(Y + Radius), Radius, Color);
				DrawFilledCircle((int)(X + Width - Radius - 1), (int)(Y + Radius), Radius, Color);

				DrawFilledCircle((int)(X + Radius), (int)(Y + Height - Radius - 1), Radius, Color);
				DrawFilledCircle((int)(X + Width - Radius - 1), (int)(Y + Height - Radius - 1), Radius, Color);

				DrawFilledRectangle((int)(X + Radius), Y, Width - Radius * 2, Height, 0, Color);
				DrawFilledRectangle(X, (int)(Y + Radius), Width, Height - Radius * 2, 0, Color);
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
		public void DrawRectangle(int X, int Y, uint Width, uint Height, uint Radius, Color Color)
		{ // This is essentialy just incomprehensible garbage that should never be touched, don't ever worry about it as i have worked out the corrdinates already and have verified it to be 100% correct.

			if (Radius > 0)
			{
				DrawArc((int)(Radius + X), (int)(Radius + Y), Radius, Color, 180, 270); // Top left
				DrawArc((int)(X + Width - Radius), (int)(Y + Height - Radius), Radius, Color, 0, 90); // Bottom right
				DrawArc((int)(Radius + X), (int)(Y + Height - Radius), Radius, Color, 90, 180); // Bottom left
				DrawArc((int)(X + Width - Radius), (int)(Radius + Y), Radius, Color, 270, 360);
			}
			DrawLine((int)(X + Radius), Y, (int)(X + Width - Radius), Y, Color); // Top Line
			DrawLine((int)(X + Radius), (int)(Y + Height), (int)(X + Width - Radius), (int)(Height + Y), Color); // Bottom Line
			DrawLine(X, (int)(Y + Radius), X, (int)(Y + Height - Radius), Color); // Left Line
			DrawLine((int)(X + Width), (int)(Y + Radius), (int)(Width + X), (int)(Y + Height - Radius), Color); // Right Line
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
		public void DrawRectangleGrid(int X, int Y, uint BlockCountX, uint BlockCountY, uint BlockSize, Color BlockType1, Color BlockType2)
		{
			for (int IX = 0; IX < BlockCountX; IX++)
			{
				for (int IY = 0; IY < BlockCountY; IY++)
				{
					if ((IX + IY) % 2 == 0)
					{
						DrawFilledRectangle((int)(X + IX * BlockSize), (int)(Y + IY * BlockSize), BlockSize, BlockSize, 0, BlockType1);
					}
					else
					{
						DrawFilledRectangle((int)(X + IX * BlockSize), (int)(Y + IY * BlockSize), BlockSize, BlockSize, 0, BlockType2);
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
		public void DrawFilledTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, bool UseAntiAliasing = false)
		{
			// 28.4 fixed-point coordinates
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
			int minx = (Math.Min(Math.Min(X1, X2), X3) + 0xF) >> 4;
			int maxx = (Math.Max(Math.Max(X1, X2), X3) + 0xF) >> 4;
			int miny = (Math.Min(Math.Min(Y1, Y2), Y3) + 0xF) >> 4;
			int maxy = (Math.Max(Math.Max(Y1, Y2), Y3) + 0xF) >> 4;

			// Half-edge constants
			int C1 = DY12 * X1 - DX12 * Y1;
			int C2 = DY23 * X2 - DX23 * Y2;
			int C3 = DY31 * X3 - DX31 * Y3;

			// Correct for fill convention
			if (DY12 < 0 || (DY12 == 0 && DX12 > 0)) C1++;
			if (DY23 < 0 || (DY23 == 0 && DX23 > 0)) C2++;
			if (DY31 < 0 || (DY31 == 0 && DX31 > 0)) C3++;

			int CY1 = C1 + DX12 * (miny << 4) - DY12 * (minx << 4);
			int CY2 = C2 + DX23 * (miny << 4) - DY23 * (minx << 4);
			int CY3 = C3 + DX31 * (miny << 4) - DY31 * (minx << 4);

			for (int Y = miny; Y < maxy; Y++)
			{
				int CX1 = CY1;
				int CX2 = CY2;
				int CX3 = CY3;

				for (int X = minx; X < maxx; X++)
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
		public void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, bool UseAntiAliasing = false)
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
		public void DrawFilledCircle(int X, int Y, uint Radius, Color Color)
		{
			if (Radius == 0)
			{
				return;
			}
			if (Color.A == 255)
			{ // This method fills the length of the circle from the correct X and Length values for every Y pixel in the circle, it uses memcpy to make it fast.
				uint R2 = Radius * Radius;
				for (int IY = (int)-Radius; IY <= Radius; IY++)
				{
					uint IX = (uint)(Math.Sqrt(R2 - IY * IY) + 0.5);
					uint* Offset = Internal + (Width * (Y + IY)) + X - IX;

					MemoryOperations.Fill(Offset, Color.ARGB, (int)IX * 2);
				}
			}
			else
			{
				for (int IX = (int)-Radius; IX < Radius; IX++)
				{
					int Height = (int)Math.Sqrt((Radius * Radius) - (IX * IX));

					for (int IY = -Height; IY < Height; IY++)
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
		public void DrawCircle(int X, int Y, uint Radius, Color Color)
		{
			int IX = 0, IY = (int)Radius, DP = (int)(3 - 2 * Radius);

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
		public void DrawQuadraticBezierLine(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color, int N = 6)
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
		/// Draws a line from point A (X1, Y1) to point B (X2, Y2)
		/// </summary>
		/// <param name="X1">X position 1.</param>
		/// <param name="Y1">Y position 1.</param>
		/// <param name="X2">X positoin 2.</param>
		/// <param name="Y2">Y position 2.</param>
		/// <param name="Color">Color to draw with.</param>
		/// <param name="UseAntiAlias">Enable or disable the use of anti-aliasing.</param>
		public void DrawLine(int X1, int Y1, int X2, int Y2, Color Color, bool UseAntiAlias = false)
		{
			int DX = Math.Abs(X2 - X1), SX = X1 < X2 ? 1 : -1;
			int DY = Math.Abs(Y2 - Y1), SY = Y1 < Y2 ? 1 : -1;
			int err = (DX > DY ? DX : -DY) / 2;

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
				int e2 = err;
				if (e2 > -DX) { err -= DY; X1 += SX; }
				if (e2 < DY) { err += DX; Y1 += SY; }
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
		public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
		{
			int IX = (int)(Radius * Math.Cos(Math.PI * Angle / 180));
			int IY = (int)(Radius * Math.Sin(Math.PI * Angle / 180));
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
		public void DrawFilledArc(int X, int Y, uint Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
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
		public void DrawArc(int X, int Y, uint Width, uint Height, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
			{
				double Angle1 = Math.PI * Angle / 180;
				int IX = (int)(Width * Math.Cos(Angle1));
				int IY = (int)(Height * Math.Sin(Angle1));
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
		public void DrawArc(int X, int Y, uint Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
		{
			for (double Angle = StartAngle; Angle < EndAngle; Angle += 0.5)
			{
				double Angle1 = Math.PI * Angle / 180;
				int IX = (int)(Radius * Math.Cos(Angle1));
				int IY = (int)(Radius * Math.Sin(Angle1));
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
		/// <exception cref="Exception"></exception>
		public void DrawImage(int X, int Y, Graphics Image, bool Alpha = true)
		{
			if (Image == null)
			{
				throw new Exception("Cannot draw a null image file.");
			}
			if (!Alpha && X == 0 && Y == 0 && Image.Width == Width && Image.Height == Height)
			{
				MemoryOperations.Copy(Internal, Image.Internal, (int)Size);
				return;
			}
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
					MemoryOperations.Copy(Internal + PadX + X + ((PadY + Y + IY) * Width), Image.Internal + PadX + ((PadY + IY) * Image.Width), (int)TWidth);
				}
				return;
			}

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
			if (Text == null || Text.Length == 0)
			{
				return;
			}
			string[] Lines = Text.Split('\n');

			// Loop Through Each Line Of Text
			for (int Line = 0; Line < Lines.Length; Line++)
			{
				// Advanced Calculations To Determine Position
				int IX = X - (Center ? ((int)Font.MeasureString(Text) / 2) : 0);
				int IY = (int)(Y + (Font.Size * Line) - (Center ? Font.Size * Lines.Length / 2 : 0));

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
					IX += (int)(DrawChar(IX, IY, Lines[Line][Char], Font, Color, Center) + 2);
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
		public uint DrawChar(int X, int Y, char Char, Font Font, Color Color, bool Center)
		{
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
				X -= (int)Font.Size16;
				Y -= (int)Font.Size8;
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
							int max = (aw * 8) + ww;

							int x = X + max;
							int y = Y + h;

							this[x, y] = Color;

							if (max > MaxX)
							{
								MaxX = (uint)max;
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
		public Graphics Scale(uint Width, uint Height)
		{
			if (Width <= 0 || Height <= 0 || Width == this.Width || Height == this.Height)
			{
				return this;
			}

			Graphics FB = new(Width, Height);
			double XRatio = (double)this.Width / Width;
			double YRatio = (double)this.Height / Height;
			for (uint Y = 0; Y < Height; Y++)
			{
				double PY = Math.Floor(Y * YRatio);
				for (uint X = 0; X < Width; X++)
				{
					double PX = Math.Floor(X * XRatio);
					FB[Y * Width + X] = Internal[(int)((PY * this.Width) + PX)];
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
			MemoryOperations.Copy(Destination, Internal, (int)Size);
		}

		/// <summary>
		/// Dispose of the canvas properly.
		/// </summary>
		public void Dispose()
		{
			if (Size != 0)
			{
				Heap.Free(Internal);
			}
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}