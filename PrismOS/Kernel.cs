using static PrismOS.Libraries.Graphics.ContentPage;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public struct Strings_EN
        {
            public const string BootMessage = "Please Wait...";
            public const string OSMessage = "Prism OS, Compiled with .Net 5.0";
        }
        public static Canvas Canvas;
        public static CosmosVFS VFS;
        public static ContentPage Page1 = new(50, 50, 250, 100, 0, "Window1", null, new()
        {
            new(5, (Canvas.Font.Default.Height + 2) * 0, 0, 0, 0, "FPS: 0", null, Page1, 0x01),
            new(5, (Canvas.Font.Default.Height + 2) * 1, 0, 0, 0, "Server status: Offline", null, Page1, 0x01),
            new(5, (Canvas.Font.Default.Height + 2) * 2, 100, 14, 0, "Start server", null, Page1, 0x02, (ref Element This) => { if (!Started) { TCP.Start(); This.Text = "Server status: Online"; Started = true; } else { TCP.Stop(); This.Text = "Server status: Offline"; Started = false; } }),
        });
        public static DateTime BootTime = DateTime.UtcNow;
        public static TcpListener TCP = new(80);
        public static bool Started = false;

        protected override void BeforeRun()
        {
            Canvas = new(960, 540, true);
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
                Page1.Children[0].Text = "FPS: " + Canvas.FPS;
                if (Started)
                {
                    TCP.AcceptTcpClient(10).Send(System.Text.Encoding.ASCII.GetBytes(Libraries.Network.HTTP.GenerateHTTP("<title>Prism OS</title><button OnClick=\"alert(1)\">Click me!</button>")));
                }
                Canvas.DrawFilledRectangle(0, Canvas.Height - 25, Canvas.Width, 25, 0, Color.StackOverflowBlack);
                Canvas.DrawString(5, Canvas.Height - Canvas.Font.Default.Height, Strings_EN.OSMessage, Color.White);
                Canvas.Update();
            }
            catch { }
        }
    }
}