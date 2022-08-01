using System;

namespace PrismOS.Libraries.Text.INI
{
    public struct Entry
    {
        public Entry(Type Type, string Name, object Value)
        {
            this.Type = Type;
            this.Name = Name;
            this.Value = Value;
        }

        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}