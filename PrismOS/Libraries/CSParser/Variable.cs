using System;

namespace PrismOS.Libraries.CSParser
{
    internal class Variable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }

        public bool IsPublic { get; set; }
        public bool IsStatic { get; set; }
        public bool IsConst { get; set; }
        public bool IsNull { get; set; }

        public static Variable Parse(string Contents)
        {
            bool ReadingQuote = false;
            string Builder = "";
            Variable V = new();

            for (int I = 0; I < Contents.Length; I++)
            {
                if (Contents[I] == ' ' && !ReadingQuote)
                {
                    if (Builder == "public")
                    {
                        V.IsPublic = true;
                        continue;
                    }
                    if (Builder == "static")
                    {
                        if (V.IsConst)
                        {
                            throw new("Variable cannot be both static and const.");
                        }
                        V.IsStatic = true;
                        continue;
                    }
                    if (Builder == "const")
                    {
                        if (V.IsStatic)
                        {
                            throw new("Variable cannot be both const and static.");
                        }
                        V.IsConst = true;
                        continue;
                    }
                    continue;
                }
                if (Contents[I] == '"' && !ReadingQuote)
                {
                    V.Type = typeof(string);
                    ReadingQuote = true;
                    continue;
                }
                if (Contents[I] == '"' && ReadingQuote)
                {
                    V.Value = Builder;
                    ReadingQuote = false;
                }
                if (Contents[I] == ';')
                {
                    return V;
                }
                if (Contents[I] == '=')
                {
                    V.Name = Builder;
                    continue;
                }

                // Default If Not Running Specified Function
                Builder += Contents[I];
            }

            throw new("Unexpected EOL (Missing ';')");
        }
    }
}