namespace PrismRuntime.SSharp.Tokenizer.Structure
{
	public class Token
	{
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
}