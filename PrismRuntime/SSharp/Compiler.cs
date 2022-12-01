using PrismRuntime.SSharp.Tokenizer.Structure;
using PrismRuntime.SSharp.Runtime.Structure;
using PrismRuntime.SSharp.Tokenizer;
using PrismRuntime.SSharp.Runtime;

namespace PrismRuntime.SSharp
{
	public static class Compiler
	{
		public static Executable Compile(string Input)
		{
			BinaryWriter Output = new(new MemoryStream());

			Token[] Tokens = Lexer.GetTokens(Input);

			for (int I = 0; I < Tokens.Length; I++)
			{
				if (Tokens[I].Type == TokenType.Literal && Tokens[I + 1].Type == TokenType.LParenthasis)
				{
					switch (Tokens[I++].Value)
					{
						case "Console.WriteLine":
							Output.Write((byte)OPCode.WriteLine);
							Output.Write(Tokens[++I].Value);
							break;
						case "Console.Write":
							Output.Write((byte)OPCode.Write);
							Output.Write(Tokens[++I].Value);
							break;
						case "Exit":
							Output.Write((byte)OPCode.Exit);
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