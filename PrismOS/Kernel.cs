using static PrismOS.Libraries.Graphics.GUI.WindowManager;
using PrismOS.Libraries.Graphics.GUI.Elements;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using PrismOS.Libraries.Graphics.GUI;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;
using Cosmos.System.FileSystem;
using Cosmos.System;
using System;

namespace PrismOS // Created on May 11th, 2021, 1:26 AM UTC-8
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static List<(Action, string)> BootTasks = new()
        {
            (() => { Canvas = new(960, 540); }, "Creating new canvas instace..."),
            (() => { System.Console.Clear(); }, "Clearing console..."),
            (() => { Booting = true; }, "Updating boot status..."),
            (() => { VFS = new(); }, "Creating new VFS instance..."),
            (() => { VFS.Initialize(true); }, "Initilizing VFS..."),
            (() => { VFSManager.RegisterVFS(VFS); }, "Registering VFS..."),
            (() => { new DHCPClient().SendDiscoverPacket(); DNS = new(); }, "Starting network services..."),
            (() =>
            {
                WM = new();

                Window Settings = new()
                {
                    X = 200,
                    Y = 200,
                    Width = 400,
                    Height = 150,
                    Radius = GlobalRadius,
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
                            Radius = GlobalRadius,
                            Text = "X",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                Parent.Visible = false;
                            },
                        },

                        new Textbox()
                        {
                            X = 200 - 64,
                            Y = 75 - 8,
                            Width = 128,
                            Height = 16,
                            Radius = GlobalRadius,
                        },
                        new Button() // 1080p
                        {
                            X = 200 - 64,
                            Y = 75 + 8,
                            Width = 128,
                            Height = 16,
                            Radius = GlobalRadius,
                            Text = "Set resolution",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                string[] Data = ((Textbox)Parent.Elements[0]).Text.Split('x');
                                Canvas.VBE.DisableDisplay();
                                Canvas = new(int.Parse(Data[0]), int.Parse(Data[1]));
                            },
                        },
                    },
                };
                Window AppMenu = new()
                {
                    X = 200,
                    Y = 200,
                    Width = 512,
                    Height = 256,
                    Radius = GlobalRadius,
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
                            Radius = GlobalRadius,
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
                            Radius = GlobalRadius,
                            Text = "Clock",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM.ShowMessage("Error", "Application not implementeed yet!", "Ok");
                            },
                        },
                        new Button()
                        {
                            X = 128 + 20,
                            Y = 15,
                            Width = 128,
                            Height = 20,
                            Radius = GlobalRadius,
                            Text = "Settings",
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM.Windows[WM.Windows.IndexOf(Settings)].Visible = !WM.Windows[WM.Windows.IndexOf(Settings)].Visible;
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
                    Radius = GlobalRadius,
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
                            Radius = GlobalRadius,
                            OnClick = (ref Element E, ref Window Parent) =>
                            {
                                WM.Windows[WM.Windows.IndexOf(AppMenu)].Visible = !WM.Windows[WM.Windows.IndexOf(AppMenu)].Visible;
                            },
                        }
                    },
                };

                WM.Windows.Add(TaskBar);
                WM.Windows.Add(AppMenu);
                WM.Windows.Add(Settings);
                WM.ShowMessage("Help screen", "Hello! Welcome to the prism desktop.\nPress +/- to set global radius. Press enter to restart the desktop.\n", "Ok");
            }, "Starting desktop..."),
            (() => { Booting = false; }, "Updating boot status..."),
        };
        public static int GlobalRadius = 6;
        public static WindowManager WM;
        public static CosmosVFS VFS;
        public static DnsClient DNS;
        public static Canvas Canvas;
        public static bool Booting;

        protected override void BeforeRun()
        {
            foreach ((Action, string) T in BootTasks)
            {
                T.Item1.Invoke();
                Canvas.Clear();
                Canvas.DrawImage(Canvas.Width / 2 - 128, Canvas.Height / 2 - 128, 256, 256, Files.Resources.Logo);
                Canvas.DrawString(Canvas.Width / 2, Canvas.Height / 2 + 128, $"Prism OS\nPowered by cosmos.\n\n{T.Item2}", Color.White, true);
                Canvas.Update(false);
            }
        }

        protected override void Run()
        {
            try
            {
                if (KeyboardManager.TryReadKey(out var Key))
                {
                    switch (Key.Key)
                    {
                        case ConsoleKeyEx.NumPlus:
                            GlobalRadius++;
                            break;
                        case ConsoleKeyEx.NumMinus:
                            GlobalRadius = GlobalRadius == 0 ? 0 : GlobalRadius - 1;
                            break;
                        case ConsoleKeyEx.Enter:
                            Cosmos.Core.GCImplementation.Free(WM);
                            BootTasks[^2].Item1.Invoke();
                            break;
                    }
                }
                Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}\nFree Memmory: {Cosmos.Core.GCImplementation.GetAvailableRAM()} MB\nGlobal radius: " + GlobalRadius, Color.Black);
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