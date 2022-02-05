using PrismOS.Core.Hexi.Misc;
using System.Text;
using System.IO;

namespace PrismOS.Core.Hexi.API
{
    public static class ProgramAPI
    {
		internal static void StartProgram(Executable exe, byte[] args)
		{
			Main.Runtime.Executables.Add(new Executable(MemoryAPI.ReadBytesFromMemory(exe, args[0], args[1])));
		}

		public static void StartProgramFromDisk(Executable exe, byte[] args)
        {
			string Path = Encoding.ASCII.GetString(exe.Memory, args[0], args[1]);
			Main.Runtime.Executables.Add(new Executable(File.ReadAllBytes(Path)));
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