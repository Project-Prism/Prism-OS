using System.Numerics;
using Cosmos.Core;

namespace PrismOS
{
	public class WADFile
	{
		public WADFile(byte[] Input)
		{
			// Initialize a binary reader.
			Reader = new(new MemoryStream(Input));
			
			Identifier = new(Reader.ReadChars(4));
			LumpCount = Reader.ReadInt32();
			Offset = Reader.ReadInt32();

			// Start reading from directory.
			Reader.BaseStream.Position = Offset;
		}

		#region Methods

		private (int Offset, int Size) GetInfo(string Name)
		{
			Reader.BaseStream.Position = Offset;

			for (int I = 0; I < LumpCount; I++)
			{
				int LumpOffset = Reader.ReadInt32();
				int Size = Reader.ReadInt32();
				string Temp = new(Reader.ReadChars(8));

				if (Temp == Name)
				{
					return (LumpOffset, Size);
				}
				else
				{
					Reader.BaseStream.Position += 8;
					GCImplementation.Free(Temp);
					continue;
				}
			}

			throw new ArgumentException("Item not found in WAD file.", nameof(Name));
		}

		public List<Vector2> ReadVertexes(string Name)
		{
			(int Offset, int Size) Info = GetInfo(Name);
			Reader.BaseStream.Position = Info.Offset;
			List<Vector2> Vertecies = new();

			while (Reader.BaseStream.Position < Info.Offset + Info.Size)
			{
				Vertecies.Add(new(Reader.ReadInt16(), Reader.ReadInt16()));
			}

			return Vertecies;
		}

		public List<LineDef> ReadLines(string Name)
		{
			(int Offset, int Size) Info = GetInfo(Name);
			Reader.BaseStream.Position = Info.Offset;
			List<LineDef> Lines = new();

			while (Reader.BaseStream.Position < Info.Offset + Info.Size)
			{
				Lines.Add(new()
				{
					StartVertex = Reader.ReadUInt16(),
					EndVertex = Reader.ReadUInt16(),
					Flags = Reader.ReadUInt16(),
					Special = Reader.ReadUInt16(),
					Sector = Reader.ReadUInt16(),
					FrontSideDef = Reader.ReadUInt16(),
					BackSideDef = Reader.ReadUInt16(),
				});
			}

			return Lines;
		}

		#endregion

		#region Fields

		public readonly string Identifier;
		private readonly int LumpCount;
		private readonly int Offset;
		public BinaryReader Reader;

		#endregion
	}
}