using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Resource.Text.XML
{
    public struct XMLDocument : IDisposable
    {
        public XMLDocument(string Source, int I = 1)
        {
            RootNode = new(Source, ref I);
            this.Source = Source[1..];
        }

        #region Definitions

        internal XMLNode RootNode;
        internal string Source;

        #endregion

        public XMLNode GetRootNode()
        {
            return RootNode;
        }
        public string GetSource()
        {
            return Source;
        }

        public void Dispose()
        {
            GCImplementation.Free(RootNode);
            GCImplementation.Free(Source);
            GC.SuppressFinalize(this);
        }
    }
}