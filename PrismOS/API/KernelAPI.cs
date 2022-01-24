using PrismOS.Hexi;
using System;
using System.Text;
using static PrismOS.Hexi.Runtime;

namespace PrismOS.API
{
	// This is a simple low level API example for your OS.
	internal class KernelAPI
	{
		internal static void KernelWriteLine(Executable exe, byte[] args)
		{
			Console.WriteLine(Encoding.ASCII.GetString(exe.Memory, args[0], args[1]));
		}

		internal static void KernelMemoryAlloc(Executable exe, byte[] args)
		{
			exe.Memory = new byte[args[0]];
		}

		internal static void KernelMemorySet(Executable exe, byte[] args)
		{
			int index = args[0];
			int length = args[1];

			for (int i = 0; i < length; i++)
				exe.Memory[index + i] = args[2 + i];
		}

		internal static void KernelStartProgram(Executable exe, byte[] args)
		{
			Executables.Add(new Executable(ReadBytesFromMemory(exe, args[0], args[1])));
		}

		internal static void KernelStopProgram(Executable exe, byte[] args)
		{
			for (var i = 0; i < Executables.Count; i++)
				if (Executables[i].Id == exe.Id)
				{
					Executables.RemoveAt(i);
					break;
				}
		}

		internal static void KernelMemoryJmp(Executable exe, byte[] args)
		{
			exe.Index = args[0];
		}

		private static byte[] ReadBytesFromMemory(Executable exe, int index, int length)
		{
			var bytes = new byte[length];

			for (var i = 0; i < length; i++)
				bytes[i] = exe.Memory[index + i];

			return bytes;
		}
	}
}
