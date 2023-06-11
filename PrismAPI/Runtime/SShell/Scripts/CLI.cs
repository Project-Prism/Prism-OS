using PrismAPI.Runtime.SSharp;

namespace PrismAPI.Runtime.SShell.Scripts;

public class CLI : Script
{
	public CLI() : base("CLI", "Starts a SSharp CLI shell instance.")
	{
		AdvancedDescription =
			"CLI [option] [file(s)]\n" +
			"======================\n" +
			"compile - Compile a file and output to the second file argument.\n" +
			"dump - Show instructions in a file as text.";
	}

	public override void Invoke(string[] Args)
	{
		if (Args.Length == 0)
		{
			Console.WriteLine("SystemSharp CLI interface, v2");
			Console.WriteLine("Copyleft PrismProject 2022.\n");

			while (true)
			{
				try
				{
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write(">>> ");
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.ForegroundColor = ConsoleColor.Black;

					string? Input = Console.ReadLine();
					Console.ResetColor();

					if (string.IsNullOrEmpty(Input))
					{
						continue;
					}

					switch (Input)
					{
						case "Exit();":
							return;
						default:
							Binary EXE = Compiler.Compile(Input);

							while (EXE.IsEnabled)
							{
								EXE.Next();
							}
							break;
					}
				}
				catch (Exception E)
				{
					Console.WriteLine(E.Message);
				}
			}
		}

		if (Args[0] == "compile")
		{
			if (Args.Length < 3)
			{
				Console.WriteLine("Missing arguments!");
				return;
			}

			Console.WriteLine("Compiling " + Args[1] + "...");
			Binary EXE = Compiler.Compile(File.ReadAllText(Args[1]));

			File.WriteAllBytes(Args[2], ((MemoryStream)EXE.ROM.BaseStream).ToArray());
		}
		if (Args[0] == "dump")
		{
			new Binary(File.ReadAllBytes(Args[1])).Dump();
			return;
		}
	}
}