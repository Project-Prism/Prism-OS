using System;

namespace PrismProject
{
    class Memory
    {
        public static int Total = Convert.ToInt32(Cosmos.Core.CPU.GetAmountOfRAM());
        public static int Used = Convert.ToInt32(Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576;
        public static int Free = Used * 100 / Total;
    }
}
