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
				case OPCode.WriteLine:
					Console.WriteLine(ROM.ReadString());
					break;
				case OPCode.Write:
					Console.Write(ROM.ReadString());
					break;
				case OPCode.Exit:
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