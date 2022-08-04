using System.Collections.Generic;

namespace PrismOS.Libraries.Text.XML
{
    public class XMLTag
    {
        public XMLTag()
        {
            Name = "";
            Value = "";
            Attributes = new();
        }
        public XMLTag(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
            Attributes = new();
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public List<XMLAttribute> Attributes { get; set; }
    }
}