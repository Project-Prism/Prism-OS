using System.Collections.Generic;

namespace PrismOS.Libraries.Text.INI
{
    public class INIFile : Dictionary<string, List<Entry>>
    {
        public INIFile(string S)
        {
            string Selected = "";
            string Builder = "";

            for (int I = 0; I < S.Length; I++)
            {
                if (S[I] == ' ' || S[I] == '\n' || S[I] == '\r')
                {
                    continue;
                }
                if (S[I] == '[')
                {
                    while (S[++I] != ']')
                    {
                        Builder += S[I];
                    }
                    Add(Builder, new());
                    Selected = Builder;
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
                    if (Selected.Length != 0)
                    {
                        if (int.TryParse(Value, out var N))
                        {
                            this[Selected].Add(new(typeof(int), Name, N));
                        }
                        else
                        {
                            this[Selected].Add(new(typeof(string), Name, Value));
                        }
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
        }
    }
}