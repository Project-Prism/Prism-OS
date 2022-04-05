using Cosmos.System.Network.IPv4.UDP.DHCP;
using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static Canvas Canvas;
        public static CosmosVFS VFS;
        public static ContentPage Page1 = new()
        {
            X = 100,
            Y = 100,
            Width = 400,
            Height = 150,
            Text = "Minecraft 1.18.2*",

            Children = new()
            {
                new()
                {
                    X = 15,
                    Y = 15,
                    OnUpdate = (ref ContentPage.Element E) => { E.Text = Canvas.FPS + " FPS"; },
                    Type = 0x01,
                }
            }
        };

        protected override void BeforeRun()
        {
            Canvas = new(960, 540);
            VFS = new();
            VFS.Initialize(false);
            VFSManager.RegisterVFS(VFS);
            new DHCPClient().SendDiscoverPacket();
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.CoolGreen);
                Page1.Update(Canvas);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
            catch { }
        }
    }
}