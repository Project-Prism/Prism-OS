using PrismAPI.Runtime.SSharp.Structure;
using PrismAPI.Tools.Extentions;

namespace PrismAPI.Runtime.SSharp;

public static class Tokenizer
{
	/// <summary>
	/// Generates a list of tokens with values based on the input.
	/// </summary>
	/// <param name="Input">Input S# code to get tokens from.</param>
	/// <returns>Array of tokens.</returns>
	public static Token[] GetTokens(string Input)
	{
		List<Token> Tokens = new();
		string T = string.Empty;

		for (int I = 0; I < Input.Length; I++)
		{
			// Skip whitespace.
			if (char.IsWhiteSpace(Input[I]))
			{
				continue;
			}

			// Check for valid syntax.
			if (!char.IsLetter(Input[I]) && (!char.IsNumber(Input[I]) || Input[I] == '.') && Input[I] != '.')
			{
				// Add literal text if ready.
				if (T.Length != 0)
				{
					if (double.TryParse(T, out double D))
					{
						Tokens.Add(new(TokenType.Number, D.ToString(), I));
						T = string.Empty;
					}
					else
					{
						Tokens.Add(new(TokenType.Literal, T, I));
						T = string.Empty;
					}
				}

				switch (Input[I])
				{
					case '(':
						Tokens.Add(new(TokenType.LParenthasis, "(", I));
						break;
					case ')':
						Tokens.Add(new(TokenType.RParenthasis, ")", I));
						break;
					case '{':
						Tokens.Add(new(TokenType.LCBracket, "{", I));
						break;
					case '}':
						Tokens.Add(new(TokenType.RCBracket, "}", I));
						break;
					case ';':
						Tokens.Add(new(TokenType.SemiColon, ";", I));
						break;
					case '[':
						Tokens.Add(new(TokenType.LBracket, "[", I));
						break;
					case ']':
						Tokens.Add(new(TokenType.RBracket, "]", I));
						break;
					case ':':
						Tokens.Add(new(TokenType.Colon, ":", I));
						break;
					case ',':
						Tokens.Add(new(TokenType.Comma, ",", I));
						break;
					case '"':
						Token TK = new(TokenType.String, string.Empty, I);
						while (Input[++I] != '"')
						{
							TK.Value += Input[I];
						}
						Tokens.Add(TK);
						break;
					default:
						throw new($"Unexpected token at {StringEx.GetLineColumn(Input, I)}");
				}

				// Loop
				continue;
			}

			// Concat literal.
			T += Input[I];
		}

		return Tokens.ToArray();
	}
}