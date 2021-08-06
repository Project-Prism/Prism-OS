using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Net;
using System.Text;
using EndPoint = Cosmos.System.Network.IPv4.EndPoint;

namespace PrismProject.System2
{
    internal class Net
    {
        public static bool IsIPAddress(string ipAddress)
        {
            try
            {
                return IPAddress.TryParse(ipAddress, out IPAddress address);
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        public static byte[] GetTcp(string address, int port, string body)
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
            if (!IsIPAddress(address))
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