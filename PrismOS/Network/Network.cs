using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace PrismOS.Network
{
    public static class Network
    {
        public static void InitNet()
        {
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), Local, Subnet, GateWay);
            DHCPClient xClient = new();
            xClient.SendDiscoverPacket();
            xClient.Dispose();
        }

        public static Address GateWay { get; } = new(10, 0, 0, 1);
        public static Address Local { get; } = new(127, 0, 0, 1);
        public static Address Subnet { get; } = new(255, 255, 255, 0);
        public static Address DNS { get; } = new(1, 1, 1, 1);
    }
}
