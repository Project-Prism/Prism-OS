using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using System.Text;
using static System.Console;
using static System.Text.Encoding;

namespace PrismProject.Functions.Network
{
    class DebugServer
    {
        //public static Cosmos.System.Network.IPv4.TCP.TcpListener xServer = new Cosmos.System.Network.IPv4.TCP.TcpListener(4333);
        public static TcpClient outc;
        public static void TCPServer()
        {
            //using (xServer)
            //{
            //    xServer.Start();
            //    outc = xServer.AcceptTcpClient();
            //    outc.Send(Encoding.ASCII.GetBytes("Started debugging\n"));
            //}
        }
        public static void Send(string message)
        {
            outc.Send(ASCII.GetBytes(message + "\n"));
        }
    }
}
