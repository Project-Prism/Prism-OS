using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;

namespace PrismOS.Network.NWeb
{
    public class NWebClient
    {
        public NWebClient(Address Address)
        {
            xAddress = Address;
            xClient = new(1080);
            xEndPoint = new(Address.Zero, 1080);
            xClient.Connect(xAddress, 1080);
        }

        public Address xAddress;
        public TcpClient xClient;
        public EndPoint xEndPoint;

        public byte[] Get(string Path)
        {
            xClient.Send(Encoding.ASCII.GetBytes("Get: " + Path));
            return xClient.Receive(ref xEndPoint);
        }

        public void Post(string Path, byte[] File)
        {
            xClient.Send(Encoding.ASCII.GetBytes("Post: " + Path + ": " + File));
        }
    }
}
