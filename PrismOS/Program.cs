using PrismAPI.Runtime.SShell;
using PrismAPI.Filesystem;
using PrismAPI.Network;
using PrismAPI.Runtime;
using PrismAPI.Audio;
using PrismAPI.Tools;

namespace PrismOS;

/*
// TO-DO: raycaster engine.
// TO-DO: Fix gradient's MaskAlpha method. (?)
// TO-DO: Move 3D engine to be shader based for all transformations.
*/
public unsafe class Program : Cosmos.System.Kernel
{
	/// <summary>
	/// A method called once when the kernel boots, Used to initialize the system.
	/// </summary>
	protected override void BeforeRun()
	{
		// Clear console & display ascii art.
		Console.Clear();
		Console.WriteLine(@"    ____       _                   ____  _____");
		Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
		Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
		Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
		Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
		Console.WriteLine("CopyLeft PrismProject 2023. Licensed with GPL2.\n");

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