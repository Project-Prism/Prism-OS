using System;
using static PrismProject.Functions.Loader;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            LoadSys();
        }
    }
}