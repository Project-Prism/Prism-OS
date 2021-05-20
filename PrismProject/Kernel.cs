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
        public static Random rnum = new System.Random();
        public static string random = Convert.ToString(rnum.Next(0, 10));
        public static string root = "0:/";
        public static CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();
        #endregion

        protected override void BeforeRun()
        {
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
        }
        protected override void Run()
        {
            Console.Clear();
            Cmds.Init();
            tools.Sleep(2);
            bool optionForm = false;
            int seconds = 1;

            while (true)
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
                while (true)
                {
                    Console.Write(root + "> ");
                    string cmd = Console.ReadLine();
                    Cmds.Parse(cmd);
                }
            }
            else
            {
                Console.Clear();
                gui.start();
                gui.draw_taskbar();
                gui.draw_menubtn();
                while (true)
                {
                    tools.Sleep(10);
                }
            }
        }
    }
}
