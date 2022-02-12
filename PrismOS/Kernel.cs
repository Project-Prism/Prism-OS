using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using Cosmos.System.Network.Config;
using Cosmos.HAL;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Console.Clear();
            Core.Shell Shell = new();

            while (true)
            {
                Console.Write("> ");
                Shell.SendCommand(Console.ReadLine());
            }
        }
    }
}