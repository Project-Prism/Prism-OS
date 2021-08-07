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
using System.Text.RegularExpressions;

namespace PrismProject.System2
{
    internal class Network
    {
        /// <summary>
        /// Get local address for current machine.
        /// </summary>
        /// <returns>IP adress (as string)</returns>
        public static string GetLoaclAddress()
        {
            return NetworkConfig.CurrentConfig.Value.IPAddress.ToString();
        }

        public static dynamic GetTCP(string serverIP)
        {
            if (IPAddress.TryParse(serverIP, out IPAddress address))
                using (var xClient = new TcpClient(80))
                {
                    byte IP0 = Convert.ToByte(Regex.Split(serverIP, ".")[0]);
                    byte IP1 = Convert.ToByte(Regex.Split(serverIP, ".")[1]);
                    byte IP2 = Convert.ToByte(Regex.Split(serverIP, ".")[2]);
                    byte IP3 = Convert.ToByte(Regex.Split(serverIP, ".")[3]);

                    xClient.Connect(new Address(IP0, IP1, IP2, IP3), 80);
                    
                    var endpoint = new EndPoint(Address.Zero, 0);
                    var data = xClient.Receive(ref endpoint);  //set endpoint to remote machine IP:port
                    var data2 = xClient.NonBlockingReceive(ref endpoint); //retrieve receive buffer without waiting
                    return data2;
                }
            return null;
        }

        /// <summary>
        /// Obtain a useable IP adress from a router, if connected.
        /// </summary>
        public static void DHCP()
        {
            using (var xClient = new DHCPClient())
            {
                /** Send a DHCP Discover packet **/
                xClient.SendDiscoverPacket();
            }
        }

        /// <summary>
        /// Basic TPC server. not working yet.
        /// </summary>
        /// 

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

        /// <summary>
        /// Basic UDP client. not working yet.
        /// </summary>
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

        /// <summary>
        /// Ping a specified IP address or hostname.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
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

        /// <summary>
        /// resolve a domain name if it is not an IP.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
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