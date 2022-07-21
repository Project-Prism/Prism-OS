using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Runtime;
using PrismOS.Libraries.UI;
using Cosmos.Core;
using System;
using Cosmos.System.Audio;
using Cosmos.HAL.Drivers.PCI.Audio;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static FrameBuffer Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        private static DateTime LT = DateTime.Now;
        public static AudioMixer Mixer = new();
        private static uint Frames = 0;
        public static uint FPS = 0;

        protected override void BeforeRun()
        {
            Canvas.DrawString((int)(Canvas.Width / 2), (int)(Canvas.Height / 2), "Please Wait...", FrameBuffer.Font.Default, Color.White, true);
            MemoryOperations.Copy((uint*)VBE.getLfbOffset(), Canvas.Internal, (int)Canvas.Size);

            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            Mouse.ScreenWidth = Canvas.Width;
            Mouse.ScreenHeight = Canvas.Height;
            _ = new AppTemplate1();
            Play(Assets.W98);
        }

        protected override void Run()
        {
            MemoryOperations.Copy(Canvas.Internal, Assets.Wallpaper.Internal, (int)Canvas.Size);
            Canvas.DrawFilledRectangle(0, 0, FrameBuffer.Font.Default.Width * $"Memory Available: {GCImplementation.GetAvailableRAM()} MB".Length + 30, FrameBuffer.Font.Default.Height * 2 + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {FPS}", FrameBuffer.Font.Default, Color.White);
            Canvas.DrawString(15, 30, $"Memory Available: {GCImplementation.GetAvailableRAM()} MB", FrameBuffer.Font.Default, Color.White);

            foreach (Application App in Application.Applications)
            {
                App.OnUpdate();
            }
            foreach (Window Window in Window.Windows)
            {
                Window.Update(Canvas);
            }

            Frames++;
            if ((DateTime.Now - LT).TotalSeconds >= 1)
            {
                FPS = Frames;
                Frames = 0;
                LT = DateTime.Now;
            }
            Canvas.DrawImage((int)Mouse.X, (int)Mouse.Y, Assets.Cursor);

            MemoryOperations.Copy((uint*)VBE.getLfbOffset(), Canvas.Internal, (int)Canvas.Size);
        }

        public static void Play(AudioStream Stream)
        {
            Mixer.Streams.Add(Stream);
            new AudioManager()
            {
                Stream = Mixer,
                Output = AC97.Initialize(4096),
            }.Enable();
        }
    }
}