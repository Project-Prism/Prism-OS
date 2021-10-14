using Cosmos.System.Network.IPv4;
using System;

namespace PrismProject.Networking
{
    class UDP_Client
    {
        public UDP_Client(Address aAddress, int aPort)
        {
            throw new NotImplementedException("The UDP Client has not yet been implemented.");
            using (var aClient = new Cosmos.System.Network.IPv4.UDP.UdpClient(aAddress, aPort))
            {

            }
        }
    }
}
