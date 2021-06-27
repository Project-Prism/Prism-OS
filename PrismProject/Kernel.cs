using System;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.2";
        public static string Codename = "Box of crayons";
        public static bool Running = true;
        public static bool canvasRunning = true;

        protected override void Run()
        {
            Driver.Init();
            Cmds.Init();
            Networking.DHCP();
            Desktop.Start();
        }
    }
}