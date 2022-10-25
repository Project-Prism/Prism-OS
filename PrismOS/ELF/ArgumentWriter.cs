namespace PrismOS.ELF
{
	public unsafe class ArgumentWriter
	{
		private readonly BinaryWriter Writer;

		public ArgumentWriter()
		{
			//clear call stack
			for (int k = 0; k < 1024; k++)
			{
				((byte*)Invoker.Stack)[k] = 0;
			}

			Writer = new BinaryWriter(new UnmanagedMemoryStream((byte*)Invoker.Stack, long.MaxValue));
			Writer.BaseStream.Position = 50;
		}


		public ArgumentWriter Push(char c)
		{
			Writer.Write(c);
			return this;
		}

		public ArgumentWriter Push(byte c)
		{
			Writer.Write(c);
			return this;
		}

		public ArgumentWriter Push(short c)
		{
			Writer.Write(c);
			return this;
		}

		public ArgumentWriter Push(int c)
		{
			Writer.Write(c);
			return this;
		}

		public ArgumentWriter Push(uint c)
		{
			Writer.Write(c);
			return this;
		}

	}
}