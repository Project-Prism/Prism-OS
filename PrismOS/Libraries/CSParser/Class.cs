using System.Collections.Generic;

namespace PrismOS.Libraries.CSParser
{
    public class Class
    {
        public List<Class> Classes { get; set; } = new();
        public List<Variable> Variables { get; set; } = new();
    }
}