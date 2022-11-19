namespace PrismBinary.Text.XML
{
    public struct XMLReader
    {
        public XMLReader(string Source, int I = 1)
        {
            RootNode = new(Source, ref I);
            this.Source = Source[1..];
        }

        #region Definitions

        internal XMLNode RootNode;
        internal string Source;

        #endregion

        public XMLNode GetRootNode()
        {
            return RootNode;
        }
        public string GetSource()
        {
            return Source;
        }
    }
}