using System;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Prism OS! type 'help' for help with commands.");
            Core.Shell Shell = new();
            Shell.SendCommand("svfs");

            while (true)
            {
                Console.Write("> ");
                Shell.SendCommand(Console.ReadLine());
            }
        }
    }
}