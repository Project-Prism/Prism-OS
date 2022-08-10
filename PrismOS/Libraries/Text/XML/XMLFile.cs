using PrismOS.Libraries.Text.Generic;
using System.Collections.Generic;

namespace PrismOS.Libraries.Text.XML
{
    // TO-DO: Re-Do Lexer, It's Not Designed Good
    public class XMLFile : Lexer
    {
        public XMLFile(string XML)
        {
            List<Property> P = GetTags(XML);
            for (int I = 0; I < P.Count; I++)
            {
                Add(P[I]);
            }
        }

        public static List<Property> GetTags(string HTML)
        {
            bool InTag = false, InValue = false, InCSS = false, InComment = false;
            string TB = "", VB = "", CB = "", CMB = "";
            List<Property> Tags = new();

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
                        Tags.Add(new(TB, VB, Parse(CB)));
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
                        Tags.Add(new("Comment", CMB, Parse(CB)));
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
        public static List<Property> Parse(string Contents)
        {
            if (Contents.Length == 0)
            {
                return new();
            }

            List<Property> Attributes = new();
            string NB = "", VB = "";
            bool InName = true;

            for (int I = 0; I < Contents.Length; I++)
            {
                if (InName)
                {
                    if (Contents[I] == ' ')
                    {
                        continue;
                    }
                    if (Contents[I] == '=')
                    {
                        I++;
                        InName = false;
                        continue;
                    }
                    NB += Contents[I];
                }
                else
                {
                    if (Contents[I] == '"')
                    {
                        Attributes.Add(new() { Name = NB, Value = VB, });
                        NB = "";
                        VB = "";
                        InName = true;
                        I++;
                        continue;
                    }
                    VB += Contents[I];
                }
            }

            return Attributes;
        }
    }
}