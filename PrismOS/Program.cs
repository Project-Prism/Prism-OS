using PrismAPI.Runtime.SystemCall;
using PrismAPI.Filesystem;
using PrismOS.Services;
using PrismAPI.Network;
using PrismAPI.Audio;
using Cosmos.System;

namespace PrismOS;

/*
// TO-DO: raycaster engine.
// TO-DO: Fix gradient's MaskAlpha method. (?)
// TO-DO: Move 3D engine to be shader based for all transformations.
*/
public unsafe class Program : Kernel
{
	#region Methods

	/// <summary>
	/// A method called once when the kernel boots, Used to initialize the system.
	/// </summary>
	protected override void BeforeRun()
	{
		// Initialize the screen service.
		Screen = new();

		// Initialize system services.
		FilesystemManager.Init();
		NetworkManager.Init();
		Handler.Init();

		// Disable the screen PIT timer.
		Screen.EnableTicks = false;

		AudioPlayer.Play(Media.Startup);
	}

	/// <summary>
	/// A method called repeatedly until the kernel stops.
	/// </summary>
	protected override void Run()
	{
		// Run screen service without PIT timer.
		Screen?.Tick();
	}

	#endregion

	#region Fields

	public static Screen? Screen;

	#endregion
}