using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Cosmos.System;
using PrismOS.Apps;
using PrismTools;
using PrismUI.UX;
using PrismUI;

namespace PrismOS
{
	public unsafe class Program : Kernel
	{
		/*
		// TO-DO: fix PrismGL2D.Extentions.VBEConsole
		// TO-DO: General cleaning
		// TO-DO: fix gradients
		// TO-DO: raycaster engine
		// TO-DO: hex colors for glang
		// TO-DO: ttf font loader & renderer
		// TO-DO: Analog clock
		// TO-DO: Working notepad
		// TO-DO: pasive rendering for gui
		// TO-DO: re-factoring
		*/

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");

			try
			{
				Desktop = new(Assets.Wallpaper, Assets.Splash, Assets.Cursor, Assets.Vista);
				Desktop.Install("Shutdown", () => Power.Shutdown());
				Desktop.Install("Explorer", () => _ = new Explorer());
				Desktop.Install("GFXTest", () => _ = new GFXTest());
				Desktop.Install("TickTackToe", () => _ = new TickTackToe());
				Desktop.Install("Physics", () => _ = new Physics());
			}
			catch
			{
				Debugger.Log("Unable to initialize the desktop!");
			}
			try
			{
				_ = new DHCPClient().SendDiscoverPacket();
				Debugger.Log("Initialized networking", Debugger.Severity.Ok);
				IsNETReady = true;
			}
			catch
			{
				Debugger.Log("Unable to initialize networking!", Debugger.Severity.Warning);
			}
			try
			{
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Log("Initialized filesystem", Debugger.Severity.Ok);
				IsFSReady = true;
			}
			catch
			{
				Debugger.Log("Unable to initialize filesystem!", Debugger.Severity.Warning);
			}

			DialogBox.ShowMessageDialog("Welcome", "Welcome to prism OS!\nIf you are wondering...\nThe seal in the background is named shawn.");
			Debugger.Log("Kernel initialized", Debugger.Severity.Ok);
		}
		protected override void Run()
		{
			Desktop.Update();
		}

		#region Fields

		public static Debugger Debugger;
		public static Desktop Desktop;
		public static bool IsNETReady;
		public static bool IsFSReady;

		#endregion
	}
}