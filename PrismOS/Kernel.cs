using static PrismOS.Libraries.Graphics.ContentPage;
using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
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
        public static ContentPage Page1 = new(50, 50, 250, 100, 0, "Window1", null, new()
        {
            new(5, (Canvas.Font.Default.Height + 2) * 0, 0, 0, 0, "FPS: 0", null, Page1, 0x01),
            new(5, (Canvas.Font.Default.Height + 2) * 1, 0, 0, 0, "Runtime: .Net 5.0", null, Page1, 0x01),
            new(5, (Canvas.Font.Default.Height + 2) * 2, 100, 14, 0, "Reboot", null, Page1, 0x02, (ref Element This) => { Cosmos.System.Power.Reboot(); }),
            new(5, (Canvas.Font.Default.Height + 2) * 3, 100, 14, 0, "Shutdown", null, Page1, 0x02, (ref Element This) => { Cosmos.System.Power.Shutdown(); }),
        });
        public static DateTime BootTime = DateTime.UtcNow;

        protected override void BeforeRun()
        {
            Canvas = new(960, 540, true);
            VFS = new();
            VFS.Initialize(false);
            VFSManager.RegisterVFS(VFS);
        }

        protected override void Run()
        {
            try
            {
                Canvas.Clear(Color.CoolGreen);
                Page1.Update(Canvas);
                Page1.Children[0].Text = "FPS: " + Canvas.FPS;
                Canvas.DrawFilledRectangle(0, Canvas.Height - 25, Canvas.Width, 25, 0, Color.StackOverflowBlack);
                Canvas.DrawString(5, Canvas.Height - Canvas.Font.Default.Height, Strings_EN.OSMessage, Color.White);

                Mouse.X = (uint)Math.Clamp(Mouse.X, 0, Canvas.Width - Files.Resources.Cursor.Width);
                Mouse.Y = (uint)Math.Clamp(Mouse.Y, 0, Canvas.Height - Files.Resources.Cursor.Height);

                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
            catch { }
        }
    }
}