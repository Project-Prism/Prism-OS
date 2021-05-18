using System;
using Sys = Cosmos.System;
using Cosmos.System.Network;
using Cosmos.System.Network.IPv4;
using Cosmos.HAL;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {

        }
        
        protected override void Run()
        {
            #region startup
            Console.Write("Starting command parser...");
            Cmds.Init();
            Utils.success("[ok]");
            Console.Clear();
            #endregion

            #region terminal
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/   
");
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("Powered by the Cosmos Kernel");
            Utils.Playjingle();
            Cmds.Init();
            Utils.SetColor(ConsoleColor.Gray);
            Utils.Sleep(2); // instead of sleeping we could also initialize more stuff here when we need to
            Console.Clear();
            Console.WriteLine("Prism OS (c) 2021, CORE version 1.5.");
            Console.WriteLine("For a list of commands, type \"help\"");
            Console.WriteLine();
            //get user input
            while (true)
            {
                Console.Write(Cmds.pfdir + "> ");
                string cmd = Console.ReadLine();
                Cmds.Parse(cmd);               
            }
            #endregion
        }
    }
}
