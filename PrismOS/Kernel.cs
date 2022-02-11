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
            CosmosVFS VFS = new();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), Address.Zero, Address.Broadcast, Address.Parse("192.168.1.1"));
            new DHCPClient().SendDiscoverPacket();

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