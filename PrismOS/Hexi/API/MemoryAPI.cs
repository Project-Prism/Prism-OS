using PrismOS.Hexi.Misc;

namespace PrismOS.Hexi.API
{
    public static class MemoryAPI
    {
		public static void Allocate(Executable exe, byte[] args)
		{
			exe.Memory = new byte[args[0]];
		}

		public static void SetBytes(Executable exe, byte[] args)
		{
			int index = args[0];
			int length = args[1];

			for (int i = 0; i < length; i++)
				exe.Memory[index + i] = args[2 + i];
		}

		public static byte[] ReadBytesFromMemory(Executable exe, int index, int length)
		{
			var bytes = new byte[length];

			for (var i = 0; i < length; i++)
				bytes[i] = exe.Memory[index + i];

			return bytes;
		}
	}
}