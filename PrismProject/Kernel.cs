using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.3", Codename = "Box of crayons";
        public static bool Running = true, canvasRunning = true;

        protected override void Run()
        {
            Desktop.Start();
            Cmds.Init();
            Networking.DHCP();
            Filesystem.Init();
            Filesystem.Set_Drive_letter(Filesystem.D0, "X");
        }
    }
}