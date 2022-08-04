using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.CSS
{
    public class CSSProperty : IDisposable
    {
        public CSSProperty(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public void Dispose()
        {
            GCImplementation.Free(Name);
            GCImplementation.Free(Value);
            GC.SuppressFinalize(this);
        }
    }
}
