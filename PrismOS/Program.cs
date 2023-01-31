using PrismGraphics.Extentions;
using PrismRuntime.SShell;
using PrismTools.Events;
using PrismFilesystem;
using Cosmos.System;
using PrismRuntime;
using PrismNetwork;
using PrismAudio;
using PrismTools;

namespace PrismOS
{
	/*
	// TO-DO: General cleaning
	// TO-DO: fix gradients
	// TO-DO: raycaster engine
	*/
	public unsafe class Program : Kernel
	{
		protected override void BeforeRun()
		{
			// Initialize debugger.
			Debugger = new("Kernel");

			// Clear console & display ascii art.
			System.Console.Clear();
			System.Console.WriteLine(@"    ____       _                   ____  _____");
			System.Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
			System.Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
			System.Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
			System.Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
			System.Console.WriteLine("CopyLeft PrismProject 2022. Licensed with GPL2.\n");

			// Initialize system services.
			FilesystemManager.Init();
			NetworkManager.Init();
			SystemCalls.Init();
			VBEConsole.Init();

			AudioPlayer.Play(Assets.Vista);
			Debugger.Log("Kernel initialized!", Debugger.Severity.Ok);
		}

		protected override void Run()
		{
			EventService.OnTick();
			Shell.Main();
		}

		#region Fields

		public static Debugger Debugger;

		#endregion
	}
}