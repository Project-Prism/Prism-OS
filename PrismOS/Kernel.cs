using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Audio;
using Cosmos.System;
using PrismGL2D.Extentions;
using PrismOS.Extentions;
using PrismGL2D.Formats;
using PrismAudio;
using PrismTools;
using PrismUI;

namespace PrismOS
{
	public unsafe class Kernel : Cosmos.System.Kernel
	{
		public static Debugger Debugger = new("Kernel");
		public static AudioMixer Mixer = new();
		public static VBECanvas Canvas = new();
		public static Overlay Overlay = new();

		protected override void BeforeRun()
		{
			#region Splash Screen

			System.Console.Clear();
			Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash256);
			Canvas.Update();
			AudioPlayer.Play(Assets.Vista);
			Debugger.Log("Initialized boot screen", Debugger.Severity.Ok);

			#endregion

			#region Networking

			try
			{
				_ = new DHCPClient().SendDiscoverPacket();
				Debugger.Log("Initialized networking", Debugger.Severity.Ok);
			}
			catch
			{
				Debugger.Log("Unable to initialize networking!", Debugger.Severity.Warning);
			}

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
			Debugger.Log("Initialized desktop", Debugger.Severity.Ok);

			#endregion

			#region Misc

			Assets.Wallpaper = (Bitmap)Assets.Wallpaper.Scale(Canvas.Width, Canvas.Height);
			MouseManager.ScreenWidth = Canvas.Width;
			MouseManager.ScreenHeight = Canvas.Height;
			Debugger.Log("Initialized boot resources", Debugger.Severity.Ok);

			#endregion
		}
		protected override void Run()
		{
			// Draw wallpaper
			Canvas.DrawImage(0, 0, Assets.Wallpaper, false);

			bool KeyPress = Keyboard.TryReadKey(out ConsoleKeyInfo Key);
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
	}
}