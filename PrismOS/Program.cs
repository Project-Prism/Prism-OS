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
	public unsafe class Program : Kernel
	{
		/*
		// TO-DO: VBE Console
		// TO-DO: General cleaning
		// TO-DO: fix gradients
		// TO-DO: raycaster engine
		// TO-DO: hex colors for glang
		*/

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");
			System.Console.Clear();

			System.Console.WriteLine(@"    ____       _                   ____  _____");
			System.Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
			System.Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
			System.Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
			System.Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
			System.Console.WriteLine("CopyLeft 2022, Created by the PrismProject team.\n");

			try
			{
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Log("Initialized filesystem!", Debugger.Severity.Ok);
				IsFSReady = true;
			}
			catch
			{
				Debugger.Log("Unable to initialize filesystem!", Debugger.Severity.Warning);
			}

			AudioPlayer.Play(Assets.Vista);
			NetworkManager.Init();
			SystemCalls.Init();

			Debugger.Log("Kernel initialized!", Debugger.Severity.Ok);
		}
		protected override void Run()
		{
			EventService.OnTick();
			Shell.Main();
		}

		#region Fields

		public static Debugger Debugger;
		public static bool IsFSReady;

		#endregion
	}
}