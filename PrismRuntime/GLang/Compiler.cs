using PrismRuntime.GLang.Structure;

namespace PrismRuntime.GLang
{
	public static class Compiler
	{
		public static Executable Compile(string Input)
		{
			BinaryWriter Output = new(new MemoryStream());
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
						case "SetMode":
							Output.Write((byte)OPCode.SetMode);
							Output.Write(Read32(ref I, Input)); // Width
							Output.Write(Read32(ref I, Input)); // Height
							break;
						case "Clear":
							Output.Write((byte)OPCode.Clear);
							Output.Write(Read32(ref I, Input)); // Color
							break;
						case "Draw":
							Output.Write((byte)OPCode.Draw);

							switch (ReadArgument(ref I, Input))
							{
								case "FilledRectangle":
									Output.Write((byte)DrawCode.FilledRectangle);
									Output.Write(Read32(ref I, Input)); // X
									Output.Write(Read32(ref I, Input)); // Y
									Output.Write(Read32(ref I, Input)); // Width
									Output.Write(Read32(ref I, Input)); // Height
									break;
								case "Rectangle":
									Output.Write((byte)DrawCode.Rectangle);
									Output.Write(Read32(ref I, Input)); // X
									Output.Write(Read32(ref I, Input)); // Y
									Output.Write(Read32(ref I, Input)); // Width
									Output.Write(Read32(ref I, Input)); // Height
									break;
								case "FilledCircle":
									Output.Write((byte)DrawCode.FilledCircle);
									Output.Write(Read32(ref I, Input)); // X
									Output.Write(Read32(ref I, Input)); // Y
									Output.Write(Read32(ref I, Input)); // Radius
									break;
								case "Circle":
									Output.Write((byte)DrawCode.Circle);
									Output.Write(Read32(ref I, Input)); // X
									Output.Write(Read32(ref I, Input)); // Y
									Output.Write(Read32(ref I, Input)); // Radius
									break;
							}

							Output.Write(Read32(ref I, Input)); // Color
							break;
						case "Exit":
							Output.Write((byte)OPCode.Exit);
							break;
						default:
							throw new($"Invalid command '{T}'!");
					}
					while (Input[I] == ')' || Input[I] == ';' || Input[I] == ' ' || Input[I] == '\n')
					{
						I++;
					}
					T = "";
				}

				T += Input[I];
			}

			Output.Write((byte)OPCode.Exit);

			return new(((MemoryStream)Output.BaseStream).ToArray());
		}

		private static string ReadArgument(ref int I, string Input)
		{
			while (Input[I] == ',' || Input[I] == ' ' || Input[I] == '\n' || Input[I] == '\t')
			{
				I++;
			}

			string T = string.Empty;
			while (Input[I] != ',' && Input[I] != ')')
			{
				T += Input[I++];
			}
			return T;
		}
		private static uint Read32(ref int I, string Input)
		{
			return uint.Parse(ReadArgument(ref I, Input));
		}
	}
}