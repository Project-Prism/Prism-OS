using Cosmos.System.Network.Config;
using PrismGL2D.Extentions;
using Cosmos.Core;
using PrismGL2D;

namespace PrismUI.Controls
{
	/// <summary>
	/// Overlay class, used for displaying debug information.
	/// </summary>
	public class Status : Control
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Status"/> class.
		/// </summary>
		/// <param name="ShowNetworkInfo">Wether to show network info.</param>
		/// <param name="ShowMemoryInfo">Wether to show memory info.</param>
		/// <param name="ShowVideoInfo">Wether to show video info.</param>
		public Status() : base(192, Font.Fallback.Size * 4)
		{
			CanInteract = false;
			CanType = false;
		}

		private void DrawOverlay(string Text, ref uint TY, Graphics G)
		{
			G.DrawFilledRectangle(X, Y + (int)TY, 192, Font.Fallback.Size + 5, 0, Color.LightBlack);
			G.DrawString(X, Y + (int)TY + 5, Text, Font.Fallback, Color.White);
			TY += Font.Fallback.Size + 5;
		}
		public void OnUpdate(Graphics G)
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