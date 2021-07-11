using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System.Text;

namespace PrismProject
{
    public class Networking
    {
        public static void DHCP()
        {
            using (var client = new DHCPClient())
                client.SendDiscoverPacket();
        }
        public static int Ping(string address, int timeout = 5000)
        {
            using (var client = new ICMPClient())
            {
                var point = new EndPoint(Address.Zero, 0);

                client.Connect(Address.Parse(address));
                client.SendEcho();

                return client.Receive(ref point, timeout);
            }
        }
        public static byte[] tcp(string address, int port, int timeout, string body)
        {
            using (var client = new TcpClient(port))
            {
                client.Connect(Address.Parse(address), port);
                client.Send(Encoding.ASCII.GetBytes(body));

                var endpoint = new EndPoint(Address.Zero, 0);

                var data = client.Receive(ref endpoint);
                var data2 = client.NonBlockingReceive(ref endpoint);

                return data2;
            }
        }
        public static Address DNS(string address)
        {
            if (!Tools.IsIPAddress(address))
            {
                using (var xClient = new DnsClient())
                {
                    xClient.Connect(new Address(192, 168, 1, 254));
                    xClient.SendAsk(address);
                    return xClient.Receive(10000);
                }
            }
            else
            {
                return new Address(0, 0, 0, 0);
            }
        }
    }
}
