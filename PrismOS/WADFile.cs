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

		public Lump GetLump(string Name)
		{
			Reader.BaseStream.Position = Offset;

			for (int I = 0; I < LumpCount; I++)
			{
				int LumpOffset = Reader.ReadInt32();
				int LumpSize = Reader.ReadInt32();
				Lump Temp = new(new(Reader.ReadChars(8)));

				if (Temp.Name == Name)
				{
					Reader.BaseStream.Position = LumpOffset;
					Temp.Reader = new(new MemoryStream(Reader.ReadBytes(LumpSize)));
					return Temp;
				}
				else
				{
					Reader.BaseStream.Position += 8;
					GCImplementation.Free(Temp.Name);
					GCImplementation.Free(Temp);
					continue;
				}
			}

			throw new ArgumentException("Item not found in WAD file.", nameof(Name));
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