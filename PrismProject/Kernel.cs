using System;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSL Revision 2";

        public static bool Running = true;
        public static bool enabled;
        protected override void Run()
        {
            Cmds.Init();
            Networking.DHCP();
            Console.Clear();
            Graphics.Demo();
        }
    }
}