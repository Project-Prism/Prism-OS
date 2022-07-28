using System;
using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Function
    {
        public Function()
        {
            Calls = new();
            ReturnValue = new();
            Locals = new();
        }

        public List<Call> Calls { get; set; }
        public Attributes ReturnValue { get; set; }
        public List<Attributes> Locals { get; set; }
    }
}