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

        /*
        protected override void BeforeRun()
        {
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
        */

        protected override void Run()
        {
            SaltCanvas c = new(1920, 1080);
            for (int i = 0; i < c.Width; i++)
            {
                for (int f = 0; f < c.Height; f++)
                {
                    c.SetPixel(i, f, System.Drawing.Color.White);
                }
            }
            c.Update();
            while(true)
            {

            }
        }
    }
}