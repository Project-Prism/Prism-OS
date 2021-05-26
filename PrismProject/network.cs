using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using System.Text;

namespace PrismProject
{
    public class Networking
    {
        public static void dhcp()
        {
            using (var client = new DHCPClient())
                client.SendDiscoverPacket();
        }

        public static int ping(string address, int timeout = 5000)
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
    }
}
