using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System;
using PrismGL2D.Formats;
using PrismOS.Apps;
using PrismTools;

namespace PrismOS
{
	public unsafe class Program : Kernel
	{
		public Debugger Debugger;
		public Desktop Desktop;

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");
			Desktop = new();

			try
			{
				_ = new DHCPClient().SendDiscoverPacket();
				Debugger.Log("Initialized networking", Debugger.Severity.Ok);
			}
			catch
			{
				Debugger.Log("Unable to initialize networking!", Debugger.Severity.Warning);
			}

			Assets.Wallpaper = (Bitmap)Assets.Wallpaper.Scale(Desktop.Canvas.Width, Desktop.Canvas.Height);
			MouseManager.ScreenWidth = Desktop.Canvas.Width;
			MouseManager.ScreenHeight = Desktop.Canvas.Height;
			Debugger.Log("Initialized boot resources", Debugger.Severity.Ok);
		}
		protected override void Run()
		{
			Desktop.Update();
		}
	}
}