using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        #region system info
        public static string versionID = "2.0 (gui beta)";
        public static Random rnum = new Random();
        public static string random = Convert.ToString(rnum.Next(0, 10));
        public static string root = "0:/";
        public static CosmosVFS fs = new CosmosVFS();
        #endregion

        public static bool Running = true;
        public static bool enabled;

        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(fs);
        }
        protected override void Run()
        {
            Console.Clear();
            Cmds.Init();
            Networking.dhcp();
            Gui.enable();
        }
    }
}