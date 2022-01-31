using PrismOS.Hexi.API;

namespace PrismOS.Hexi.Misc
{
	public delegate void MethodDefinition(Executable exe, byte[] args);

	public class Function
	{
		public string Name;
		public byte Type;
		public int Arguments;
		public MethodDefinition Definition;

		public static Function[] Functions { get; } =
		{
			new Function("ConsoleAPI.WriteLine", 0x0000, 2, ConsoleAPI.WriteLine),
			new Function("ConsoleAPI.Write", 0x0001, 2, ConsoleAPI.Write),

			new Function("MemoryAPI.Allocate", 0x0002, 1, MemoryAPI.Allocate),
			new Function("MemoryAPI.SetBytes", 0x0003, 3, MemoryAPI.SetBytes),

			new Function("ProgramAPI.ProgramStart", 0x0004, 2, ProgramAPI.StartProgram),
			new Function("ProgramAPI.ProgramStop", 0x0005, 1, ProgramAPI.StopProgram),
			new Function("ProgramAPI.Jump", 0x0006, 1, ProgramAPI.MemoryJump),
		};

		public Function(string Name, byte Type, int ArgumentCount, MethodDefinition Definition)
		{
			this.Name = Name;
			this.Type = Type;
			Arguments = ArgumentCount;
			this.Definition = Definition;
		}
	}
}