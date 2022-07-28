using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Class
    {
        public Class()
        {
            Name = "";
            IsPublic = false;
            IsStatic = false;
            Contents = "";
            Variables = new();
            Functions = new();
        }

        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsStatic { get; set; }
        public string Contents { get; set; }
        public List<Class> Classes { get; set; }
        public List<Variable> Variables { get; set; }
        public List<Function> Functions { get; set; }
    }
}