using PrismRuntime.SSharp.Structure;
using PrismTools;

namespace PrismRuntime.SSharp
{
	public static class Compiler
	{
		/// <summary>
		/// Compiles S# code into a S# runtime-compatable executable.
		/// </summary>
		/// <param name="Input">S# Source.</param>
		/// <returns>S# Executable.</returns>
		public static Executable Compile(string Input)
		{
			// Set binary output.
			BinaryWriter Output = new(new MemoryStream());

			// Generate tokens from output
			Token[] Tokens = Tokenizer.GetTokens(Input);

			for (int I = 0; I < Tokens.Length; I++)
			{
				// If tokens are in a call order...
				if (Tokens[I].Type == TokenType.Literal && Tokens[I + 1].Type == TokenType.LParenthasis)
				{
					// Check call name.
					switch (Tokens[I++].Value)
					{
						case "Throw":
							Output.Write((byte)OPCode.System_ThrowException);
							Output.Write(Tokens[++I].Value);
							Output.Write((byte)OPCode.System_Enviroment_Exit);
							break;
						case "Console.WriteLine":
							Output.Write((byte)OPCode.System_Console_WriteLine);
							Output.Write(Tokens[++I].Value);
							break;
						case "Console.Write":
							Output.Write((byte)OPCode.System_Console_Write);
							Output.Write(Tokens[++I].Value);
							break;
						case "Exit":
							Output.Write((byte)OPCode.System_Enviroment_Exit);
							break;
						default:
							// Error if method does not exist.
							throw new($"Unexpected '{Tokens[I - 1].Value}' at {StringEx.GetLineColumn(Input, I - 1)}!");
					}
				}
			}

			// Write final exit code so app does not run forever.
			Output.Write((byte)OPCode.System_Enviroment_Exit);

			// Return raw executable.
			return new(((MemoryStream)Output.BaseStream).ToArray());
		}
	}
}