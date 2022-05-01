using static PrismOS.Libraries.Graphics.GUI.WindowManager;
using PrismOS.Libraries.Graphics.GUI.Elements;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using PrismOS.Libraries.Graphics.GUI;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using System;

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
            Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 128, 128, Files.Resources.Logo);
            Canvas.Update();
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
                            new Button()
                            {
                                X = 0,
                                Y = 0,
                                Width = 64,
                                Height = 32,
                                Radius = 0,
                                Text = "Start",
                                OnClick = (ref Element E, ref Window Parent) => { Parent.Elements[2].Visible = !Parent.Elements[2].Visible; },
                            },
                            new Button()
                            {
                                X = 70,
                                Y = 0,
                                Width = 32,
                                Height = 32,
                                Radius = 0,
                                Text = "M",
                                OnClick = (ref Element E, ref Window Parent) => { WM.Windows.Add(new()
                                {
                                    X = 200,
                                    Y = 50,
                                    Width = 300,
                                    Height = 150,
                                    Draggable = true,
                                    Text = "Clock",
                                    Elements = new()
                                    {
                                        new Clock()
                                        {
                                            X = 150,
                                            Y = 75,
                                            Radius = 50,
                                            OnUpdate = (ref Element E, ref Window Parent) => { ((Clock)E).Time = DateTime.Now; },
                                        },
                                        new Button()
                                        {
                                            X = 285,
                                            Y = 0,
                                            Width = 15,
                                            Height = 15,
                                            Text = "X",
                                            OnClick = (ref Element E, ref Window Parent) => { WM.Windows.Remove(Parent); },
                                        },
                                    },
                                }); },
                            },
                            new Panel()
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
                        new Panel()
                        {
                            X = 0,
                            Y = 0,
                            Height = 12,
                            Width = 300,
                            Radius = 0,
                            Color = Color.Black,
                        },
                        new Label()
                        {
                            X = 0,
                            Y = 0,
                            Color = Color.White,
                            Text = "Critical error!",
                        },
                        new Label()
                        {
                            X = 50,
                            Y = 50,
                            Color = Color.White,
                            Text = EX.Message,
                        },
                        new Button()
                        {
                            X = 284,
                            Y = 115,
                            Width = 12,
                            Height = 35,
                            Text = "Okay",
                            Radius = 0,
                            OnClick = (ref Element E, ref Window Parent) => { WM.Windows.Remove(Parent); },
                        },
                    },
                });
            }
        }
    }
}