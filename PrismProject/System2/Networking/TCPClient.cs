using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;

namespace PrismProject.System2.Networking
{
    class TCPClient
    {
        public static string Get(string serverIP)
        {
            Address loc = Networking.ParseURL(serverIP);
            using (var xClient = new TcpClient(80))
            {
                xClient.Connect(loc, 80);
                var endpoint = new EndPoint(loc, 80);
                var data = xClient.Receive(ref endpoint);  //set endpoint to remote machine IP:port
                var data2 = xClient.NonBlockingReceive(ref endpoint); //retrieve receive buffer without waiting
                return data2.ToString();
            }
        }
    }
}
