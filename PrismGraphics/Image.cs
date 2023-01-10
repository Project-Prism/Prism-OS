using System.Runtime.InteropServices;
using PrismBinary.Compression;
using System.Text;

namespace PrismGraphics
{
	public unsafe class Image : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Image"/> class.
		/// </summary>
		/// <param name="Width">Width of the image.</param>
		/// <param name="Height">Height of the image.</param>
		public Image(uint Width, uint Height) : base(Width, Height) { }

		#region Structure

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct TGAHeader
		{
			public char Magic1;                         // must be zero
			public char ColorMap;                       // must be zero
			public char Encoding;                       // must be 2
			public short CMaporig, CMaplen, CMapent;    // must be zero
			public short X;                             // must be zero
			public short Y;                             // image's height
			public short Height;                        // image's height
			public short Width;                         // image's width
			public char ColorDepth;                     // must be 32
			public char PixelType;                      // must be 40
		}

		#endregion

		#region Methods

		/// <summary>
		/// Loads a bitmap file.
		/// </summary>
		/// <param name="Binary">Raw file data.</param>
		/// <returns>Bitmap file as an <see cref="Image"/>.</returns>
		public static Image FromBitmap(byte[] Binary)
		{
			Cosmos.System.Graphics.Bitmap BMP = new(Binary);
			Image Result = new(BMP.Width, BMP.Height);

			fixed (int* PTR = BMP.rawData)
			{
				Buffer.MemoryCopy((uint*)PTR, Result.Internal, BMP.rawData.Length * 4, BMP.rawData.Length * 4);
			}
			return Result;
		}

		/// <summary>
		/// Loads a PNG file.
		/// </summary>
		/// <param name="Binary">Raw file data.</param>
		/// <returns>PNG file as an <see cref="Image"/>.</returns>
		public static Image FromPNG(byte[] Binary)
		{
			// Check header for invalid magic data.
			if (Binary[0] != 137 ||
				Binary[1] != 80 ||
				Binary[2] != 78 ||
				Binary[3] != 71 ||
				Binary[4] != 13 ||
				Binary[5] != 10 ||
				Binary[6] != 26 ||
				Binary[7] != 10)
			{
				throw new("Invalid header magic!");
			}

			BinaryReader Reader = new(new MemoryStream(Binary));
			Reader.BaseStream.Position = 8;
			Image Result = new(0, 0);

			while (Reader.BaseStream.Position < Reader.BaseStream.Length)
			{
				uint Length = Reader.ReadUInt32();
				long Position = Reader.BaseStream.Position;
				bool IsDone = false;

				switch (Encoding.ASCII.GetString(Reader.ReadBytes(4)))
				{
					case "IHDR":
						Result.Width = Reader.ReadUInt32();
						Result.Height = Reader.ReadUInt32();
						Reader.BaseStream.Position += 5;
						break;
					case "PLTE":
						break;
					case "IDAT":
						List<byte> Buffer = new();
						Reader.BaseStream.Position += 2;
						for (int i = 2; i < Length; i++)
						{
							Buffer.Add(Reader.ReadByte());
						}

						List<byte> Data = DeflateStream.Inflate(Buffer);

						BinaryReader D = new(new MemoryStream(Data.ToArray()));

						var totalScanlines = Data.Count / (Result.Width + 1) / 4;

						var prevScanline = new List<byte>();

						for (int y = 0; y < totalScanlines; y++)
						{
							var filter = D.ReadByte();

							var dat = new List<byte>();

							for (int x = 0; x < Result.Width * 4; x++)
							{
								dat.Add(D.ReadByte());
							}
							var scanline = new List<byte>();

							if (filter == 1)
							{
								scanline.Add(dat[0]);
								for (var index = 1; index < dat.Count; index++)
								{
									scanline.Add((byte)((scanline[index - 4 > 0 ? index - 4 : 0] + dat[index - 1]) % 256));
									//scanline.Add((byte) (255));
								}
							}
							else if (filter == 2)
							{
								for (var index = 0; index < dat.Count; index++)
								{
									scanline.Add((byte)((prevScanline[index] + dat[index]) % 256));
									//scanline.Add((byte) (255));
								}
							}
							else
							{
							}

							var line = new BinaryReader(new MemoryStream(scanline.ToArray()));
							prevScanline.Clear();
							prevScanline.AddRange(scanline);

							for (int x = 0; x < Result.Width; x++)
							{
								// Read ARGB color
								Result[x, y] = line.ReadUInt32();
							}
						}

						break;
					case "IEND":
						IsDone = true;
						break;
				}

				if (IsDone)
				{
					break;
				}

				Reader.BaseStream.Position = (int)(Position + Length) + 4;
			}

			return Result;
		}

		/// <summary>
		/// Loads a TGA file.
		/// </summary>
		/// <param name="Binary">Raw file data.</param>
		/// <returns>TGA file as an <see cref="Image"/>.</returns>
		public static Image FromTGA(byte[] Binary)
		{
			Image Result = new(0, 0);
			TGAHeader* Header;

			fixed (byte* P = Binary)
			{
				Header = (TGAHeader*)P;
			}

			Result.Height = (uint)Header->Height;
			Result.Width = (uint)Header->Width;

			switch (Header->ColorDepth)
			{
				case (char)32:
					for (uint I = 0; I < Result.Width * Result.Height * 4; I++)
					{
						Result[I] = Color.FromARGB(Binary[I + 22], Binary[I + 21], Binary[I + 20], Binary[I + 19]);
					}
					break;
				case (char)24:
					for (uint I = 0; I < Result.Width * Result.Height * 3; I++)
					{
						Result[I] = Color.FromARGB(255, Binary[I + 21], Binary[I + 20], Binary[I + 19]);
					}
					break;
			}

			return Result;
		}

		/// <summary>
		/// Loads a PPM file.
		/// </summary>
		/// <param name="Binary">Raw file data.</param>
		/// <returns>PPM file as an <see cref="Image"/>.</returns>
		public static Image FromPPM(byte[] Binary)
		{
			BinaryReader Reader = new(new MemoryStream(Binary));

			if (Reader.ReadChar() != 'P' || Reader.ReadChar() != '6')
			{
				throw new("Not a PPM image!");
			}

			Reader.ReadChar(); // Skip Newline
			string widths = "", heights = "";

			for (char TMP = '\0'; TMP != ' '; TMP = Reader.ReadChar())
			{
				if (TMP == '#')
				{
					while (Reader.ReadChar() != '\n') ;
				}
				else
				{
					widths += TMP;
				}
			}
			for (char TMP = '\0'; TMP != '0' && TMP != '9'; TMP = Reader.ReadChar())
			{
				heights += TMP;
			}

			if (Reader.ReadChar() != '2' || Reader.ReadChar() != '5' || Reader.ReadChar() != '5')
			{
				throw new("Improper file data!");
			}

			Reader.ReadChar(); // Skip Newline

			Image Result = new(uint.Parse(widths), uint.Parse(heights));

			for (int Y = 0; Y < Result.Height; Y++)
			{
				for (int X = 0; X < Result.Width; X++)
				{
					Result[X, Y] = (Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte());
				}
			}

			return Result;
		}

		#endregion
	}
}