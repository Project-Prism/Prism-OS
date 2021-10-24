using Cosmos.Core;

namespace Prism.Services.Basic
{
    public class Memory
    {
        public static int GetUsedMemory()
        {
            int UsedRAM = (int)CPU.GetEndOfKernel() + 1024;
            return UsedRAM / div;
        }
        public static int TotalMemory = (int)CPU.GetAmountOfRAM();
        public int FreePercentage;
        public int UsedPercentage = ((int)GetUsedMemory() * 100) / TotalMemory;
        public int FreeMemory = TotalMemory - GetUsedMemory();
        private const int div = 1048576;

        public static void GetTotalMemory()
        {
            TotalMemory = (int)CPU.GetAmountOfRAM() + 1;
        }
        public void Monitor()
        {
            GetTotalMemory();
            FreeMemory = TotalMemory - GetUsedMemory();
            UsedPercentage = (GetUsedMemory() * 100) / TotalMemory;
            FreePercentage = 100 - UsedPercentage;
        }
        public Memory()
        {
            this.Monitor();
        }
        public static int GetFreeMemory()
        {
            return TotalMemory - GetUsedMemory();
        }
    }
}
