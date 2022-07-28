using System;
using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Function
    {
        public Function()
        {
            Calls = new();
            HasArguments = false;
            IsConstructor = false;
            ReturnValue = new();
            Locals = new();
        }

        public List<Call> Calls { get; set; }
        public bool HasArguments { get; set; }
        public bool IsConstructor { get; set; }
        public Variable ReturnValue { get; set; }
        public List<Variable> Locals { get; set; }
    }
}