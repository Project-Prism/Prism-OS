using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try { }
            catch (Exception e) { Console.WriteLine("[ERROR] " + e); }
        }
    }
}