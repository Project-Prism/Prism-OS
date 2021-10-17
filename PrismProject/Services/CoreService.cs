using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace PrismProject.Services
{
    class CoreService
    {
        public static void Shutdown()
        {
            _System.Environment.IsShuttingDown = true;
            using (DHCPClient xClient = new DHCPClient()) // Disable networking
            {
                xClient.SendReleasePacket();
                xClient.Close();
                xClient.Dispose();
            }
            Cosmos.System.Power.Shutdown();
        }
    }
}
