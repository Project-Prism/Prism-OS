using PrismRuntime.GLang.Tokenizer.Structure;

namespace PrismRuntime.GLang.Tokenizer
{
	public static class Lexer
	{
		private static string ReadArgument(ref int I, string Input)
		{
			while (ShouldSkip(Input[I]) || Input[I] == '(')
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
		public static GToken[] GetTokens(string Input)
		{
			List<GToken> Tokens = new();
			string T = string.Empty;

			for (int I = 0; I < Input.Length; I++)
			{
				// Skip whitespace.
				if (ShouldSkip(Input[I]))
				{
					continue;
				}

				// Begin call read if '(' is detected.
				if (Input[I] == '(')
				{
					// Add call name and '(' tokens.
					Tokens.Add(new(GTokenType.Call, T));
					T = string.Empty;

					// End if function has no args.
					if (Input[I + 1] == ')')
					{
						if (Input[I + 2] != ';')
						{
							throw new($"Missing ';' at index {I}.");
						}
						I += 3;
						continue;
					}

					// Add arguments as tokens.
					while (Input[I] != ')')
					{
						Tokens.Add(new(GTokenType.Literal, ReadArgument(ref I, Input)));

						if (long.TryParse(Tokens[^1].Value, out _))
						{
							Tokens[^1].Type = GTokenType.Number;
						}
					}

					// Throw exception if line isn't complete.
					if (Input[I++] != ')' && Input[I] != ';')
					{
						throw new($"Unexpected end of line at index {I}.");
					}
				}

				// Add literal for call names
				if (!ShouldSkip(Input[I]))
				{
					T += Input[I];
				}
			}

			return Tokens.ToArray();
		}
		private static bool ShouldSkip(char C)
		{
			return
				C == ':' ||
				C == ';' ||
				C == ',' ||
				C == ' ' ||
				C == '\n' ||
				C == '\t' ||
				C == '\r';
		}
	}
}