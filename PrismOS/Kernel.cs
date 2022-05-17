using PrismOS.Libraries.Graphics.GUI.Elements;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using PrismOS.Libraries.Graphics.GUI;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;
using Cosmos.System.FileSystem;
using System;

namespace PrismOS // Created on May 11th, 2021, 1:26 AM UTC-8
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static List<(Action, string)> BootTasks = new()
        {
            (() => { Canvas = new(960, 540); }, "Creating new canvas instace..."),
            (() => { VFS.Initialize(true); }, "Initilizing VFS..."),
            (() => { VFSManager.RegisterVFS(VFS); }, "Registering VFS..."),
            (() => { new DHCPClient().SendDiscoverPacket(); DNS = new(); }, "Starting network services..."),
            (() =>
            {
                Window Settings = new()
                {
                    X = 200,
                    Y = 200,
                    Width = 400,
                    Height = 150,
                    Radius = WM.GlobalRadius,
                    Text = "Settings",
                    Visible = false,
                    Elements = new()
                    {
                        // Close button
                        new Button()
                        {
                            X = 400 - 15,
                            Y = -15,
                            Width = 15,
                            Height = 15,
                            Radius = WM.GlobalRadius,
                            Text = "X",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                Parent.Visible = false;
                            },
                        },

                        new Panel()
                        {
                            X = 200 - 32,
                            Y = 75 - 32,
                            Width = 96,
                            Height = 32,
                            Radius = WM.GlobalRadius,
                            Color = Color.White,
                        },
                        new Label()
                        {
                            X = 200,
                            Y = 75 - 32,
                            Width = 32,
                            Height = 32,
                            Text = "NULL",
                            Color = Color.Black,
                            Center = true,
                            OnUpdate = (ref Element E, ref Window Parent) =>
                            {
                                ((Label)E).Text = WM.GlobalRadius.ToString();
                            }
                        },
                        new Button()
                        {
                            X = 200 - 32,
                            Y = 75 - 32,
                            Width = 32,
                            Height = 32,
                            Radius = WM.GlobalRadius,
                            Text = "-",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM.GlobalRadius--;
                                ((Label)Parent.Elements[2]).Text = (int.Parse(((Label)Parent.Elements[2]).Text) - 1).ToString();
                            },
                        }, // Minus
                        new Button()
                        {
                            X = 200 + 32,
                            Y = 75 - 32,
                            Width = 32,
                            Height = 32,
                            Radius = WM.GlobalRadius,
                            Text = "+",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM.GlobalRadius++;
                                ((Label)Parent.Elements[2]).Text = (int.Parse(((Label)Parent.Elements[2]).Text) + 1).ToString();
                            },
                        }, // Plus
                        new Button()
                        {
                            X = 200 - 32,
                            Y = 75,
                            Width = 96,
                            Height = 32,
                            Radius = WM.GlobalRadius,
                            Text = "Apply",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                BootTasks[^2].Item1.Invoke();
                            },
                        }, // Apply
                    },
                };
                Window AppMenu = new()
                {
                    X = 200,
                    Y = 200,
                    Width = 512,
                    Height = 256,
                    Radius = WM.GlobalRadius,
                    Text = "Applications",
                    Visible = false,
                    Elements = new()
                    {
                        // Close button
                        new Button()
                        {
                            X = 512 - 15,
                            Y = -15,
                            Width = 15,
                            Height = 15,
                            Radius = WM.GlobalRadius,
                            Text = "X",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                Parent.Visible = false;
                            },
                        },

                        new Button()
                        {
                            X = 15,
                            Y = 15,
                            Width = 128,
                            Height = 20,
                            Radius = WM.GlobalRadius,
                            Text = "Clock",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                MessageBox.ShowMessage("Error", "Application not implementeed yet!", "Ok", WM);
                            },
                        },
                        new Button()
                        {
                            X = 128 + 20,
                            Y = 15,
                            Width = 128,
                            Height = 20,
                            Radius = WM.GlobalRadius,
                            Text = "Settings",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM[WM.IndexOf(Settings)].Visible = !WM[WM.IndexOf(Settings)].Visible;
                            },
                        },
                    },
                };
                Window TaskBar = new()
                {
                    X = (Canvas.Width / 2) - 256,
                    Y = Canvas.Height - 64,
                    Width = 512,
                    Height = 32,
                    Radius = WM.GlobalRadius,
                    Draggable = false,
                    TitleVisible = false,
                    Text = "System.Core.TaskBar",
                    Elements = new()
                    {
                        new Button()
                        {
                            X = 0,
                            Y = 0,
                            Width = 128,
                            Height = 32,
                            Text = "Apps",
                            Radius = WM.GlobalRadius,
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM[WM.IndexOf(AppMenu)].Visible = !WM[WM.IndexOf(AppMenu)].Visible;
                            },
                        }
                    },
                };

                WM.Clear();
                WM.Add(TaskBar);
                WM.Add(AppMenu);
                WM.Add(Settings);
            }, "Starting desktop..."),
        };
        public static WindowManager WM = new();
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
                Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 256, 256, Files.Resources.Logo);
                Canvas.DrawString(Canvas.Width / 2, Canvas.Height / 2 + 128, $"Prism OS\nPowered by cosmos.\n\n{T.Item2}", Color.White, true);
                Canvas.Update(false);
            }
            Booting = false;
        }

        protected override void Run()
        {
            try
            {
                Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}", Color.Black);
                WM.Update(Canvas);
                Canvas.Update(true);
            }
            catch (Exception EX)
            {
                #region Crash Screen

                Canvas.Clear();
                Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 256, 256, Files.Resources.Logo);
                string Error = $"[!] Critical failure [!]\nPrism OS has {(Booting ? "failed to boot" : "crashed")}! see error message below.\n" + EX.Message;
                Canvas.DrawString(Canvas.Width / 2, Canvas.Height / 2 + 128, Error, Color.Red, true);
                Canvas.Update(false);
                while (true) { }

                #endregion
            }
        }
    }
}