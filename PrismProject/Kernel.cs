using Cosmos.System.FileSystem.Listing;
using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Prism_Core.Sequence.Boot.Boot.Main();
        }
    }
}