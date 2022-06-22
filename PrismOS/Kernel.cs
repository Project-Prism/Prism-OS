using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using Cosmos.System.FileSystem.VFS;
using System.Collections.Generic;
using Cosmos.System.FileSystem;
using PrismOS.Graphics;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static List<(Action, string)> BootTasks = new()
        {
            (() => { Canvas = new(960, 540); }, "Creating new canvas instance..."),
            (() => { VFS.Initialize(true); }, "Initializing VFS..."),
            (() => { VFSManager.RegisterVFS(VFS); }, "Registering VFS..."),
            (() => { new DHCPClient().SendDiscoverPacket(); DNS = new(); }, "Starting network services..."),
            (() => { _ = new Applications.AppTemplate1(); }, "Creating app..."),
        };
        public static CosmosVFS VFS = new();
        public static DnsClient DNS = new();
        public static Canvas Canvas;
        public static bool Booting;

        protected override void BeforeRun()
        {
            Booting = true;
            foreach ((Action, string) T in BootTasks)
            {
                T.Item1.Invoke();
                Canvas.Clear();
                Canvas.DrawImage((int)(Canvas.Width / 2 - 128), (int)(Canvas.Height / 2 - 128), 256, 256, Assets.Logo);
                Canvas.DrawString((int)(Canvas.Width / 2), (int)(Canvas.Height / 2 + 128), $"Prism OS\nPowered by the cosmos Kernel.\n\n{T.Item2}", Canvas.Font.Default, Color.White, true);
                Canvas.Update(false);
            }
            Booting = false;
        }

        protected override void Run()
        {
            try
            {
                Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}", Canvas.Font.Default, Color.Black);
                Runtime.Update();
                Canvas.Update(true);
            }
            catch (Exception EX)
            {
                #region Crash Screen

                Canvas.Clear();
                Canvas.DrawImage((int)(Canvas.Width / 2 - 128), (int)(Canvas.Height / 2 - 128), 256, 256, Assets.Logo);
                string Error = $"[!] Critical failure [!]\nPrism OS has {(Booting ? "failed to boot" : "crashed")}! see error message below.\n" + EX.Message;
                Canvas.DrawString((int)(Canvas.Width / 2), (int)(Canvas.Height / 2 + 128), Error, Canvas.Font.Default, Color.Red, true);
                Canvas.Update(false);
                while (true) { }

                #endregion
            }
        }
    }
}