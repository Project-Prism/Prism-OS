using System;

namespace PrismOS.Libraries.Text.CSharp
{
    public class Variable
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }

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
    }
}