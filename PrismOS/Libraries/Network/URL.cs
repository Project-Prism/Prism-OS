using Cosmos.System.Network.IPv4.UDP.DNS;
using GC = Cosmos.Core.GCImplementation;
using Cosmos.System.Network.IPv4;

namespace PrismOS.Libraries.Network
{
    public class URL : System.IDisposable
    {
        public URL(string FullURL)
        {
            this.FullURL = FullURL;
        }
        
        public string FullURL;
        public string Path => FullURL.Split('/')[^1];
        public string Protocol => FullURL.Split(':')[0];
        public string Domain => FullURL.Replace("//", "&&&&").Split("/")[0].Replace("&&&&", "//") + "/";
        public Address GetAddress()
        {
            DnsClient Client = new();
            Client.Connect(Cosmos.System.Network.Config.NetworkConfiguration.CurrentNetworkConfig.IPConfig.DefaultGateway);
            Client.SendAsk(Domain);
            Address Temp = Client.Receive();
            Client.Dispose();
            return Temp;
        }

        public void Dispose()
        {
            GC.Free(FullURL);
            GC.Free(this);
        }
    }
}