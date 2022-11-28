using PrismUI.Controls.Buttons;
using Cosmos.System.Audio.IO;
using PrismGL2D.Extentions;
using PrismUI.Controls;
using Cosmos.System;
using PrismAudio;
using PrismTools;
using PrismGL2D;

namespace PrismUI.UX
{
	public class Desktop
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Desktop"/> class.
		/// </summary>
		public Desktop(Image Wallpaper, Image Splash, Image Cursor, MemoryAudioStream Chime)
		{
			Debugger = new("Desktop");
			System.Console.Clear();

			#region Initial setup

			Canvas = new();
			this.Wallpaper = Wallpaper.Scale(Canvas.Width, Canvas.Height);
			MouseManager.ScreenHeight = Canvas.Height;
			MouseManager.ScreenWidth = Canvas.Width;
			this.Cursor = Cursor;
			Debugger.Log("Initialized desktop drawing.", Debugger.Severity.Ok);

			#endregion

			#region Splash Screen

			Splash = (Image)Splash.Scale(Canvas.Height / 3, Canvas.Height / 3);
			Canvas.DrawImage((int)((Canvas.Width / 2) - (Splash.Height / 2)), (int)((Canvas.Height / 2) - Splash.Height / 2), Splash);
			Canvas.Update();
			AudioPlayer.Play(Chime);

			#endregion

			#region Launcher

			Launcher = new(Canvas.Width, Control.Config.Scale, "PrismUI.UX.Launcher", false)
			{
				Y = (int)(Canvas.Height - Control.Config.Scale),
				X = 0,
				NeedsFront = false,
				HasBorder = false,
				CanType = false,
			};
			Debugger.Log("Initialized launcher.", Debugger.Severity.Ok);

			#endregion
		}

		#region Methods

		public void Install(string Label, Action A)
		{
			Launcher.Controls.Add(new Button(Label)
			{
				X = (int)(Launcher.Controls.Count == 0 ? 0 : Launcher.Controls[^1].X + Launcher.Controls[^1].Width),
				OnClickEvent = (int X, int Y, MouseState State) => { A(); },
			});
		}
		public void Update()
		{
			Canvas.DrawImage(0, 0, Wallpaper, false);

			bool CallKeyEvent = KeyboardEx.TryReadKey(out ConsoleKeyInfo Key);
			foreach (Window F in WindowManager.Windows)
			{
				if (!F.IsEnabled)
				{
					continue;
				}

				if (CallKeyEvent && WindowManager.Windows[^1] == F && F.CanType)
				{
					F.OnKey(Key);
				}
				F.OnDraw(Canvas);
			}
			Status.DrawStatus(Canvas);

			// Draw Cursor And Update The Screen
			Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Cursor);
			Canvas.Update();
		}

		#endregion

		#region Fields

		public Graphics Wallpaper;
		public Debugger Debugger;
		public VBECanvas Canvas;
		public Window Launcher;
		public Image Cursor;

		#endregion
	}
}