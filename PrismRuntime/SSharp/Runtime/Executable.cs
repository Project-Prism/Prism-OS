using PrismRuntime.SSharp.Runtime.Structure;

namespace PrismRuntime.SSharp.Runtime
{
	public unsafe class Executable
	{
		public Executable(byte[] Binary)
		{
			ROM = new(new MemoryStream(Binary));
			IsEnabled = true;
		}

		#region Methods

		public void Dump()
		{
			long BPoint = ROM.BaseStream.Position;
			ROM.BaseStream.Position = 0;

			for (; ROM.BaseStream.Position < ROM.BaseStream.Length - 1;)
			{
				switch ((OPCode)ROM.ReadByte())
				{
					case OPCode.System_ThrowException:
						Console.WriteLine($"System_ThrowException: {ROM.ReadString()}");
						break;
					case OPCode.System_Console_WriteLine:
						Console.WriteLine($"System_Console_WriteLine: {ROM.ReadString()}");
						break;
					case OPCode.System_Console_Write:
						Console.WriteLine($"System_Console_Write: {ROM.ReadString()}");
						break;
					case OPCode.System_Enviroment_Exit:
						Console.WriteLine("System_Enviroment_Exit");
						break;
				}
			}

			ROM.BaseStream.Position = BPoint;
		}

		public void Next()
		{
			if (ROM.BaseStream.Position == ROM.BaseStream.Length)
			{
				IsEnabled = false;
			}
			if (!IsEnabled)
			{
				return;
			}

			switch ((OPCode)ROM.ReadByte())
			{
				case OPCode.System_ThrowException:
					Console.WriteLine($"Exception: " + ROM.ReadString());
					break;
				case OPCode.System_Console_WriteLine:
					Console.WriteLine(ROM.ReadString());
					break;
				case OPCode.System_Console_Write:
					Console.Write(ROM.ReadString());
					break;
				case OPCode.System_Enviroment_Exit:
					IsEnabled = false;
					break;
			}
		}

		#endregion

		#region Fields

		public BinaryReader ROM;
		public bool IsEnabled;

		#endregion
	}
}