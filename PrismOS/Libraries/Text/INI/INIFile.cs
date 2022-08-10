using PrismOS.Libraries.Text.Generic;
using Cosmos.Core;

namespace PrismOS.Libraries.Text.INI
{
    public class INIFile : Lexer
    {
        public INIFile(string S)
        {
            Property Selected = new();
            string Builder = "";

            for (int I = 0; I < S.Length; I++)
            {
                if (S[I] == ' ' || S[I] == '\n' || S[I] == '\r')
                {
                    continue;
                }
                if (S[I] == '[')
                {
                    Selected = new();
                    while (S[++I] != ']')
                    {
                        Builder += S[I];
                    }
                    Add(new(Builder, ""));
                    Selected.Name = Builder;
                    Builder = "";
                    continue;
                }
                if (S[I] == '=')
                {
                    string Name = Builder;
                    string Value = "";
                    Builder = "";
                    while (S[I] != '\n' && S[I] != '\r' && S[I] != '\0' && I < S.Length - 1)
                    {
                        Value += S[++I];
                    }
                    if (Selected.Name.Length != 0)
                    {
                        this[IndexOf(Selected)].Add(new(Name, Value));
                    }
                    continue;
                }
                if (S[I] == '#')
                {
                    while (S[++I] != '\n')
                    {
                    }
                    continue;
                }

                Builder += S[I];
            }

            GCImplementation.Free(Selected);
            GCImplementation.Free(Builder);
        }
    }
}