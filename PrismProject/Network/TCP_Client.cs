using static PrismProject.Network.NetworkCore;
using Cosmos.System.Network.IPv4.TCP;
using static System.Text.Encoding;
using static System.Console;
using Cosmos.System.Network.IPv4;

namespace PrismProject.Network
{
    class TCP_Client
    {
        public static void TCPC(Address IP, int Port)
        {
            try
            {
                using (TcpClient xClient = new TcpClient(Port))
                {
                    xClient.Connect(IP, Port);
                    while (xClient.IsConnected())
                    {
                        EndPoint endpoint = new EndPoint(IP, (ushort)Port);
                        Write(ASCII.GetString(xClient.Receive(ref endpoint)));
                        xClient.Send(ASCII.GetBytes(ReadLine() + "\n"));
                    }
                    // popup for disconect should go here
                    xClient.Dispose();
                }
            }
            catch { return; }
        }
    }
}
