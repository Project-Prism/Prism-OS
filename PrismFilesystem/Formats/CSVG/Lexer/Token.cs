namespace PrismFilesystem.Formats.CSVG.Lexer
{
    public class Token
    {
        public Token(string Value)
        {
            this.Value = Value;
        }

        public Token()
        {
            Value = string.Empty;
        }

        #region Fields

        public string Value;

        #endregion
    }
}