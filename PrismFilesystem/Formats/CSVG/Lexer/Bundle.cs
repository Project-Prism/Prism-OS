namespace PrismFilesystem.Formats.CSVG.Lexer
{
    public class Bundle
    {
        public Bundle()
        {
            Tokens = new();
        }

        #region Fields

        public List<Token> Tokens;
        public BundleKind Kind;

        #endregion
    }
}