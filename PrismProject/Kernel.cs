//using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.5", Codename = "glass";
        public static bool Running = true, canvasRunning = true;

        protected override void Run()
        {
            // uncomment when threading is a thing
            //Task Boottask = Task.Run(() =>
            //{
            //    Networking.DHCP();
            //    Filesystem.Init();
            //    Filesystem.Set_Drive_letter(Filesystem.D0, "X");
            //    Cmds.Init();
            //});
            Driver.Init();
            Desktop.Start();
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