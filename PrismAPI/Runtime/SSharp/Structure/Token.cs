namespace PrismAPI.Runtime.SSharp.Structure;

public class Token
{
	/// <summary>
	/// Creates a new instance of the <see cref="Token"/> class.
	/// </summary>
	/// <param name="Type">The type of token.</param>
	/// <param name="Value">The text value of the token.</param>
	/// <param name="Index">The index of the token in the source code.</param>
	public Token(TokenType Type, string Value, int Index)
	{
		this.Type = Type;
		this.Value = Value;
		this.Index = Index;
	}

	#region Fields

	public TokenType Type;
	public string Value;
	public int Index;

	#endregion
}