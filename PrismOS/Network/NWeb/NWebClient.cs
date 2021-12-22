using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;

namespace PrismOS.Network.NWeb
{
    public class NWebClient
    {
        public NWebClient(Address xAddress, string xID)
        {
            Address = xAddress;
            Client = new(1080);
            EndPoint = new(Address.Zero, 1080);
            Client.Connect(Address, 1080);
            ID = xID;
        }

        public Address Address;
        public TcpClient Client;
        public EndPoint EndPoint;
        public string ID;

        public byte[] Get(string Path)
        {
            Client.Send(Encoding.ASCII.GetBytes("GET: " + Path));
            return Client.NonBlockingReceive(ref EndPoint);
        }

        public void Post(string Path, byte[] File)
        {
            Client.Send(Encoding.ASCII.GetBytes("POST: " + Path + ": " + File));
        }
    }
}