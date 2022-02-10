using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;
using PrismOS.Generic;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Prism OS! type 'help' for help with commands.\n(Note: it is recomended to run 'svfs' and 'snet' when you are in the terminal.)");
            Core.Shell Shell = new();

            while (true)
            {
                Console.Write("> ");
                Shell.SendCommand(Console.ReadLine());
            }
        }
    }
}