using PrismOS.Libraries.Graphics;
using System;
using PrismOS.Libraries;
using Cosmos.System.FileSystem;
using PrismOS.Libraries.Graphics.GUI;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static WindowManager WM;
        public static Canvas Canvas;
        public static CosmosVFS VFS;

        protected override void BeforeRun()
        {
            Canvas = new(960, 540);
            VFS = new(); VFS.Initialize(false); VFSManager.RegisterVFS(VFS);
            new DHCPClient().SendDiscoverPacket();
            WM = new()
            {
                Windows = new()
                {
                    new()
                    {
                        X = 0,
                        Y = Canvas.Height - 32,
                        Width = Canvas.Width,
                        Height = 32,
                        Radius = 0,
                        Draggable = false,
                        Visible = true,
                        TitleVisible = false,
                        Elements = new()
                        {
                            new Libraries.Graphics.GUI.Elements.Button()
                            {
                                X = 0,
                                Y = 0,
                                Width = 64,
                                Height = 32,
                                Radius = 0,
                                Text = "Start",
                                OnClick = (ref Libraries.Graphics.GUI.Elements.Element E) => { WM.Windows[0].Elements[2].Visible = !WM.Windows[0].Elements[2].Visible; },
                                OnUpdate = (ref Libraries.Graphics.GUI.Elements.Element E) => { },
                            },
                            new Libraries.Graphics.GUI.Elements.Button()
                            {
                                X = 70,
                                Y = 0,
                                Width = 32,
                                Height = 32,
                                Radius = 0,
                                Text = "M",
                                OnClick = (ref Libraries.Graphics.GUI.Elements.Element E) => { WM.Windows.Add(new() { X = 200, Y = 50, Width = 300, Height = 150, Draggable = true, }); },
                            },
                            new Libraries.Graphics.GUI.Elements.Panel()
                            {
                                X = 0,
                                Y = -300,
                                Width = 150,
                                Height = 300,
                                Color = new(Color.Black, 128),
                                Visible = false,
                                Radius = 0,
                            },
                        },
                    },
                }
            };
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.CoolGreen);
                Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.Black);
                WM.Update(Canvas);
                Canvas.Update();
            }
            catch (Exception EX)
            {
                WM.Windows.Add(new()
                {
                    X = Canvas.Width / 2 - 150,
                    Y = Canvas.Height / 2 - 75,
                    Width = 300,
                    Height = 150,
                    Radius = 0,
                    Elements = new()
                    {
                        new Libraries.Graphics.GUI.Elements.Panel()
                        {
                            X = 0,
                            Y = 0,
                            Height = 12,
                            Width = 300,
                            Radius = 0,
                            Color = Color.Black,
                        },
                        new Libraries.Graphics.GUI.Elements.Label()
                        {
                            X = 0,
                            Y = 0,
                            Color = Color.White,
                            Text = "Critical error!",
                        },
                        new Libraries.Graphics.GUI.Elements.Label()
                        {
                            X = 50,
                            Y = 50,
                            Color = Color.White,
                            Text = EX.Message,
                        },
                        new Libraries.Graphics.GUI.Elements.Button()
                        {
                            X = 284,
                            Y = 115,
                            Width = 12,
                            Height = 35,
                            Text = "Okay",
                            Radius = 0,
                        },
                    },
                });
            }
        }
    }
}