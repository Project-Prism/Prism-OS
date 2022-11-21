using PrismRuntime.GLang.Tokenizer.Structure;
using PrismRuntime.GLang.Runtime.Structure;
using PrismRuntime.GLang.Tokenizer;
using PrismRuntime.GLang.Runtime;

namespace PrismRuntime.GLang
{
	public static class Compiler
	{
		public static Executable Compile(string Input)
		{
			BinaryWriter Output = new(new MemoryStream());

			GToken[] Tokens = Lexer.GetTokens(Input);

			for (int I = 0; I < Tokens.Length; I++)
			{
				if (Tokens[I].Type == GTokenType.Call)
				{
					switch (Tokens[I].Value)
					{
						case "SetMode":
							Output.Write((byte)OPCode.SetMode);
							Output.Write(uint.Parse(Tokens[++I].Value));
							Output.Write(uint.Parse(Tokens[++I].Value));
							break;
						case "Clear":
							Output.Write((byte)OPCode.Clear);
							Console.WriteLine(I);
							Output.Write(uint.Parse(Tokens[++I].Value));
							break;
						case "Draw":
							switch (Tokens[++I].Value)
							{
								case "FilledRectangle":
									Output.Write((byte)DrawCode.FilledRectangle);
									Output.Write(uint.Parse(Tokens[++I].Value)); // X
									Output.Write(uint.Parse(Tokens[++I].Value)); // Y
									Output.Write(uint.Parse(Tokens[++I].Value)); // Width
									Output.Write(uint.Parse(Tokens[++I].Value)); // Height
									break;
								case "Rectangle":
									Output.Write((byte)DrawCode.Rectangle);
									Output.Write(uint.Parse(Tokens[++I].Value)); // X
									Output.Write(uint.Parse(Tokens[++I].Value)); // Y
									Output.Write(uint.Parse(Tokens[++I].Value)); // Width
									Output.Write(uint.Parse(Tokens[++I].Value)); // Height
									break;
								case "FilledCircle":
									Output.Write((byte)DrawCode.FilledCircle);
									Output.Write(uint.Parse(Tokens[++I].Value)); // X
									Output.Write(uint.Parse(Tokens[++I].Value)); // Y
									Output.Write(uint.Parse(Tokens[++I].Value)); // Radius
									break;
								case "Circle":
									Output.Write((byte)DrawCode.Circle);
									Output.Write(uint.Parse(Tokens[++I].Value)); // X
									Output.Write(uint.Parse(Tokens[++I].Value)); // Y
									Output.Write(uint.Parse(Tokens[++I].Value)); // Radius
									break;
								default:
									throw new($"Unexpected shape '{Tokens[I].Value}' at token index {I}!");
							}
							break;
						default:
							throw new($"Unexpected '{Tokens[I].Value}' at token index {I}!");
					}
				}
			}

			Output.Write((byte)OPCode.Exit);

			return new(((MemoryStream)Output.BaseStream).ToArray());
		}
	}
}