using Cosmos.System.Network;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using Cosmos.System.Network.IPv4;
using System;
using System.Net;
using System.Text;
using EndPoint = Cosmos.System.Network.IPv4.EndPoint;
using Cosmos.System.Network.IPv4.UDP;
using Cosmos.System.Network.Config;

namespace PrismProject.System2
{
    internal class Network
    {
        /// <summary>
        /// Get local address for current machine.
        /// </summary>
        /// <returns>IP adress (as string)</returns>
        public static string GetLocalAdress()
        {
            return NetworkConfig.CurrentConfig.Value.IPAddress.ToString();
        }

        /// <summary>
        /// Check if a string is a valid IP adress
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>True or false</returns>
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
            using (var xClient = new DHCPClient())
            {
                /** Send a DHCP Discover packet **/
                xClient.SendDiscoverPacket();
            }
        }

        public static void TCPServer()
        {
            using (var xServer = new TcpListener(4242))
            {
                var message = "1, success!";
                /** Start server **/
                xServer.Start();

                /** Accept incoming TCP connection **/
                var client = xServer.AcceptTcpClient(); //blocking

                /** Stop server **/
                xServer.Stop();

                /** Send data **/
                client.Send(Encoding.ASCII.GetBytes(message));
            }
        }

        public static void TCPClient()
        {
            using (var xClient = new TcpClient(4242))
            {
                var message = "1, success!, client sent";
                xClient.Connect(new Address(192, 168, 1, 70), 4242);

                /** Send data **/
                xClient.Send(Encoding.ASCII.GetBytes(message));

                /** Receive data **/
                var endpoint = new EndPoint(Address.Zero, 0);
                var data = xClient.Receive(ref endpoint);  //set endpoint to remote machine IP:port
                var data2 = xClient.NonBlockingReceive(ref endpoint); //retrieve receive buffer without waiting
            }
        }

        public static void UDP()
        {
            using (var xClient = new UdpClient(4242))
            {
                xClient.Connect(new Address(192, 168, 1, 70), 4242);
                var message = "1, success!, udp sent";
                /** Send data **/
                xClient.Send(Encoding.ASCII.GetBytes(message));

                /** Receive data **/
                var endpoint = new EndPoint(Address.Zero, 0);
                var data = xClient.Receive(ref endpoint);  //set endpoint to remote machine IP:port
                var data2 = xClient.NonBlockingReceive(ref endpoint); //retrieve receive buffer without waiting
            }
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
                    xClient.Connect(new Address(1, 1, 1, 1));
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