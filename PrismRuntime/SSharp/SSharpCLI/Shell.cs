using PrismRuntime.SSharp.Runtime;

namespace PrismRuntime.SSharp.SSharpCLI
{
	public static class Shell
	{
		public static void Main()
		{
			Console.WriteLine("SystemSharp CLI interface, v1.0");
			Console.WriteLine("Copyleft PrismProject 2022.\n");

			while (true)
			{
				try
				{
					Console.Write("> ");

					(string, string) Input = ReadInput();

					switch (Input.Item1)
					{
						case "run":
							Executable EXE = Compiler.Compile(Input.Item2);

							while (EXE.IsEnabled)
							{
								EXE.Next();
							}
							break;
						case "exit":
							Console.WriteLine("Exiting...");
							return;
					}

				}
				catch (Exception E)
				{
					Console.WriteLine(E.Message);
				}
			}
		}

		private static (string, string) ReadInput()
		{
			string Arguments = string.Empty;
			string? B = Console.ReadLine();

			if (B == null)
			{
				return (string.Empty, string.Empty);
			}

			for (int I = B.IndexOf(' '); I < B.Length; I++)
			{
				Arguments += B[I];
			}

			return (B.Split(' ')[0], Arguments);
		}
	}
}