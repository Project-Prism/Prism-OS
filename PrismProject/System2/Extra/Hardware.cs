namespace PrismProject.System2.Extra
{
    internal class Hardware
    {
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
            /// <summary>
            /// CPU speed in MHZ.
            /// </summary>
            public static long CPU_Speed { get => Cosmos.Core.CPU.GetCPUCycleSpeed(); }

            /// <summary>
            /// System power controls.
            /// </summary>
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