using System.Collections.Generic;
using static PrismOS.API.KernelAPI;

namespace PrismOS.Hexi
{
	internal class Runtime
	{
		internal static List<Executable> Executables;

		internal static Function[] Functions =
		{
			new Function("KernelWriteLine", Funcs.KernelWriteLine, 2, KernelWriteLine),
			new Function("KernelMemoryAlloc", Funcs.KernelMemoryAlloc, 1, KernelMemoryAlloc),
			new Function("KernelMemorySet", Funcs.KernelMemorySet, 3, KernelMemorySet),
			new Function("KernelStartProgram", Funcs.KernelStartProgram, 2, KernelStartProgram),
			new Function("KernelStopProgram", Funcs.KernelStopProgram, 0, KernelStopProgram),
			new Function("KernelMemoryJmp", Funcs.KernelMemoryJmp, 1, KernelMemoryJmp),
		};

		internal enum Funcs
		{
			KernelWriteLine = 0x0000, // [Code][Memory Index][Length]
			KernelMemoryAlloc = 0x0001, // [Code][Size]
			KernelMemorySet = 0x0002, // [Code][Index][Length][Data]
			KernelStartProgram = 0x0003, // [Code][Memory Index][Length]
			KernelStopProgram = 0x0004, // [Code]
			KernelMemoryJmp = 0x0005, // [Code][Location]
		}

		internal static void Tick()
		{
			foreach (Executable exe in Executables)
				exe.Tick();
		}
	}
}
