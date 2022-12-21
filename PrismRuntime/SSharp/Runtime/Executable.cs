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