using PrismAPI.Runtime.SystemCall;
using PrismAPI.Tools.Extentions;
using PrismAPI.Hardware.GPU;
using PrismAPI.UI.Controls;
using PrismAPI.Filesystem;
using Cosmos.Core.Memory;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using PrismAPI.Network;
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

		Boot.Show(Canvas);

		// Initialize the FPS widget.
		FPSWidget = new(15, 15, "Initializing...");

		// Scale the boot slash image.
		Media.Prism = Filters.Scale((ushort)(Canvas.Height / 3), (ushort)(Canvas.Height / 3), Media.Prism);

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
		Boot.Hide();

		AudioPlayer.Play(Media.Startup);
	}

	/// <summary>
	/// A method called repeatedly until the kernel stops.
	/// </summary>
	protected override void Run()
	{
		Canvas.Clear(Color.CoolGreen);
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

	#endregion
}