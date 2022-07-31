using Mouse = Cosmos.System.MouseManager;
using Cosmos.HAL.Drivers.PCI.Audio;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Runtime;
using PrismOS.Libraries.UI;
using Cosmos.System.Audio;
using Cosmos.Core;

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

            // Setup Gui
            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            Mouse.ScreenWidth = Canvas.Width;
            Mouse.ScreenHeight = Canvas.Height;

            // Create "Headless" Objects
            Desktop D = new();
            D.Add(() => { _ = new AppTemplate1(); });
            D.Add(() => { _ = new Console(); });

            // Play Startup Sound
            Play(Assets.Window98Startup);
        }

        protected override void Run()
        {
            Canvas.DrawImage(0, 0, Assets.Wallpaper, false);
            Canvas.DrawFilledRectangle(0, 0, (int)Canvas.MeasureString($"FPS: {Canvas.FPS}", Font.Default) + 30, (int)Font.Default.Size + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}", Font.Default, Color.White);

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
            catch (System.Exception E)
            {
                Cosmos.HAL.Debug.Serial.SendString($"[WARN] Unable To Play Audio! ({E.Message})");
            }
        }
    }
}