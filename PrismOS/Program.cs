using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using PrismRuntime.SShell;
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
		// TO-DO: VBE Canvas
		// TO-DO: General cleaning
		// TO-DO: fix gradients
		// TO-DO: raycaster engine
		// TO-DO: hex colors for glang
		*/

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");
			System.Console.Clear();

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
			UserEnviroment.Init();
			NetworkManager.Init();

			Debugger.Log("Kernel initialized!", Debugger.Severity.Ok);
		}
		protected override void Run()
		{
			Shell.Main();
		}

		#region Fields

		public static Debugger Debugger;
		public static bool IsFSReady;

		#endregion
	}
}