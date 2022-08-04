using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.INI
{
    public class INIFile : Dictionary<string, List<INIEntry>>, IDisposable
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

            GCImplementation.Free(Selected);
            GCImplementation.Free(Builder);
        }

        public void Dispose()
        {
            foreach (KeyValuePair<string, List<INIEntry>> KVP in this)
            {
                GCImplementation.Free(KVP.Value);
                foreach (INIEntry INI in KVP.Value)
                {
                    INI.Dispose();
                }
                GCImplementation.Free(KVP.Value);
                GCImplementation.Free(KVP);
            }
            GC.SuppressFinalize(this);
        }
    }
}