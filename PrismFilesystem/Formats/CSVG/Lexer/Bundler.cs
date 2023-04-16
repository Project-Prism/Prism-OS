namespace PrismFilesystem.Formats.CSVG.Lexer
{
    public class Bundler
    {
        public Bundler(string Input)
        {
            Tokenizer = new(Input);
        }

        #region Methods

        public Bundle Next()
        {
            Bundle Temp = new();

            while (!Tokenizer.IsEOF)
            {
                Token Token = Tokenizer.Next();

                if (Token.Value == "=")
                {
                    Temp.Kind = BundleKind.Definition;
                }
                if (Token.Value == "(")
                {
                    Temp.Kind = BundleKind.DrawCall;
                }
                if (Token.Value == ";")
                {
                    Temp.Tokens.Add(Token);
                    return Temp;
                }

                Temp.Tokens.Add(Token);
            }

            return Temp;
        }

        #endregion

        #region Fields

        public Tokenizer Tokenizer;

        #endregion
    }
}