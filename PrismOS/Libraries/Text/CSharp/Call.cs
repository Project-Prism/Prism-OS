using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Call
    {
        public Call()
        {
            Name = "";
            Arguments = new();
        }

        public string Name { get; set; }
        public List<Variable> Arguments { get; set; }
    }
}