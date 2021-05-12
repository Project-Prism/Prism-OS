using System;
using Sys = Cosmos.System;

namespace LemonProject
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            var hour = Cosmos.HAL.RTC.Hour;
            var minute = Cosmos.HAL.RTC.Minute;
            var second = Cosmos.HAL.RTC.Second;
            Console.Clear();
        }
        
        protected override void Run()
        {
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
  _                                   ____   _____ 
 | |                                 / __ \ / ____|
 | |     ___ _ __ ___   ___  _ __   | |  | | (___  
 | |    / _ \ '_ ` _ \ / _ \| '_ \  | |  | |\___ \ 
 | |___|  __/ | | | | | (_) | | | | | |__| |____) |
 |______\___|_| |_| |_|\___/|_| |_|  \____/|_____/ 
");
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("Powered by the Cosmos Kernel");
            Utils.SetColor(ConsoleColor.Gray);
            Cmds.Init();
            Utils.Sleep(5); // instead of sleeping we could also initialize more stuff here when we need to
            Console.Clear();
            Console.WriteLine("Lemon OS (c) 2021, release 1.2.");
            Console.WriteLine("For a list of commands, type \"help\"");
            Console.WriteLine();
            while (true)
            {
                Console.Write("> ");
                string cmd = Console.ReadLine();
                Cmds.Parse(cmd);
            }
        }
    }
}
