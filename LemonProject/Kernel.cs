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
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine("Powered by the Cosmos Kernel");
            Utils.Sleep(2);
            Console.Clear();
            Initialize();
            
            while (true)
            {
                Console.Write(">");
                string cmd = Console.ReadLine();
                Cmds.Parse(cmd);
            }
        }

        static void Initialize()
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
            Console.WriteLine();
            Cmds.Init();
            Utils.Sleep(3); // instead of sleeping we could also initialize more stuff here when we need to
            Console.Clear();
            Utils.SetColor(ConsoleColor.Gray);
        }
    }
}
