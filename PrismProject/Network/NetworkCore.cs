using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;

namespace PrismProject.Network
{
    internal class NetworkCore
    {
        public static Address GateWay = new Address(10, 0, 0, 1);
        public static Address Local = new Address(127, 0, 0, 1);
        public static Address Subnet = new Address(255, 255, 255, 0);

        public static Address GetLocalAddress()
        {
            return DHCPClient.DHCPServerAddress(GetNetworkDevice("eth0"));
        }

        public static NetworkDevice GetNetworkDevice(string DeviceName)
        {
            return NetworkDevice.GetDeviceByName(DeviceName);
        }

        public static Address DNS(Address GateWay, string Domain)
        {
            try
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
            catch (Exception aException)
            {
                throw new Exception("DNS failure exception (" + aException.Message + ")");
            }
        }

        public static void NetStart(Address LocalIP, Address SubNet)
        {
            IPConfig.Enable(GetNetworkDevice("eth0"), LocalIP, SubNet, GateWay);

            using (DHCPClient xClient = new DHCPClient())
            {
                xClient.SendDiscoverPacket();
                xClient.Dispose();
            }
        }
    }
}