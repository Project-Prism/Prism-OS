namespace PrismOS.Libraries.Runtime
{
    public class Memory
    {
        public static int Used => (int)Cosmos.Core.GCImplementation.GetUsedRAM();
        public static int Available => (int)Cosmos.Core.GCImplementation.GetAvailableRAM();
        public static int Total => Used + Available;
    }
}