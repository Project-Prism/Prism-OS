using System;
using System.Text;
using PrismOS.Hexi;
using static PrismOS.Hexi.Main.Runtime;

namespace PrismOS.Hexi.Misc
{
	// This is a simple low level API example for your OS.
	public static class KernelAPI
	{
		public static byte[] ReadBytesFromMemory(Executable exe, int index, int length)
		{
			var bytes = new byte[length];

			for (var i = 0; i < length; i++)
				bytes[i] = exe.Memory[index + i];

			return bytes;
		}
	}
}