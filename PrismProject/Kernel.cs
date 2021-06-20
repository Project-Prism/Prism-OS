using System;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSL Revision 2.1";
        public static string Codename = "Box of crayons";
        public static bool Running = true;
        public static bool canvasRunning = new bool();

        protected override void Run()
        {
            //Interface.Start();
            Cmds.Init();
            Networking.DHCP();
            Cmds.Input();
        }
    }
}