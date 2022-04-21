using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using PrismOS.Libraries.GUI;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static WindowManager WM = new();
        public static bool ShowStart = false;
        public static Canvas Canvas;
        public static CosmosVFS VFS;

        protected override void BeforeRun()
        {
            Canvas = new(960, 540);
            VFS = new();
            VFS.Initialize(false);
            VFSManager.RegisterVFS(VFS);
            new DHCPClient().SendDiscoverPacket();
            WM.Windows.Add(new()
            {
                X = 0,
                Y = Canvas.Height - 32,
                Width = Canvas.Width,
                Height = Canvas.Height,
                Elements = new()
                {
                    new Libraries.GUI.Elements.Button()
                    {
                        X = 0,
                        Y = Canvas.Height - 32,
                        Width = 50,
                        Height = 32,
                        Radius = 0,
                        Text = "Start",
                        OnClick = (ref Libraries.GUI.Elements.Element E) => { ShowStart = !ShowStart; },
                    },
                    new Libraries.GUI.Elements.Panel()
                    {
                        X = 0,
                        Y = Canvas.Height - 32 - 200,
                        Width = 100,
                        Height = 200,
                        Radius = 0,
                        Color = Color.White,
                        OnUpdate = (ref Libraries.GUI.Elements.Element E) => { E.Visible = ShowStart; },
                    },
                }
            });
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.GoogleYellow);
                WM.Update(Canvas);
                Canvas.Update();
            }
            catch { }
        }
    }
}