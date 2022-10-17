namespace PrismGL2D.Formats.Targa
{
	public unsafe class Targa : Graphics
	{
		public Targa(byte[] Buffer) : base(0, 0)
		{
			fixed (byte* P = Buffer)
			{
				Header = (Header*)P;
			}

			Height = (uint)Header->Height;
			Width = (uint)Header->Width;

			switch (Header->ColorDepth)
			{
				case (char)32:
					for (uint I = 0; I < Width * Height * 4; I++)
					{
						this[I] = Color.FromARGB(Buffer[I + 22], Buffer[I + 21], Buffer[I + 20], Buffer[I + 19]);
					}
					break;
				case (char)24:
					for (uint I = 0; I < Width * Height * 3; I++)
					{
						this[I] = Color.FromARGB(255, Buffer[I + 21], Buffer[I + 20], Buffer[I + 19]);
					}
					break;
			}
		}

		public Header* Header;
	}
}