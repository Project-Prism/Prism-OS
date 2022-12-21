using PrismRuntime.SSharp.Runtime;

namespace PrismRuntime.SSharp.SSharpCLI
{
	public static class Shell
	{
		public static void Main()
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

					if (Input == null || Input.Length == 0)
					{
						continue;
					}

					switch (Input)
					{
						case "Exit();":
							return;
						default:
							Executable EXE = Compiler.Compile(Input);

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
	}
}