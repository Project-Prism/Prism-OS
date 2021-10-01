using System;
using static PrismProject.Functions.Loader;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                InitCore();
            }
            catch (Exception EX)
            {
                Crash(EX.Message);
            }
        }
    }
}