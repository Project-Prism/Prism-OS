using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace PrismOS.Network.NWeb
{
    public class NWebServer
    {
        public NWebServer(string xPath, bool xAllowPost, bool xAllowGet)
        {
            Clients = new();
            Server = new(1080);
            EndPoint = new(Address.Zero, 1080);
            Path = xPath;

            AllowPost = xAllowPost;
            AllowGet = xAllowGet;

            Server.Start();
        }

        public List<TcpClient> Clients;
        public TcpListener Server;
        public EndPoint EndPoint;
        public string Path;

        public bool AllowPost = true;
        public bool AllowGet = true;

        public void Run()
        {
            while (true)
            {
                Clients.Add(Server.AcceptTcpClient());

                foreach (TcpClient Client in Clients)
                {
                    byte[] Recieved = Client.NonBlockingReceive(ref EndPoint);
                    int Method = Recieved[1];

                    for (int I = 0; I < Recieved.Length; I++)
                    {
                    }
                }
            }
        }
    }
}