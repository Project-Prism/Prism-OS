using PrismGL2D.Extentions;
using PrismGL2D.Formats;
using PrismGL2D;
using Cosmos.System;
using PrismAudio;
using PrismTools;
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

			#region Apps

			_ = new Explorer();
			_ = new Test3D();
			// _ = new Snake();
			Debugger.Log("Initialized startup apps", Debugger.Severity.Ok);

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

		#endregion
	}
}