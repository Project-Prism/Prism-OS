using static PrismProject.Functions.Services.NetoworkService;
using Cosmos.System.Network.IPv4.TCP;
using static System.Text.Encoding;
using static System.Console;
using Cosmos.System.Network.IPv4;

namespace PrismProject.Functions.Core
{
    class TCPClient
    {
        public static void TCPC(Address IP, int Port)
        {
            try
            {
                using (TcpClient xClient = new TcpClient(Port))
                {
                    xClient.Connect(IP, Port);
                    while (true)
                    {
                        EndPoint endpoint = new EndPoint(IP, (ushort)Port);
                        Write(ASCII.GetString(xClient.Receive(ref endpoint)));
                        xClient.Send(ASCII.GetBytes(ReadLine() + "\n"));
                    }
                }
            }
            catch { return; }
        }

        public static void Download(string IP, int Port, string SaveTo)
        {
            using (TcpClient xClient = new TcpClient(Port))
            {
                var IP2 = DNS(Gateway1, IP);
                xClient.Connect(IP2, Port);
                while (true)
                {
                    EndPoint endpoint = new EndPoint(IP2, (ushort)Port);
                    FileSystem.WriteFile(SaveTo, ASCII.GetString(xClient.Receive(ref endpoint)), false);
                    xClient.Dispose();
                }
            }
        }
    }
}
