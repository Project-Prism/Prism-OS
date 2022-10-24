using Cosmos.System;
using PrismAudio;
using PrismGL2D;
using PrismGL2D.Extentions;
using PrismGL2D.Formats;
using PrismTools;
using PrismUI.Controls;

namespace PrismOS.Apps
{
	public class Desktop
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Desktop"/> class.
		/// </summary>
		public Desktop()
		{
			Debugger = new("Desktop");
			System.Console.Clear();

			#region Initial setup

			Canvas = new();
			Overlay = new();
			Wallpaper = (Bitmap)Assets.Wallpaper.Scale(Canvas.Width, Canvas.Height);
			MouseManager.ScreenWidth = Canvas.Width;
			MouseManager.ScreenHeight = Canvas.Height;
			Debugger.Log("Initialized desktop drawing", Debugger.Severity.Ok);

			#endregion

			#region Splash Screen

			Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash);
			Canvas.Update();
			AudioPlayer.Play(Assets.Vista);

			#endregion

			#region Launcher

			Launcher = new(Canvas.Width, Control.Config.Scale, Launcher.GetType().FullName)
			{
				Y = (int)(Canvas.Height - Control.Config.Scale),
				NeedsFront = false,
				HasBorder = false,
				CanDrag = false,
			};
			Launcher.Controls.Add(new Button(Control.Config.Scale, Control.Config.Scale)
			{
				Text = "SD",
				OnClickEvent = (int X, int Y, MouseState State) => { Power.Shutdown(); },
			});
			Launcher.Controls.Add(new Button(Control.Config.Scale, Control.Config.Scale)
			{
				X = (int)Control.Config.Scale,
				Text = "/",
				OnClickEvent = (int X, int Y, MouseState State) => { _ = new Explorer(); },
			});
			Launcher.Controls.Add(new Button(Control.Config.Scale, Control.Config.Scale)
			{
				X = (int)Control.Config.Scale * 2,
				Text = "3D",
				OnClickEvent = (int X, int Y, MouseState State) => { _ = new Test3D(); },
			});
			Launcher.Controls.Add(new Button(Control.Config.Scale, Control.Config.Scale)
			{
				X = (int)Control.Config.Scale * 3,
				Text = "OX",
				OnClickEvent = (int X, int Y, MouseState State) => { _ = new TTT(); },
			});
			Launcher.Controls.RemoveAt(0); // Remove close button
			Debugger.Log("Initialized launcher", Debugger.Severity.Ok);

			#endregion
		}

		#region Methods

		public void Update()
		{
			Canvas.DrawImage(0, 0, Wallpaper, false);

			foreach (Frame F in Frame.Frames)
			{
				if (F.IsEnabled)
				{
					F.Update(Canvas);
				}
			}
			Overlay.OnUpdate(Canvas);

			// Draw Cursor And Update The Screen
			Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Assets.Cursor);
			Canvas.Update();
		}

		#endregion

		#region Fields

		public Graphics Wallpaper;
		public Debugger Debugger;
		public VBECanvas Canvas;
		public Overlay Overlay;
		public Frame Launcher;

		#endregion
	}
}