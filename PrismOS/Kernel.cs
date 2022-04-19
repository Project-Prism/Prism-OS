// BROKEN:
// Circles
// mouse image / loading (ln 19)

using Cosmos.System.Network.IPv4.UDP.DHCP;
using Mouse = Cosmos.System.MouseManager;
using static PrismOS.Files.Resources;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Formats;
using Cosmos.System.FileSystem;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static string TaskString
        {
            get
            {
                return
                    Canvas.FPS + " FPS" +
                    "\nMemory Used: " + Cosmos.Core.GCImplementation.GetUsedRAM() / 1000000 + " MB" +
                    "\nMemory Free: " + Cosmos.Core.GCImplementation.GetAvailableRAM() + " MB" +
                    "\nTotal Memory: " + Cosmos.Core.GCImplementation.GetAvailableRAM() + (Cosmos.Core.GCImplementation.GetUsedRAM() / 1000000) + " MB";
            }
        }
        public static ContentPage Page1 = new()
        {
            X = 100,
            Y = 100,
            Width = 400,
            Height = 150,
            Text = "Task Manager - Performance",

            Children = new()
            {
                new()
                {
                    X = 15,
                    Y = 15,
                    OnUpdate = (ref ContentPage.Element E) => { E.Text = TaskString; },
                    Type = 0x01,
                },
            }
        };
        public static Canvas Canvas;
        public static CosmosVFS VFS;

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
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Cursor);
                Canvas.Update();
            }
            catch { }
        }
    }
}