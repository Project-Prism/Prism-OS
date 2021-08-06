namespace PrismProject.System2
{
    internal class Hardware
    {
        internal class KernelInfo
        {
            public static string Kernel_build = "POSK Revision 2.5", Codename = "Plug";
        }

        internal class Memory
        {
            public static uint Total { get => Cosmos.Core.CPU.GetAmountOfRAM(); }
            public static uint Used { get => (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576; }
            public static uint Free { get => Total - Used; }
            public static uint Used_percent { get => (Used * 100) / Total; }
            public static uint Free_percent { get => 100 - Used_percent; }
        }

        internal class CPU
        {
            public static long CPU_Speed { get => Cosmos.Core.CPU.GetCPUCycleSpeed(); }

            internal class Power
            {
                public static void Shutdown()
                {
                    Cosmos.System.Power.Shutdown();
                }

                public static void Reboot()
                {
                    Cosmos.System.Power.Reboot();
                }
            }
        }
    }
}