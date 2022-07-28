using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Class
    {
        public Class()
        {
            Classes = new();
            Attributes = new();
            Functions = new();
            Variables = new();
        }

        public List<Class> Classes { get; set; }
        public Attributes Attributes { get; set; }
        public List<Function> Functions { get; set; }
        public List<Attributes> Variables { get; set; }
    }
}