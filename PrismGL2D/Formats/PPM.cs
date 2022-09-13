namespace PrismGL2D.Formats
{
	/// <summary>
	/// A PPM image file loader, original design by myvar.
	/// https://github.com/Myvar/CosmosGL/blob/master/CosmosGL.System/Imaging/Formats/PPM.cs
	/// </summary>
	/// <param name="Binary"></param>
	public class PPM : Graphics
	{
		public PPM(byte[] Binary) : base(0, 0)
		{
			BinaryReader Reader = new(new MemoryStream(Binary));

			if (Reader.ReadChar() != 'P' || Reader.ReadChar() != '6')
			{
				return;
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
				return;
			}

			Reader.ReadChar(); // Skip Newline
			Width = uint.Parse(widths);
			Height = uint.Parse(heights);

			for (int Y = 0; Y < Height; Y++)
			{
				for (int X = 0; X < Width; X++)
				{
					this[X, Y] = (Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte());
				}
			}
		}
	}
}