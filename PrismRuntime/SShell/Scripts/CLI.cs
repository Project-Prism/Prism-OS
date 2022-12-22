using PrismRuntime.SShell.Structure;
using PrismRuntime.SSharp.Runtime;
using PrismRuntime.SSharp;

namespace PrismRuntime.SShell.Scripts
{
	public class CLI : Script
	{
		public CLI() : base("CLI", "Starts a SSharp CLI shell instance.") { }

		public override void Invoke(string[] Args)
		{
			if (Args.Length == 0)
			{
				SSharp.SSharpCLI.Shell.Main();
				return;
			}

			if (Args[0] == "dump")
			{
				new Executable(File.ReadAllBytes(Args[1])).Dump();
				return;
			}
			if (Args[0] == "compile")
			{
				if (Args.Length < 3)
				{
					Console.WriteLine("Missing arguments!");
					return;
				}

				Console.WriteLine("Compiling " + Args[1] + "...");
				Executable EXE = Compiler.Compile(File.ReadAllText(Args[1]));

				File.WriteAllBytes(Args[2], ((MemoryStream)EXE.ROM.BaseStream).ToArray());
			}
			if (Args[0] == "execute")
			{
				Executable EXE = new(File.ReadAllBytes(Args[1]));

				while (EXE.IsEnabled)
				{
					EXE.Next();
				}
			}
		}
	}
}