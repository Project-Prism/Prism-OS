using Cosmos.System.Network.Config;
using PrismGL2D.Extentions;
using Cosmos.Core;
using PrismGL2D;

namespace PrismTools
{
	/// <summary>
	/// Overlay class, used for displaying debug information.
	/// </summary>
	public class Overlay
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Overlay"/> class.
		/// </summary>
		/// <param name="ShowNetworkInfo">Wether to show network info.</param>
		/// <param name="ShowMemoryInfo">Wether to show memory info.</param>
		/// <param name="ShowVideoInfo">Wether to show video info.</param>
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
				DrawOverlay($"Video [{G.Width}x{G.Height}@32]", ref TY, G);
				DrawOverlay($"FPS [{((VBECanvas)G).GetFPS()}]", ref TY, G);
			}
		}
	}
}