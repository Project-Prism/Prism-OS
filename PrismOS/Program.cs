using PrismRuntime.SShell;
using PrismFilesystem;
using Cosmos.System;
using PrismRuntime;
using PrismNetwork;
using PrismAudio;
using PrismTools;

namespace PrismOS;

/*
// TO-DO: raycaster engine.
// TO-DO: Fix gradient's MaskAlpha method. (?)
// TO-DO: Move 3D engine to be shader based for all transformations.
*/
public unsafe class Program : Kernel
{
	/// <summary>
	/// A method called once when the kernel boots, Used to initialize the system.
	/// </summary>
	protected override void BeforeRun()
	{
		// Clear console & display ascii art.
		System.Console.Clear();
		System.Console.WriteLine(@"    ____       _                   ____  _____");
		System.Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
		System.Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
		System.Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
		System.Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
		System.Console.WriteLine("CopyLeft PrismProject 2023. Licensed with GPL2.\n");

		// Initialize system services.
		FilesystemManager.Init();
		NetworkManager.Init();
		SystemCalls.Init();

		AudioPlayer.Play(Media.Startup);
	}

	/// <summary>
	/// A method called repeatedly until the kernel stops.
	/// </summary>
	protected override void Run()
	{
		// Hold any key for graphics test.
		if (KeyboardEx.TryReadKey(out _))
		{
			Tests.TestGraphics();
		}

		Shell.Main();
	}
}