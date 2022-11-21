namespace PrismRuntime.GLang.Tokenizer.Structure
{
	public class GToken
	{
		public GToken(GTokenType Type, string Value)
		{
			this.Type = Type;
			this.Value = Value;
		}

		#region Fields

		public GTokenType Type;
		public string Value;

		#endregion
	}
}