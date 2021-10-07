using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using static System.Text.Encoding;
using static System.Console;

namespace PrismProject.Functions.Network
{
    internal class Basic
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
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), LocalIP, SubNet, GateWay);
            using (DHCPClient xClient = new DHCPClient())
            {
                xClient.SendDiscoverPacket();
            }
        }

        public static void TCPClient(Address IP, int Port)
        {
            try
            {
                using (TcpClient xClient = new TcpClient(Port))
                {
                    xClient.Connect(IP, Port);
                    while (true)
                    {
                        EndPoint endpoint = new EndPoint(IP, (ushort)Port);
                        Write(ASCII.GetString(xClient.Receive(ref endpoint)));
                        xClient.Send(ASCII.GetBytes(ReadLine() + "\n"));
                    }
                }
            }
            catch { return; }
        }

        public static void Download(string IP, int Port, string SaveTo)
        {
            using (TcpClient xClient = new TcpClient(Port))
            {
                var IP2 = DNS(Gateway1, IP);
                xClient.Connect(IP2, Port);
                while (true)
                {
                    EndPoint endpoint = new EndPoint(IP2, (ushort)Port);
                    IO.Disk.WriteFile(SaveTo, ASCII.GetString(xClient.Receive(ref endpoint)), false);
                    xClient.Dispose();
                }
            }
        }
    }
}