using PrismBinary.Compression;
using System.Text;

namespace PrismGL2D.Formats
{
	/// <summary>
	/// PNG file loading class, cleaner version of Myvar's PNG loading class.
	/// Licensed under MIT.
	/// Original: https://github.com/Myvar/CosmosGL/blob/master/CosmosGL.System/Imaging/Formats/Png.cs
	/// </summary>
	public unsafe class PNG : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="PNG"/> class.
		/// </summary>
		/// <param name="Binary">Binary of a png file.</param>
		public PNG(byte[] Binary) : base(0, 0)
		{
			if (IsValid(Binary))
			{
				return;
			}

			BinaryReader Reader = new(new MemoryStream(Binary));
			Reader.BaseStream.Position = 8;

			while (Reader.BaseStream.Position < Reader.BaseStream.Length)
			{
				uint Length = Reader.ReadUInt32();
				long Position = Reader.BaseStream.Position;
				bool IsDone = false;

				switch (Encoding.ASCII.GetString(Reader.ReadBytes(4)))
				{
					case "IHDR":
						Width = Reader.ReadUInt32();
						Height = Reader.ReadUInt32();
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

						List<byte> Data = ZLIB.Inflate(Buffer);

						BinaryReader D = new BinaryReader(new MemoryStream(Data.ToArray()));

						var totalScanlines = Data.Count / (Width + 1) / 4;

						var prevScanline = new List<byte>();

						for (int y = 0; y < totalScanlines; y++)
						{
							var filter = D.ReadByte();

							var dat = new List<byte>();

							for (int x = 0; x < Width * 4; x++)
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

							for (int x = 0; x < Width; x++)
							{
								// Read ARGB color
								this[x, y] = line.ReadUInt32();
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
		}

		/// <summary>
		/// Verify a PNG file is valid via magic.
		/// </summary>
		/// <param name="Magic">Bytes of the magic numbers header.</param>
		/// <returns>Validity of the PNG file.</returns>
		public static bool IsValid(byte[] Magic)
		{
			return
				Magic[0] == 137 &&
				Magic[1] == 80 &&
				Magic[2] == 78 &&
				Magic[3] == 71 &&
				Magic[4] == 13 &&
				Magic[5] == 10 &&
				Magic[6] == 26 &&
				Magic[7] == 10;
		}
	}
}