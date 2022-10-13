using Cosmos.System.Network.Config;
using Cosmos.Core;
using PrismTools;
using PrismGL2D;

namespace PrismOS.Extentions
{
	public class Overlay
	{
		public Overlay(bool ShowNetworkInfo = true, bool ShowMemoryInfo = true, bool ShowVideoInfo = true)
		{
			this.ShowNetworkInfo = ShowNetworkInfo;
			this.ShowMemoryInfo = ShowMemoryInfo;
			this.ShowVideoInfo = ShowVideoInfo;
		}

		public bool ShowNetworkInfo;
		public bool ShowMemoryInfo;
		public bool ShowVideoInfo;

		private static void DrawOverlay(string Text, ref int TY, Graphics G)
		{
			G.DrawFilledRectangle(0, TY, 192, Font.Fallback.Size + 5, 0, Color.LightBlack);
			G.DrawString(5, TY, Text, Font.Fallback, Color.White);
			TY += (int)Font.Fallback.Size;
		}
		public void OnUpdate(Graphics G)
		{
			int TY = 0;
			if (ShowNetworkInfo)
			{
				DrawOverlay($"Netowrk [{NetworkConfiguration.CurrentNetworkConfig.Device.Name}]", ref TY, G);
			}
			if (ShowMemoryInfo)
			{
				uint Total = CPU.GetAmountOfRAM();
				uint Used = GCImplementation.GetUsedRAM() / 1048576;

				DrawOverlay($"Memory [{Used}/{Total}, MB]", ref TY, G);
			}
			if (ShowVideoInfo)
			{
				DrawOverlay($"Video [{Kernel.Canvas.Width}x{Kernel.Canvas.Height}@32]", ref TY, G);
				DrawOverlay($"FPS [{Kernel.Canvas.GetFPS()}]", ref TY, G);
			}
		}
	}
}