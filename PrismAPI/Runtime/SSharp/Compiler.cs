using PrismAPI.Runtime.SSharp.Structure;
using PrismAPI.Tools.Diagnostics;
using PrismAPI.Tools.Extentions;

namespace PrismAPI.Runtime.SSharp;

public static class Compiler
{
	#region Methods

	/// <summary>
	/// Compiles S# code into a S# runtime-compatable executable.
	/// </summary>
	/// <param name="Input">S# Source.</param>
	/// <returns>S# Executable.</returns>
	public static Binary Compile(string Input)
	{
		// Set binary output.
		BinaryWriter Output = new(new MemoryStream());

		try
		{
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
						#region Throw

						case "Throw":
							Output.Write((byte)OPCode.System_Runtime_ThrowException);

							// Check for correct (string) type.
							if (Tokens[++I].Type != TokenType.String)
							{
								throw new($"{StringEx.GetLineColumn(Input, Tokens[I].Index)}: Unexpected type '{Tokens[I].Type}'.");
							}

							Output.Write(Tokens[I].Value);
							Output.Write((byte)OPCode.System_Runtime_Exit);
							break;

						#endregion
						#region WriteLine

						case "Console.WriteLine":
							Output.Write((byte)OPCode.System_Console_WriteLine);

							// Check for correct (string) type.
							if (Tokens[++I].Type != TokenType.String)
							{
								throw new($"{StringEx.GetLineColumn(Input, Tokens[I].Index)}: Unexpected type '{Tokens[I].Type}'.");
							}

							Output.Write(Tokens[I].Value);
							break;

						#endregion
						#region Write

						case "Console.Write":
							Output.Write((byte)OPCode.System_Console_Write);

							// Check for correct (string) type.
							if (Tokens[++I].Type != TokenType.String)
							{
								throw new($"{StringEx.GetLineColumn(Input, Tokens[I].Index)}: Unexpected type '{Tokens[I].Type}'.");
							}

							Output.Write(Tokens[I].Value);
							break;

						#endregion
						#region Exit

						case "Exit":
							Output.Write((byte)OPCode.System_Runtime_Exit);
							break;

						#endregion
						#region Jump

						case "Jump":
							Output.Write((byte)OPCode.System_Inline_Jump);

							// Check for correct (number) type.
							if (Tokens[++I].Type != TokenType.Number)
							{
								throw new($"{StringEx.GetLineColumn(Input, Tokens[I].Index)}: Unexpected type '{Tokens[I].Type}'.");
							}

							Output.Write(ulong.Parse(Tokens[I].Value));
							break;

						#endregion
						default:
							// Error if method does not exist.
							throw new($"Unexpected '{Tokens[I - 1].Value}' at {StringEx.GetLineColumn(Input, I - 1)}!");
					}
				}
			}

			// Write final exit code so app does not run forever.
			Output.Write((byte)OPCode.System_Runtime_Exit);

			// Return raw executable.
			return new(((MemoryStream)Output.BaseStream).ToArray());
		}
		catch (Exception E)
		{
			Debugger.WriteFull($"Critical error! ({E.Message}).", Severity.Fail);

			// Return raw executable.
			return new(((MemoryStream)Output.BaseStream).ToArray());
		}
	}

	#endregion

	#region Fields

	public static Debugger Debugger { get; set; } = new("S#");

	#endregion
}