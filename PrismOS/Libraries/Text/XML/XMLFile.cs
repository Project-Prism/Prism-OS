using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.XML
{
    public class XMLFile
    {
        // TO-DO: Re-Do Lexer
        public List<XMLTag> Tags = new();

        public static List<XMLTag> GetTags(string HTML)
        {
            bool InTag = false, InValue = false, InCSS = false, InComment = false;
            string TB = "", VB = "", CB = "", CMB = "";
            List<XMLTag> Tags = new();

            for (int I = 0; I < HTML.Length; I++)
            {
                if (InCSS)
                {
                    if (HTML[I] == '>')
                    {
                        InCSS = false;
                        InValue = true;
                        continue;
                    }
                    CB += HTML[I];
                    continue;
                }
                if (InValue)
                {
                    if (HTML[I] == '<')
                    {
                        InValue = false;
                        InTag = false;
                        InCSS = false;
                        I += TB.Length + 2;
                        Tags.Add(new() { Name = TB, Value = VB, Attributes = XMLAttribute.Parse(CB), });
                        TB = "";
                        VB = "";
                        CB = "";
                        continue;
                    }
                    VB += HTML[I];
                    continue;
                }
                if (InTag)
                {
                    if (HTML[I] == ' ')
                    {
                        InCSS = true;
                        continue;
                    }
                    if (HTML[I] == '>')
                    {
                        InTag = false;
                        InValue = true;
                        continue;
                    }
                    TB += HTML[I];
                }
                if (InComment)
                {
                    if (HTML[I] == '-' && HTML[I + 2] == '>')
                    {
                        Tags.Add(new() { Name = "Comment", Value = CMB, Attributes = XMLAttribute.Parse(CB), });
                        InComment = false;
                        continue;
                    }
                    CMB += HTML[I];
                    continue;
                }

                if (HTML[I] == '<' && HTML[I + 1] == '!')
                {
                    I += 3;
                    InComment = true;
                    continue;
                }
                if (HTML[I] == '<')
                {
                    InTag = true;
                    continue;
                }
            }

            return Tags;
        }

        public void Dispose()
        {
            GCImplementation.Free(Tags);
            GC.SuppressFinalize(this);
        }
    }
}