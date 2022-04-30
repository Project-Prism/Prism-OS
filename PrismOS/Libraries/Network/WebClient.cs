using GC = Cosmos.Core.GCImplementation;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;

namespace PrismOS.Libraries.Network
{
    public class WebClient : System.IDisposable
    {
        public WebClient()
        {
        }
        public WebClient(string URL)
        {
            this.URL = new(URL);
        }
        public WebClient(URL URL)
        {
            this.URL = URL;
        }

        public URL URL;

        public byte[] DownloadFile(int Port = 80)
        {
            EndPoint EP = new(URL.GetAddress(), (ushort)Port);
            string Request =
                $"GET {URL.GetPath()} HTTP/1.1\n" +
                "Connection: Keep - Alive";

            TcpClient Client = new(URL.GetAddress(), Port);
            Client.Connect(URL.GetAddress(), Port);
            Client.Send(Encoding.UTF8.GetBytes(Request));
            byte[] Binary = Client.Receive(ref EP);
            Client.Dispose();
            return Binary;
        }

        public void Dispose()
        {
            URL.Dispose();
            GC.Free(this);
        }
    }
}