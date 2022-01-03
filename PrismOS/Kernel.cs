using PrismOS.UI;
using Cosmos.System.Network.Config;
using Cosmos.HAL;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static readonly CosmosVFS VFS = new();

        protected override void BeforeRun()
        {
            // Start the network and filesystem
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);

            IPConfig.Enable(
                NetworkDevice.GetDeviceByName("eth0"),
                new(127, 0, 0, 1),
                new(255, 255, 255, 0),
                new(10, 0, 0, 1));

            DHCPClient xClient = new();
            xClient.SendDiscoverPacket();
            xClient.Dispose();
        }

        protected override void Run()
        {
            SaltUI.Windows.Add(new SaltUI.Window(50, 50, 100, 50));
            SaltUI.Windows[0].Children.Add(new SaltUI.Text(0, 0, "Haii"));

            while (true)
            {
                SaltUI.Tick();
            }
        }
    }
}