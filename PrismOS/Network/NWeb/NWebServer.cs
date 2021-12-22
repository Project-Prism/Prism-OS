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
                    string[] Recieved = Encoding.ASCII.GetString(Client.NonBlockingReceive(ref EndPoint)).Split(": ");

                    switch (Recieved[0])
                    {
                        case "GET":
                            if (AllowGet)
                            {
                                Client.Send(File.ReadAllBytes(Path + Recieved[1]));
                            }
                            break;
                        case "POST":
                            if (AllowPost)
                            {
                                File.WriteAllBytes(Path + Recieved[1], Encoding.ASCII.GetBytes(Recieved[2]));
                            }
                            break;
                        default:
                            Client.Send(Encoding.ASCII.GetBytes("Unknown NWeb command."));
                            break;
                    }
                }
            }
        }
    }
}