using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using static PrismProject.Functions.Network.Basic;
using static System.Text.Encoding;
using static System.Console;

namespace PrismProject.Functions.Networking
{
    class BasicTCPClient
    {
        public static void TCPClient(string IP, int Port, string Contents)
        {
            WriteLine("class started");
            WriteLine("Connecting to ip " + NewAddress(IP));
            using (TcpClient xClient = new TcpClient(Port))
            {
                xClient.Connect(NewAddress(IP), Port);
                xClient.Send(ASCII.GetBytes("Debug connected!\n"));

                while (true)
                {
                    EndPoint endpoint = new EndPoint(NewAddress(IP), (ushort)Port);
                    WriteLine(ASCII.GetString(xClient.Receive(ref endpoint)));
                    xClient.Send(ASCII.GetBytes(ReadLine()));
                }
            }
        }
    }
}
