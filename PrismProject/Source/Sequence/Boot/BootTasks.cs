using static PrismProject.Source.Network.Interface;
using static PrismProject.Source.Assets.AssetList;
using PrismProject.Source.FileSystem;
using Cosmos.System;

namespace PrismProject.Source.Sequence.Boot
{
    internal class BootTasks
    {
        public static void Main()
        {
            PCSpeaker.Beep();
            Disk.Start();
            NetStart(Local, Subnet, Gateway2);
            InitStatic();
        }
    }
}
