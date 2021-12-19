using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;
using System.IO;

namespace PrismOS.Network.NWeb
{
    public class NWebServer
    {
        public NWebServer(string Path)
        {
            xServer = new(1080);
            xEndPoint = new(Address.Zero, 1080);
            xPath = Path;
            xServer.Start();
        }

        public TcpListener xServer;
        public EndPoint xEndPoint;
        public string xPath;

        public void Run()
        {
            while(true)
            {
                TcpClient TcpClient = xServer.AcceptTcpClient();

                string[] Recieved = Encoding.ASCII.GetString(TcpClient.Receive(ref xEndPoint)).Split(": ");

                switch (Recieved[0])
                {
                    case "GET":
                        TcpClient.Send(File.ReadAllBytes(xPath + Recieved[1]));
                        break;
                    case "POST":
                        File.WriteAllBytes(xPath + Recieved[1], Encoding.ASCII.GetBytes(Recieved[2]));
                        break;
                }
            }
        }
    }
}
