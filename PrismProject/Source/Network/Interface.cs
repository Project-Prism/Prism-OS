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
    internal class Interface
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
                return xClient.Receive();
            }
        }

        public static void NetStart(Address LocalIP, Address SubNet, Address GateWay)
        {
            Console.Write("Starting network service...");
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), LocalIP, SubNet, GateWay);
            using (DHCPClient xClient = new DHCPClient()) { xClient.SendDiscoverPacket(); }
            Console.WriteLine(" [done]");
        }

        public static void TCPClient(byte[] IP, int Port, string Contents)
        {
            using (TcpClient xClient = new TcpClient(Port))
            {
                xClient.Connect(new Address(IP[0], IP[1], IP[2], IP[3]), Port);
                xClient.Send(Encoding.ASCII.GetBytes(Contents));
                EndPoint endpoint = new EndPoint(Address.Zero, 0);
                byte[] data = xClient.Receive(ref endpoint);
                byte[] data2 = xClient.NonBlockingReceive(ref endpoint);
            }
        }

        public static void TCPServer(int port)
        {
            /*
            using (var xServer = new TcpListener(port))
            {
                xServer.Start();

                while (true) // one client at a time until threading is implemented.
                {
                    var OutClient = xServer.AcceptTcpClient();
                    while (true)
                    {
                        OutClient.Send(Encoding.ASCII.GetBytes("Connected to server 0."));
                    }
                }
            }
            */
        }
    }
}