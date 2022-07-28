namespace PrismOS.Libraries.Text.CSharp
{
    public class File
    {
        public File(string Contents)
        {
            Builder = "";
            NameSpace = "";
            Usings = new();
            Classes = new();

            for (int I = 0; I < Contents.Length; I++)
            {
                if (Contents[I] == '\n' || Contents[I] == '\t')
                {
                    continue;
                }
                if (Contents[I] == ' ')
                {
                    if (Builder == "namespace")
                    {
                        Builder = "";
                        while (Contents[I] != '{')
                        {
                            NameSpace += Contents[I++];
                        }
                        continue;
                    }
                    if (Builder == "using")
                    {
                        Builder = "";
                        Usings.Add("");
                        while (Contents[I] != ';')
                        {
                            Usings[Usings.Count - 1] += Contents[I++];
                        }
                        continue;
                    }

                    continue;
                }

                Builder += Contents[I];
            }
        }

        public string Builder { get; set; }
        public string NameSpace { get; set; }
        public List<string> Usings { get; set; }
        public List<Class> Classes { get; set; }
    }
}