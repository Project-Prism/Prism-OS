using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Resource.Text.INI
{
    public struct INIDocument : IDisposable
    {
        public INIDocument(string Source)
        {
            ININode T = new();
            Nodes = new();

            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '\n' || Source[I] == '\r')
                {
                    T.IsNewLine = true;
                    Nodes.Add(T);
                    T = new();
                    continue;
                }
                if (Source[I] == ' ')
                {
                    T.IsSpace = true;
                    Nodes.Add(T);
                    T = new();
                    continue;
                }
                if (Source[I] == '#')
                {
                    T.IsComment = true;
                    while (Source[I] != '\n' && Source[I] != '\r')
                    {
                        T.Name += Source[I++];
                    }

                    Nodes.Add(T);
                    T = new();
                    continue;
                }
                if (Source[I] == '[')
                {
                    T.IsSection = true;
                    while (Source[I] != ']')
                    {
                        T.Name += Source[I++];
                    }

                    Nodes.Add(T);
                    T = new();
                    continue;
                }
                if (Source[I] == '=')
                {
                    string TMP = "";
                    while (Source[I++] != '\n' && Source[I] != '\r')
                    {
                        TMP += Source[I];
                    }

                    T.Value = TMP;
                    GCImplementation.Free(TMP);
                    Nodes.Add(T);
                    T = new();
                    continue;
                }

                T.Name += Source[I];
            }
        }

        #region Definitions

        internal List<ININode> Nodes;

        #endregion

        public override string ToString()
        {
            string S = "";
            foreach (ININode Node in Nodes)
            {
                S += Node.ToString();
            }
            return S;
        }

        public List<ININode> GetNodes()
        {
            List<ININode> TNodes = new();
            for (int I = 0; I < Nodes.Count; I++)
            {
                if (Nodes[I].GetIsComment() || Nodes[I].GetIsNewLine() || Nodes[I].GetIsSection() || Nodes[I].GetIsSpace())
                {
                    continue;
                }

                TNodes.Add(Nodes[I]);
            }
            return TNodes;
        }

        public void Dispose()
        {
            GCImplementation.Free(Nodes);
            GC.SuppressFinalize(this);
        }
    }
}