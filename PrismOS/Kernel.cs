using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;
using PrismOS.Libraries.Runtime;
using PrismOS.Libraries.UI;
using Cosmos.Core;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static FrameBuffer Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        public static List<Application> Applications = new();
        public static List<Window> Windows = new();
        private static DateTime LT = DateTime.Now;
        public static bool Dragging = false;
        private static uint Frames = 0;
        public static uint FPS = 0;

        protected override void BeforeRun()
        {
            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            Mouse.ScreenWidth = Canvas.Width;
            Mouse.ScreenHeight = Canvas.Height;
            //_ = new AppTemplate1();
        }

        protected override void Run()
        {
            MemoryOperations.Copy(Canvas.Internal, Assets.Wallpaper.Internal, (int)Canvas.Size);
            Canvas.DrawFilledRectangle(0, 0, FrameBuffer.Font.Default.Width * $"FPS: {FPS}".Length + 30, FrameBuffer.Font.Default.Height + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {FPS}", FrameBuffer.Font.Default, Color.White);

            foreach (Application App in Applications)
            {
                App.OnUpdate();
            }
            foreach (Window Window in Windows)
            {
                if (Window.Draggable)
                {
                    if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        if (Mouse.X > Window.X && Mouse.X < Window.X + Window.Width && Mouse.Y > Window.Y && Mouse.Y < Window.Y + 20 && !Window.Moving && !Dragging)
                        {
                            Dragging = true;
                            Windows.Remove(Window);
                            Windows.Insert(Windows.Count, Window);
                            Window.Moving = true;
                            Window.IX = (int)Mouse.X - Window.X;
                            Window.IY = (int)Mouse.Y - Window.Y;
                        }
                    }
                    else
                    {
                        Dragging = false;
                        Window.Moving = false;
                    }
                    if (Window.Moving)
                    {
                        Window.X = (int)Mouse.X - Window.IX;
                        Window.Y = (int)Mouse.Y - Window.IY;
                    }
                }

                Window.Update(Canvas);
            }

            Frames++;
            if ((DateTime.Now - LT).TotalSeconds >= 1)
            {
                Cosmos.Core.Memory.Heap.Collect();
                FPS = Frames;
                Frames = 0;
                LT = DateTime.Now;
            }
            Canvas.DrawImage((int)Mouse.X, (int)Mouse.Y, Assets.Cursor);
            
            MemoryOperations.Copy((uint*)VBE.getLfbOffset(), Canvas.Internal, (int)Canvas.Size);
        }
    }
}