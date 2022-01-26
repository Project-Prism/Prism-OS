using PrismOS.Hexi.Misc;

namespace PrismOS.Hexi.API
{
    public static class ProgramAPI
    {
		internal static void StartProgram(Executable exe, byte[] args)
		{
			Main.Runtime.Executables.Add(new Executable(KernelAPI.ReadBytesFromMemory(exe, args[0], args[1])));
		}

		internal static void StopProgram(Executable exe, byte[] args)
		{
			Main.Runtime.Executables.Remove(exe);
		}

		internal static void MemoryJump(Executable exe, byte[] args)
		{
			exe.Index = args[0];
		}
	}
}
