using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.5", Codename = "glass";
        public static bool Running = true, canvasRunning = true;

        protected override void Run()
        {
            //dont change any code to try and fix, when threadding gets implemented it will work flawlesly with a small edit
            Driver.Init();
            info_screen.Start();
            Cmds.Init();
            Networking.DHCP();
            Filesystem.Init();
            Filesystem.Set_Drive_letter(Filesystem.D0, "X");
            while (Running)
            {
                if (Memory.Free < 100)
                {
                    Memory.OutOfMemoryWarning();
                    Cosmos.Core.Bootstrap.CPU.Halt();
                }
            }
        }
    }
}