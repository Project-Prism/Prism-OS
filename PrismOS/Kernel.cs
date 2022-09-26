﻿using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio;
using Cosmos.System;
using Cosmos.Core;
using PrismGL2D.Formats;
using PrismGL2D.UI;
using PrismGL2D;
using System;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static Graphics Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        public static AudioMixer Mixer = new();

		protected override void BeforeRun()
        {
            #region Splash Screen

            Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash256);
            Canvas.CopyTo((uint*)VBE.getLfbOffset());
            //Play(Assets.Vista);

            #endregion

            Control.Config.Radius = 5;
            _ = new Frame("Frame Testing")
            {
                Controls = new()
                {
                    new Button()
					{
                        X = 0,
                        Y = (int)Control.Config.Scale,
                        Width = 200,
                        Height = 25,
                        Text = "Click me!",
					},
				}
            };

            #region Misc

            Assets.Wallpaper = (Bitmap)Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            MouseManager.ScreenWidth = Canvas.Width;
            MouseManager.ScreenHeight = Canvas.Height;

            #endregion
        }

        protected override void Run()
        {
            Canvas.DrawImage(0, 0, Assets.Wallpaper, false);
            Canvas.DrawFilledRectangle(0, 0, Control.Config.Font.MeasureString($"FPS: {Canvas.GetFPS()}") + 30, Control.Config.Font.Size + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {Canvas.GetFPS()}", Control.Config.Font, Color.White);

            bool KeyPress = TryReadKey(out ConsoleKeyInfo Key);
            foreach (Frame Frame in Frame.Frames)
            {
                if (Frame.Frames[^1] == Frame && KeyPress)
                {
                    Frame.OnKeyEvent(Key);
                }
                Frame.OnDrawEvent(Canvas);
            }

            // Draw Cursor And Update The Screen
            Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Assets.Cursor);
            Canvas.CopyTo((uint*)VBE.getLfbOffset());
        }

        public static void Shutdown()
        {
            // Try VBOX Method
            IOPort P = new(0x4004);
            P.DWord = 0x3400;

            // Try QEMU Method
            Power.QemuShutdown();

            // Try Normal Method
            Power.Shutdown();
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
                Cosmos.HAL.Debug.Serial.SendString($"[WARN] Unable to play audio! ({E.Message})");
            }
        }

        public static bool TryReadKey(out ConsoleKeyInfo Key)
        {
            if (KeyboardManager.TryReadKey(out var KeyX))
            {
                Key = new(KeyX.KeyChar, (ConsoleKey)KeyX.Key, KeyX.Modifiers == ConsoleModifiers.Shift, KeyX.Modifiers == ConsoleModifiers.Alt, KeyX.Modifiers == ConsoleModifiers.Control);
                return true;
            }

            Key = default;
            return false;
        }
    }
}