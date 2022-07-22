using Mouse = Cosmos.System.MouseManager;
using Cosmos.HAL.Drivers.PCI.Audio;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Runtime;
using PrismOS.Libraries.UI;
using Cosmos.System.Audio;
using Cosmos.Core.Memory;
using Cosmos.Core;
using Cosmos.HAL;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static FrameBuffer Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        public static AudioMixer Mixer = new();
        private static DateTime LT = DateTime.Now;
        private static uint Frames = 0;
        public static uint FPS = 0;

        protected override void BeforeRun()
        {
            // Draw Boot Screen
            Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Logo256);
            MemoryOperations.Copy((uint*)VBE.getLfbOffset(), Canvas.Internal, (int)Canvas.Size);

            // Setup Gui Stuff
            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            Mouse.ScreenWidth = Canvas.Width;
            Mouse.ScreenHeight = Canvas.Height;

            // Create "Headless" Objects
            //_ = new PIT.PITTimer(new(() => { Heap.Collect(); }), 10000000000, true);
            _ = new PIT.PITTimer(() => { FPS = Frames; Frames = 0; }, 1000000000, true);
            _ = new AppTemplate1();

            // Play Startup Sound
            Play(Assets.Window98Startup);
        }

        protected override void Run()
        {
            Canvas.DrawImage(0, 0, Assets.Wallpaper, false);
            Canvas.DrawFilledRectangle(0, 0, FrameBuffer.Font.Default.Width * $"FPS: {FPS}".Length + 30, FrameBuffer.Font.Default.Height + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {FPS}", FrameBuffer.Font.Default, Color.White);
            
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
            try
            {
                Mixer.Streams.Add(Stream);
                new AudioManager()
                {
                    Stream = Mixer,
                    Output = AC97.Initialize(4096),
                }.Enable();
            }
            catch (Exception E)
            {
                Cosmos.HAL.Debug.Serial.SendString($"[WARN] Unable To Play Audio! ({E.Message})");
            }
        }
    }
}