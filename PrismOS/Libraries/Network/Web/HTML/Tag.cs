using System.Collections.Generic;

namespace PrismOS.Libraries.Network.Web.HTML
{
    public class Tag
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public List<Attribute> Attributes { get; set; } = new();
    }
}