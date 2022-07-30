using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Runtime
{
    public abstract class Application : IDisposable
    {
        // Application Manager Variable
        public static List<Application> Applications { get; set; } = new();

        public Application()
        {
            OnCreate();
            Applications.Add(this);
        }
        ~Application()
        {
            OnDestroy();
            Applications.Remove(this);
        }

        public abstract void OnKey(Cosmos.System.KeyEvent Key);
        public abstract void OnUpdate();
        public abstract void OnDestroy();
        public abstract void OnCreate();


        public void Dispose()
        {
            Cosmos.Core.GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}