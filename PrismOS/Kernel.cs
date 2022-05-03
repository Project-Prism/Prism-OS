using static PrismOS.Libraries.Graphics.GUI.WindowManager;
using PrismOS.Libraries.Graphics.GUI.Elements;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using PrismOS.Libraries.Graphics.GUI;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using Cosmos.System;
using System;

namespace PrismOS // Created on May 11th, 2021, 1:26 AM UTC-8
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static WindowManager WM;
        public static Canvas Canvas;
        public static bool Booting;

        protected override void BeforeRun()
        {
            Booting = true;
            Canvas = new(960, 540);
            Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 256, 256, Files.Resources.Logo);
            Canvas.Update();
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
                        Draggable = false,
                        TitleVisible = false,
                        Elements = new()
                        {
                            // Task bar and start button
                            new Panel()
                            {
                                X = 0,
                                Y = -300,
                                Width = 150,
                                Height = 300,
                                Color = new(Color.Black, 128),
                                Visible = false,
                            },
                            new Button()
                            {
                                X = 0,
                                Y = 0,
                                Width = 64,
                                Height = 32,
                                Text = "Start",
                                OnClick = (ref Element E, ref Window Parent) => { Parent.Elements[0].Visible = !Parent.Elements[0].Visible; },
                            },

                            // App buttons
                            new Button()
                            {
                                X = 70,
                                Y = 0,
                                Width = 32,
                                Height = 32,
                                Text = "C",
                                OnClick = (ref Element E, ref Window Parent) =>
                                {
                                    WM.Windows.Add(new()
                                    {
                                        X = 200,
                                        Y = 50,
                                        Width = 300,
                                        Height = 150,
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
                                                Y = -15,
                                                Width = 15,
                                                Height = 15,
                                                Text = "X",
                                                OnClick = (ref Element E, ref Window Parent) => { WM.Windows.Remove(Parent); },
                                            },
                                        },
                                    });
                                },
                            },
                            new Button()
                            {
                                X = 108,
                                Y = 0,
                                Width = 32,
                                Height = 32,
                                Text = "TT",
                                OnClick = (ref Element E, ref Window Parent) =>
                                {
                                    WM.Windows.Add(new()
                                    {
                                        X = 200,
                                        Y = 50,
                                        Width = 300,
                                        Height = 150,
                                        Text = "typing test",
                                        Elements = new()
                                        {
                                            new Textbox()
                                            {
                                                X = 0,
                                                Y = 150 - 12,
                                                Width = 300,
                                                Height = 12,
                                                OnUpdate = (ref Element E, ref Window Parent) =>
                                                {
                                                    if (KeyboardManager.TryReadKey(out var Key) && Key.Key == ConsoleKeyEx.Enter)
                                                    {
                                                        ((Label)Parent.Elements[1]).Text += '\n' + ((Textbox)E).Text;
                                                    }
                                                }
                                            },
                                            new Label()
                                            {
                                                X = 0,
                                                Y = 0,
                                                Color = Color.White,
                                                Text = "",
                                            },
                                            new Button()
                                            {
                                                X = 285,
                                                Y = -15,
                                                Width = 15,
                                                Height = 15,
                                                Text = "X",
                                                OnClick = (ref Element E, ref Window Parent) => { WM.Windows.Remove(Parent); },
                                            },
                                        },
                                    });
                                },
                            },
                        },
                    }
                },
            };
            VFSManager.RegisterVFS(new CosmosVFS());
            new DHCPClient().SendDiscoverPacket();
            Booting = false;
        }

        protected override void Run()
        {
            try
            {
                Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.Black);
                WM.Update(Canvas);
                Canvas.Update();
            }
            catch (Exception EX)
            {
                #region Crash Screen

                Canvas.Clear();
                Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 256, 256, Files.Resources.Logo);
                string Error = $"[!] Critical failure [!]\nPrism OS has {(Booting ? "failed to boot" : "crashed")}! see error message below.\n" + EX.Message;
                Canvas.DrawString(Canvas.Width / 2, Canvas.Height / 2 + 128, Error, Color.Red, true);
                Canvas.Update();
                while(true) { }

                #endregion
            }
        }
    }
}