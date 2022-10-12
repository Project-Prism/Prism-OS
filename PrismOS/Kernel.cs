using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio;
using Cosmos.System;
using System;
using PrismGL2D.Extentions;
using PrismOS.Extentions;
using PrismGL2D.Formats;
using PrismUI;

namespace PrismOS
{
	public unsafe class Kernel : Cosmos.System.Kernel
	{
		public static AudioMixer Mixer = new();
		public static VBECanvas Canvas = new();
		public static Overlay Overlay = new();

		protected override void BeforeRun()
		{
			#region Splash Screen

			Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash256);
			Canvas.Update();
			//Play(Assets.Vista);

			#endregion

			#region Networking

			_ = new DHCPClient().SendDiscoverPacket();

			#endregion

			#region Desktop

			_ = new Frame("Frame Testing")
			{
				Controls = new()
				{
					new Button()
					{
						X = 50,
						Y = (int)Control.Config.Scale + 50,
						Width = 200,
						Height = 25,
						Text = "Click me!",
					},
				}
			};
			//_ = new Snake();

			#endregion

			#region Misc

			Assets.Wallpaper = (Bitmap)Assets.Wallpaper.Scale(Canvas.Width, Canvas.Height);
			MouseManager.ScreenWidth = Canvas.Width;
			MouseManager.ScreenHeight = Canvas.Height;

			#endregion
		}
		protected override void Run()
		{
			// Draw wallpaper
			Canvas.DrawImage(0, 0, Assets.Wallpaper, false);

			bool KeyPress = TryReadKey(out ConsoleKeyInfo Key);
			foreach (Frame Frame in Frame.Frames)
			{
				if (Frame.Frames[^1] == Frame && KeyPress)
				{
					Frame.OnKeyEvent(Key);
				}
				Frame.OnDrawEvent(Canvas);
			}
			Overlay.OnUpdate(Canvas);

			// Draw Cursor And Update The Screen
			Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Assets.Cursor);
			Canvas.Update();
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
	}
}