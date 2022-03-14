using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
using System.Collections.Generic;
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

        protected override void BeforeRun()
        {
            base.BeforeRun();
            Canvas = new(960, 540, true);
            VFS = new();
            VFS.Initialize(false);
            VFSManager.RegisterVFS(VFS);
            GUI.Windows.Add(new(50, 50, 200, 100, 0, "Hello, World!", null, Color.StackOverflowBlack, Color.White, null));
            GUI.Windows[0].Children.Add(new(0, 20, 0, 0, 0, "Some subtext.", null, Color.White, Color.Transparent, GUI.Windows[0]));
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.CoolGreen);
                Canvas.DrawFilledRectangle(0, Canvas.Height - 25, Canvas.Width, 25, 0, Color.StackOverflowBlack);
                foreach(GUI.Element E in GUI.Windows)
                {
                    E.Draw(Canvas);
                }
                Canvas.DrawString(5, Canvas.Height - Canvas.Font.Default.Height, Strings_EN.OSMessage, Color.White);
                Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.White);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
            catch { }
        }
    }
}