using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Cosmos.System;
using PrismOS.Apps;
using PrismTools;
using PrismUI;

namespace PrismOS
{
	public unsafe class Program : Kernel
	{
		public static Debugger Debugger;
		public static Desktop Desktop;
		public static bool IsNETReady;
		public static bool IsFSReady;

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");
			Desktop = new();

			DialogBox.ShowMessageDialog("Welcome", "Welcome to prism OS!\nIf you are wondering...\nThe seal in the background is named shawn.");

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
		}
		protected override void Run()
		{
			Desktop.Update();
		}
	}
}