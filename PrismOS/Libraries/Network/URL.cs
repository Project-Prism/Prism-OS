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

        public string GetName1()
        {
            return FullURL.Replace("//", "&&&&").Split("/")[0].Replace("&&&&", "//") + "/";
        }
        public string GetName2()
        {
            return FullURL.Replace("//", "&&&&").Split("/")[0].Replace("&&&&", "//").Replace("http://", "").Replace("https://", "");
        }
        public string GetPath()
        {
            string Path = FullURL.Replace(FullURL.Replace("//", "&&&&").Split("/")[0].Replace("&&&&", "//") + "/", "").Insert(0, "/");
            return Path;
        }
        public Address GetAddress()
        {
            DnsClient Client = new();
            Client.Connect(Cosmos.System.Network.Config.NetworkConfig.CurrentConfig.Value.DefaultGateway);
            Client.SendAsk(GetName1());
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