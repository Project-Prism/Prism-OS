using Mouse = Cosmos.System.MouseManager;
using Cosmos.System.FileSystem.VFS;
using System.Collections.Generic;
using PrismOS.Libraries.Graphics;
using Cosmos.System.FileSystem;
using System;
using XSharp.Assembler.x86;

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
            new(5, (Canvas.Font.Default.Height + 2) * 2, 0, 0, 0, "UpTime: ", null, Page1, 0x01),
            new(5, (Canvas.Font.Default.Height + 2) * 3, 100, 25, 0, "Reboot", null, Page1, 0x02, () => { Cosmos.System.Power.Reboot(); }),
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

                //XSharp.XS.LiteralCode("mov ax, 0x4f15\nmov bl, 0x01\nxor cx, cx\nxor dx, dx\nint 0x10");

                //uint* EDID = (uint*)ES:DI; (wtf is address ES:DI???)
                // AL = 0x4F if function supported
                // AH = status(0 is success, 1 is fail)
                // ES: DI contains the EDID

                Page1.Update(Canvas);
                Page1.Children[0].Text = "FPS: " + Canvas.FPS;
                Page1.Children[2].Text = "UpTime: " + DateTime.UtcNow.Subtract(BootTime).ToString("d Days, h Hour(s), mm Minute(s), ss Second(s).");
                Canvas.DrawFilledRectangle(0, Canvas.Height - 25, Canvas.Width, 25, 0, Color.StackOverflowBlack);
                Canvas.DrawString(5, Canvas.Height - Canvas.Font.Default.Height, Strings_EN.OSMessage, Color.White);
                Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                Canvas.Update();
            }
            catch { }
        }
    }
}