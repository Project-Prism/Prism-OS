using PrismGL2D.Extentions;
using PrismGL3D.Objects;
using PrismGL3D;
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
			Canvas = new();
			Overlay = new();

			System.Console.Clear();
			Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash256);
			Canvas.Update();
			AudioPlayer.Play(Assets.Vista);

			Engine = new(200, 200, 90);
			Cube = new(300, 300, 100);

			Engine.Objects.Add(Cube);

			//_ = new Test3D();
			//_ = new Snake();
			Debugger.Log("Initialized desktop", Debugger.Severity.Ok);
		}

		public Debugger Debugger;
		public VBECanvas Canvas;
		public Overlay Overlay;
		public Engine Engine;
		public Cube Cube;

		public void Update()
		{
			Canvas.DrawImage(0, 0, Assets.Wallpaper, false);

			Cube.TestLogic(0.01);
			Engine.Render();
			Canvas.DrawImage(200, 200, Engine);

			foreach (Frame F in Frame.Frames)
			{
				F.Update(Canvas);
			}
			Overlay.OnUpdate(Canvas);

			// Draw Cursor And Update The Screen
			Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Assets.Cursor);
			Canvas.Update();
		}
	}
}