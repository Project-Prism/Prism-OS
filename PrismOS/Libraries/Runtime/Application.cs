namespace PrismOS.Libraries.Runtime
{
    public abstract class Application
    {
        public Application()
        {
            OnCreate();
            Kernel.Applications.Add(this);
        }
        ~Application()
        {
            OnDestroy();
            Kernel.Applications.Remove(this);
        }

        public abstract void OnUpdate();
        public abstract void OnDestroy();
        public abstract void OnCreate();
    }
}