using System.Collections.Generic;

namespace PrismOS.Libraries.Network.Web.HTML
{
    public class Attribute
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";

        public static List<Attribute> Parse(string Contents)
        {
            if (Contents.Length == 0)
            {
                return new();
            }

            List<Attribute> Attributes = new();
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