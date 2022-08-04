using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.XML
{
    public class XMLTag : IDisposable
    {
        public XMLTag()
        {
            Name = "";
            Value = "";
            Attributes = new();
        }
        public XMLTag(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
            Attributes = new();
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public List<XMLAttribute> Attributes { get; set; }

        public void Dispose()
        {
            GCImplementation.Free(Name);
            GCImplementation.Free(Value);
            GCImplementation.Free(Attributes);
            GC.SuppressFinalize(this);
        }
    }
}