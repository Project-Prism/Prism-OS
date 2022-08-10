using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.Generic
{
    public class Property : List<Property>, IDisposable
    {
        public Property(string Name, string Value, List<Property> Properties)
        {
            for (int I = 0; I < Properties.Count; I++)
            {
                Add(Properties[I]);
            }
            this.Value = Value;
            this.Name = Name;
        }
        public Property(string Name, string Value)
        {
            this.Value = Value;
            this.Name = Name;
        }
        public Property()
        {
            Value = "";
            Name = "";
        }

        internal string Value;
        internal string Name;

        public void SetValue(string Value)
        {
            this.Value = Value;
        }
        public void SetName(string Value)
        {
            Name = Value;
        }

        public string GetValue()
        {
            return Value;
        }
        public string GetName()
        {
            return Name;
        }

        public void Dispose()
        {
            GCImplementation.Free(Name);
            GCImplementation.Free(Value);
            GC.SuppressFinalize(this);
        }
    }
}