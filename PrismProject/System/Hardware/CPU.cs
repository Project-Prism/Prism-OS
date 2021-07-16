﻿using Cosmos.System;
using System;

namespace PrismProject
{
    class CPU
    {
        public static long Speed { get => Cosmos.Core.CPU.GetCPUCycleSpeed(); }

        static void shutdown(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-r")
                {
                    Power.Reboot();
                    return;
                }
                if (args[0] == "-t" && args[2] == "-r")
                {
                    Tools.Sleep(Convert.ToInt32(args[1]));
                    Power.Reboot();
                    return;
                }
                if (args[0] == "-t")
                {
                    Tools.Sleep(Convert.ToInt32(args[1]));
                    Power.Shutdown();
                    return;
                }

            }
        }
    }
}
