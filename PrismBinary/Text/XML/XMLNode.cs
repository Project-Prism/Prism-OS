namespace PrismBinary.Text.XML
{
    public unsafe struct XMLNode
    {
        public XMLNode(string Source, ref int I)
        {
            Children = new();
            Value = "";
            Tag = "";

            for (; I < Source.Length; I++)
            {
                if (Source[I] == '<')
                {
                    while (Source[I] != '>')
                    {
                        Tag += Source[I++];
                    }

                    while(I < Source.Length)
                    {
                        if (Source[I] == '<' && Source[I] != '/')
                        {
                            I++;
                            Children.Add(new(Source, ref I));
                            continue;
                        }
                        if (Source[I] == '<' && Source[I] == '/')
                        {
                            I += Tag.Length + 2;
                            continue;
                        }
                        
                        while (Source[I] != '<')
                        {
                            Value += Source[I++];
                        }
                    }
                }
            }
        }

        #region Definitions

        internal List<XMLNode> Children;
        internal string Value;
        internal string Tag;

        #endregion

        public List<XMLNode> GetChildren()
        {
            return Children;
        }
        public string GetValue()
        {
            return Value;
        }
        public string GetTag()
        {
            return Tag;
        }
    }
}