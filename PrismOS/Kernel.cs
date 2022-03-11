using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
using System.Collections.Generic;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using System.Drawing;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static Canvas Canvas;
        public static CosmosVFS VFS;
        public List<BootTask> BootTasks = new()
        {
            () => { Console.WriteLine("Unable to initiate fastflux ultra-cryptic portable computer zionator. (a.k.a: You are screwed.) Have fun with a fried pc!"); },
            () => { Cosmos.System.PCSpeaker.Beep(1000, (uint)Cosmos.System.Durations.Sixteenth); },
            () => { Canvas = new(1024, 768, true); },
            () => { Canvas.DrawString(0, 0, "Please Wait...", Color.White, Canvas.Position.Center); },
            () => { Canvas.Update(); },
            () => { VFS = new CosmosVFS(); },
            () => { VFS.Initialize(false); },
            () => { VFSManager.RegisterVFS(VFS); },
            () => { Console.WriteLine("Initiating self destruction sequence..."); }
        };
        public delegate void BootTask();

        public bool Clicked = false;
        public int Clicks = 0;
        public Color C;

        protected override void Run()
        {
            //Runtime.BatchFile BF = new("cls\n@echo off\necho Hello, World!\necho Written in batch!\n@echo\ntimeout \t 9999");

            //while (true)
            //{
            //    BF.Cycle();
            //}

            Libraries.Sound.PCSpeaker.PlayMono(Files.Resources.Audio);

            try
            {
                for (int I = 0; I < BootTasks.Count; I++)
                {
                    BootTasks[I].Invoke();
                }

                while (true)
                {
                    Canvas.Clear(Color.Green);

                    if (Mouse.X > Canvas.Width / 2 - 100 && Mouse.Y > Canvas.Height / 2 - 20 && Mouse.X < Canvas.Width / 2 + 100 && Mouse.Y < Canvas.Height / 2 + 20 && Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        if (Clicked == false)
                            Clicks++;
                        Clicked = true;
                        C = Color.DarkSlateGray;
                    }
                    else if (Mouse.X > Canvas.Width / 2 - 100 && Mouse.Y > Canvas.Height / 2 - 20 && Mouse.X < Canvas.Width / 2 + 100 && Mouse.Y < Canvas.Height / 2 + 20)
                    {
                        Clicked = false;
                        C = Color.LightGray;
                    }
                    else
                    {
                        C = Color.White;
                    }

                    Canvas.DrawFilledRectangle(0, 0, 200, 40, 0, C, Canvas.Position.Center);
                    Canvas.DrawString(0, 0, "Click me!\n" + Clicks + " clicks.", Color.Black, Canvas.Position.Center);
                    Canvas.DrawString(0, 0, "left text!", Color.White, Canvas.Position.Left);
                    Canvas.DrawString(0, 0, "right text!", Color.White, Canvas.Position.Right);
                    Canvas.DrawString(0, 0, "top text!", Color.White, Canvas.Position.Top);
                    Canvas.DrawString(0, 0, "bottom text!", Color.White, Canvas.Position.Bottom);

                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.White);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();

                    Libraries.Interfaces.Events.Update();
                }
            }
            catch (Exception EX)
            {
                Canvas.Clear(Color.ForestGreen);
                Canvas.DrawFilledRectangle(0, 0, 400, 50, 0, Color.FromArgb(25, 25, 25), Canvas.Position.Center);
                Canvas.DrawString(0, 0, EX.Message, Color.White, Canvas.Position.Center);
                Canvas.Update();
                while (true) { }
            }
        }
    }
}