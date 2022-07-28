using System;
using System.Collections.Generic;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Attributes
    {
        public Attributes()
        {
            Type = typeof(void);
            Name = "";
            Value = null;
            Arguments = new();
        }

        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        
        public List<Attributes> Arguments { get; set; }
        public bool IsConstructor { get; set; }
        public bool HasArguments { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsPointer { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLambda { get; set; }
        public bool IsStatic { get; set; }
        public bool IsUnsafe { get; set; }
        public bool IsMethod { get; set; }
        public bool IsExtern { get; set; }
        public bool IsConst { get; set; }
        public bool IsNull { get; set; }
        public bool IsCall { get; set; }
    }
}