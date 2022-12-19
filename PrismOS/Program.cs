using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using PrismRuntime.SShell;
using PrismTools.Events;
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
			Debugger = new("Kernel");
			System.Console.Clear();

			System.Console.WriteLine(@"    ____       _                   ____  _____");
			System.Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
			System.Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
			System.Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
			System.Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
			System.Console.WriteLine("CopyLeft PrismProject 2022. Licensed with GPL2.\n");

			NetworkManager.Init();
			SystemCalls.Init();
			//VBEConsole.Init();

			try
			{
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Log("Initialized filesystem!", Debugger.Severity.Ok);
			}
			catch
			{
				Debugger.Log("Unable to initialize filesystem!", Debugger.Severity.Warning);
			}

			Debugger.Log("Kernel initialized!", Debugger.Severity.Ok);
			AudioPlayer.Play(Assets.Vista);
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