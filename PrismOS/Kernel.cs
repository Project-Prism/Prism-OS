using Mouse = Cosmos.System.MouseManager;
using Cosmos.HAL.Drivers.PCI.Audio;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Runtime;
using PrismOS.Libraries.UI;
using Cosmos.System.Audio;
using Cosmos.Core;
using Cosmos.HAL;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static FrameBuffer Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        public static AudioMixer Mixer = new();

        protected override void BeforeRun()
        {
            // Draw Boot Screen
            Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Logo256);
            MemoryOperations.Copy((uint*)VBE.getLfbOffset(), Canvas.Internal, (int)Canvas.Size);

            // Setup Networking
            new Cosmos.System.Network.IPv4.UDP.DHCP.DHCPClient().SendDiscoverPacket();

            // Setup Gui
            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            Mouse.ScreenWidth = Canvas.Width;
            Mouse.ScreenHeight = Canvas.Height;

            // Create "Headless" Objects
            _ = new Desktop();

            // Play Startup Sound
            Play(Assets.Window98Startup);
        }

        protected override void Run()
        {
            Canvas.DrawImage(0, 0, Assets.Wallpaper, false);
            Canvas.DrawFilledRectangle(0, 0, FrameBuffer.Font.Default.Width * $"FPS: {Canvas.FPS}".Length + 30, FrameBuffer.Font.Default.Height + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}", FrameBuffer.Font.Default, Color.White);

            bool KeyPress = Cosmos.System.KeyboardManager.TryReadKey(out var Key);

            foreach (Application App in Application.Applications)
            {
                if (KeyPress)
                {
                    App.OnKey(Key);
                }

                App.OnUpdate();
            }
            foreach (Window Window in Window.Windows)
            {
                if (Window.Windows[^1] == Window && KeyPress)
                {
                    Window.OnKey(Key);
                }
                Window.Update(Canvas);
            }

            Canvas.DrawImage((int)Mouse.X, (int)Mouse.Y, Assets.Cursor);

            // Update The Screen
            Canvas.Copy((uint*)VBE.getLfbOffset());
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