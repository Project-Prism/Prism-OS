//using System.Threading;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.5", Codename = "glass";
        public static bool Running = true, canvasRunning = true;

        ///<summary>Dont call this function unless absolutly needed</summary>
        protected override void Run()
        {
            //uncomment when threading is a thing
            //new Thread(Networking.DHCP).Start();
            //new Thread(Filesystem.Init).Start();
            //new Thread(Cmds.Init).Start();
            //new Thread(Memory.memcheck).Start();
            Driver.Init();
            Desktop.Start();
        }
    }
}