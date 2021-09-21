using Cosmos.System.FileSystem.Listing;
using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Source.Sequence.Boot.Main();
        }
    }
}