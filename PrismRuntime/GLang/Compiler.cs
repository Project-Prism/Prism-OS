using PrismRuntime.GLang.Structure;

namespace PrismRuntime.GLang
{
	public static class Compiler
	{
		public static Executable Compile(string Input)
		{
			List<byte> Output = new();
			string T = string.Empty;

			for (int I = 0; I < Input.Length; I++)
			{
				if (Input[I] == ' ' || Input[I] == '\n' || Input[I] == '\r')
				{
					continue;
				}

				if (Input[I] == '(')
				{
					if (T.Length == 0)
					{
						throw new($"Unexpected token '(' at char {I}.");
					}

					I++;
					switch (T)
					{
						case "Clear":
							Output.Add((byte)OPCode.Clear);
							break;
						case "Draw":
							Output.Add((byte)OPCode.Draw);
							break;
						case "Exit":
							Output.Add((byte)OPCode.Exit);
							break;
						default:
							throw new($"Invalid command '{T}'!");
					}
					T = "";
				}


				T += Input[I];
			}

			return new(Output.ToArray());
		}
	}
}