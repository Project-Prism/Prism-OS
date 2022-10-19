using PrismGL2D;

namespace PrismTools
{
	public static class GraphicsTest
	{
		/// <summary>
		/// Run a basic graphics test.
		/// TO-DO: add all graphics calls to test.
		/// </summary>
		/// <param name="G">Canvas to draw on</param>
		public static void Run(Graphics G)
		{
			G.Clear("#1e1e32");
			G.DrawFilledRectangle(15, 15, 128, 32, 20, Color.LimeGreen);
			G.DrawRectangle(30, 30, 50, 20, 40, Color.Black);
			G.DrawString(15, 15, "Graphics Testing!", Font.Fallback, Color.White);
			G.DrawArc(500, 500, 20, Color.White, 45, 270);
			G.DrawFilledArc(500, 500, 10, Color.White, 45, 270);
			G.DrawCircle(500, 500, 25, Color.RubyRed);
			G.DrawTriangle(0, 0, 100, 100, 100, 15, Color.White);
			G.DrawCubicBezierLine((int)G.Width, (int)G.Height, 0, 0, (int)G.Width, 0, 0, (int)G.Height, Color.Blue);
		}
	}
}