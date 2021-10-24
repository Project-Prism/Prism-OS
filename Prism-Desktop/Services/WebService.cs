using Cosmos.System.Network.IPv4.TCP;
using System;
using System.Text;

namespace Prism.Services
{
    class WebService
    {
        public static void StartServer()
        {
            using TcpListener xServer = new(80);
            xServer.Start();
            while(true)
            {
                var x = xServer.AcceptTcpClient();
                var a = x.RemoteEndPoint;
                Console.WriteLine(Encoding.ASCII.GetString(x.Receive(ref a)));
                x.Close();
            }
        }
    }
}
