using Cosmos.System.Network.IPv4.UDP.DHCP;
using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Formats;
using Cosmos.System.FileSystem;
using PrismOS.Libraries.GUI;
using System;
using System.Collections.Generic;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static Image Cursor = new(Files.Resources.CursorB);
        public static Canvas Canvas;
        public static CosmosVFS VFS;
        public static WindowManager WM;
        public static bool ShowStart = false;

        protected override void BeforeRun()
        {
            VFS = new();
            VFS.Initialize(false);
            VFSManager.RegisterVFS(VFS);
            new DHCPClient().SendDiscoverPacket();
            WM.Windows.Add(new()
                    {
                        X = 0,
                        Y = Canvas.Height - 32,
                        Width = Canvas.Width,
                        Height = 32,
                        Radius = 0,
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
                                Visible = true,
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
                                Visible = true,
                                OnUpdate = (ref Libraries.GUI.Elements.Element E) => { E.Visible = ShowStart; },
                            },
                        }
                    });
            Canvas = new(1280, 720);
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.CoolGreen);
                WM.Update(Canvas);
                Canvas.DrawImage((int)Mouse.X, (int)Mouse.Y, Cursor);
                Canvas.Update();
            }
            catch (Exception EX)
            {
                WM.Windows.Add(new()
                {
                    X = 15,
                    Y = 15,
                    Width = 300,
                    Height = 150,
                    Elements = new()
                    {
                        new Libraries.GUI.Elements.Label()
                        {
                            X = 0,
                            Y = 0,
                            Text = "A critical error occured!\n" + EX.Message,
                            Color = Color.White,
                        }
                    }
                });
            }
        }
    }
}