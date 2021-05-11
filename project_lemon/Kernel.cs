using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace project_lemon
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Loaded project lemon. developer test 1");
        }

        protected override void Run()
        {
            Console.Write("setup: ");
            var input = Console.Read();
            {

            }
        }
    }
}
