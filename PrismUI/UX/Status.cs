using Cosmos.System.Network.Config;
using PrismGL2D.Extentions;
using Cosmos.Core;
using PrismGL2D;

namespace PrismUI.UX
{
	/// <summary>
	/// Overlay class, used for displaying debug information.
	/// </summary>
	public static class Status
	{
		private static void DrawOverlay(string Text, ref uint TY, Graphics G)
		{
			G.DrawFilledRectangle(0, 0 + (int)TY, 192, Font.Fallback.Size + 5, 0, Color.LightBlack);
			G.DrawString(0, 0 + (int)TY + 5, Text, Font.Fallback, Color.White);
			TY += Font.Fallback.Size + 5;
		}
		public static void DrawStatus(Graphics G)
		{
			uint TY = 0;
			DrawOverlay($"Netowrk [{NetworkConfiguration.CurrentNetworkConfig.Device.Name}]", ref TY, G);

			uint Total = CPU.GetAmountOfRAM();
			uint Used = GCImplementation.GetUsedRAM() / 1048576;

			DrawOverlay($"Memory [{Used}/{Total}, MB]", ref TY, G);

			DrawOverlay($"Video [{G.Width}x{G.Height}@32]", ref TY, G);
			DrawOverlay($"FPS [{((VBECanvas)G).GetFPS()}]", ref TY, G);
		}
	}
}