using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace PrismProject.Network
{
    internal class NetworkCore
    {
        /// <summary>Type 1 for 192.168.x.x type routers</summary>
        public static Address Gateway1 = new Address(192, 168, 1, 1);

        /// <summary>Type 2 for 10.0.0.x type routers</summary>
        public static Address Gateway2 = new Address(10, 0, 0, 1);

        public static Address Local = new Address(127, 0, 0, 1);
        public static Address Subnet = new Address(255, 255, 255, 0);

        public static Address DNS(Address GateWay, string Domain)
        {
            using (DnsClient xClient = new DnsClient())
            {
                xClient.Connect(GateWay);
                xClient.SendAsk(Domain);
                Address ret = xClient.Receive();
                xClient.Dispose();
                return ret;
            }
        }

        public static void NetStart(Address LocalIP, Address SubNet, Address GateWay)
        {
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), LocalIP, SubNet, GateWay);
            using (DHCPClient xClient = new DHCPClient())
            {
                xClient.SendDiscoverPacket();
                xClient.Dispose();
            }
        }
    }
}