using PrismGL2D.Extentions;
using PrismUI.Controls;
using Cosmos.System;
using PrismAudio;
using PrismTools;
using PrismGL2D;
using PrismUI;

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
			Wallpaper = Assets.Wallpaper.Scale(Canvas.Width, Canvas.Height);
			MouseManager.ScreenWidth = Canvas.Width;
			MouseManager.ScreenHeight = Canvas.Height;
			Debugger.Log("Initialized desktop drawing", Debugger.Severity.Ok);

			#endregion

			#region Splash Screen

			Splash = (Image)Assets.Splash.Scale(Canvas.Height / 3, Canvas.Height / 3);
			Canvas.DrawImage((int)((Canvas.Width / 2) - (Splash.Height / 2)), (int)((Canvas.Height / 2) - Splash.Height / 2), Splash);
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
				CanType = false,
			};
			Launcher.Controls.RemoveAt(0); // Remove close button
			Install("Shutdown", () => Power.Shutdown());
			Install("Explorer", () => _ = new Explorer());
			Install("GFXTest", () => _ = new GFXTest());
			Install("TickTackToe", () => _ = new TickTackToe());
			Install("Physics", () => _ = new Physics());
			Debugger.Log("Initialized launcher", Debugger.Severity.Ok);

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

			foreach (Window F in Window.Frames)
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
		public Window Launcher;
		public Image Splash;
		public Window Menu;

		#endregion
	}
}