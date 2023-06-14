using PrismAPI.Runtime.SystemCall;
using PrismAPI.Graphics.Animation;
using PrismAPI.Tools.Extentions;
using PrismAPI.Hardware.GPU;
using PrismAPI.UI.Controls;
using PrismAPI.Filesystem;
using Cosmos.Core.Memory;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using PrismAPI.Network;
using PrismAPI.Tools;
using PrismAPI.Audio;
using Cosmos.System;
using PrismAPI.UI;
using Cosmos.Core;

namespace PrismOS;

/*
// TO-DO: raycaster engine.
// TO-DO: Fix gradient's MaskAlpha method. (?)
// TO-DO: Move 3D engine to be shader based for all transformations.
*/
public class Program : Kernel
{
	#region Methods

	/// <summary>
	/// A method called once when the kernel boots, Used to initialize the system.
	/// </summary>
	protected override void BeforeRun()
	{
		// Initialize the display output.
		Canvas = Display.GetDisplay(1920, 1080);

		// Initialize the FPS widget.
		FPSWidget = new(15, 15, "Initializing...");

		// Initialize the animations.
		AnimationController A = new(25f, 270f, new(0, 0, 0, 0, 750), AnimationMode.Ease);
		AnimationController B = new(0f, 360f, new(0, 0, 0, 0, 500), AnimationMode.Linear);

		A.IsContinuous = true;

		// Get the half size of the target canvas.
		H3 = (ushort)(Canvas.Height / 3);
		W3 = (ushort)(Canvas.Width / 3);
		H2 = (ushort)(Canvas.Height / 2);
		W2 = (ushort)(Canvas.Width / 2);

		bool EnableTimer = true;

		// Add a timer to update the screen while booting.
		Timer T = new((_) =>
		{
			if (!EnableTimer)
			{
				return;
			}

			if (B.IsFinished)
			{
				B.Reset();
			}

			Canvas.Clear();
			Canvas.DrawImage(W2 - (H3 / 2), H2 - (H3 / 2), Media.Prism, false);

			int LengthOffset = (int)(B.Current + A.Current);
			int Offset = (int)B.Current;

			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 19, Color.LightGray, Offset, LengthOffset);
			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 20, Color.White, Offset, LengthOffset);
			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 21, Color.LightGray, Offset, LengthOffset);

			Canvas.Update();
		}, null, 55, 0);

		Media.Prism = Filters.Scale(H3, H3, Media.Prism);

		WindowManager.Windows.Add(new(100, 100, 250, 150, "Window1")
		{
			Controls =
			{
				new Button(50, 50, 128, 64, 4, "Button1", ThemeStyle.Holo),
			},
		});
		WindowManager.Widgets.Add(FPSWidget);

		MouseManager.ScreenHeight = Canvas.Height;
		MouseManager.ScreenWidth = Canvas.Width;

		// Initialize system services.
		FilesystemManager.Init();
		NetworkManager.Init();
		AudioPlayer.Init();
		Handler.Init();

		// Disable the screen timer.
		EnableTimer = false;

		AudioPlayer.Play(Media.Startup);
	}

	/// <summary>
	/// A method called repeatedly until the kernel stops.
	/// </summary>
	protected override void Run()
	{
		Canvas.Clear();
		FPSWidget.Contents = $"{Canvas.GetFPS()} FPS\n{Canvas.GetName()}\n{StringEx.GetMegaBytes(GCImplementation.GetUsedRAM())} MB";
		WindowManager.Update(Canvas);
		Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Media.Cursor);
		Canvas.Update();
		Heap.Collect();
	}

	#endregion

	#region Fields

	public static Display Canvas = null!;
	public Label FPSWidget = null!;
	public ushort H3;
	public ushort W3;
	public ushort H2;
	public ushort W2;

	#endregion
}