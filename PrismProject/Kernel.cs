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
            Tools.Sleep(2);
            Networking.dhcp();
            bool optionForm = false;
            int seconds = 1;

            // Using a variable instead of only true helps when debugging
            while (Running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo c = Console.ReadKey(true);
                    if (c.Key == ConsoleKey.R)
                    {
                        optionForm = true;
                        break;
                    }
                }

                if (seconds++ > 2)
                    break;
            }

            if (optionForm)
            {
                Console.WriteLine("Entering recovery mode...");
                Console.WriteLine("Type \"help\" for a list of commands");
                
                while (Running)
                {
                    Console.Write(root + "> ");
                    string cmd = Console.ReadLine();
                    Cmds.Parse(cmd);
                }
            } else Gui.enable();
        }
    }
}
