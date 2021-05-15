using System;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();
        }
        
        protected override void Run()
        {
            //display logo at boot
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/   
");
            Utils.PlayJingle();
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("Powered by the Cosmos Kernel");
            Cmds.Init();
            Utils.SetColor(ConsoleColor.Gray);
            Utils.Sleep(2); // instead of sleeping we could also initialize more stuff here when we need to
            Console.Clear();
            Console.WriteLine("Prism OS (c) 2021, release 1.2.");
            Console.WriteLine("For a list of commands, type \"help\"");
            Console.WriteLine();
            //get user input
            while (true)
            {
                Console.Write("> ");
                string cmd = Console.ReadLine();
                Cmds.Parse(cmd);               
            }
        }
    }
}
