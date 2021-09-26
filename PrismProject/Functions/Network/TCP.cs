using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using System.Text;

namespace PrismProject.Functions.Network
{
    class TCP
    {
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
