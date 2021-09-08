using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Text;

namespace PrismProject.Source.Network
{
    class Network
    {
        public static void NetStart(Address LocalIP, Address SubNet, Address GateWay)
        {
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), LocalIP, SubNet, GateWay);
            using (var xClient = new DHCPClient()) { xClient.SendDiscoverPacket(); }

        }
        public static void TCPClient(byte[] IP, int Port, string Contents)
        {
            NetStart(new Address(10,0,0,50), new Address(255,255,255,0), new Address(10,0,0,1));
            using (var xClient = new TcpClient(Port))
            {
                xClient.Connect(new Address(IP[0], IP[1], IP[2], IP [3]), Port);
                xClient.Send(Encoding.ASCII.GetBytes(Contents));
                var endpoint = new EndPoint(Address.Zero, 0);
                var data = xClient.Receive(ref endpoint);
                var data2 = xClient.NonBlockingReceive(ref endpoint);
            }
        }
        public static Address DNS(Address GateWay, string Domain)
        {
            using (var xClient = new DnsClient())
            {
                xClient.Connect(GateWay);
                xClient.SendAsk(Domain);
                return xClient.Receive();
            }
        }
    }
}
