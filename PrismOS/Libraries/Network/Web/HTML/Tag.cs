using System.Collections.Generic;

namespace PrismOS.Libraries.Network.Web.HTML
{
    public class Tag
    {
        public Tag(string Name, string Contents, List<Attribute> Attributes)
        {
            this.Name = Name;
            this.Contents = Contents;
            this.Attributes = Attributes;
        }

        public static List<string> ValidTags = new()
        {
            "p",
            "h1", "h2", "h3", "h4", "h6",
            "button",
            "br", "hr",
            "li", "ul",
            "div",
            "title",
        };

        public static bool IsTagValid(Tag Tag)
        {
            foreach (string S in ValidTags)
            {
                if (S == Tag.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public string Name = "", Contents = "";
        public List<Attribute> Attributes = new();
    }
}