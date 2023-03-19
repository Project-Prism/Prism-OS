using PrismGraphics.Extentions.VMWare;
using PrismGraphics.Rasterizer;
using PrismGraphics.Animation;
using PrismGraphics;

namespace PrismOS.Tests
{
	/// <summary>
	/// The graphics testing class. Used to test the graphics features in prism for bugs.
	/// </summary>
	public static class GraphicsTests
	{
		/// <summary>
		/// Only run when the class is accessed, memory is not used when no tests are ran.
		/// </summary>
		static GraphicsTests()
		{
			Canvas = new(800, 600);
			Buffer = new(128, 64);
			Engine = new(800, 600, 75);
			Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
			Engine.Camera.Position.Z = 200;
		}

		#region Methods

		/// <summary>
		/// A method to test gradients.
		/// </summary>
		public static void TestGradients()
		{
			Gradient G = new(256, 256, new Color[] { Color.Red, Color.DeepOrange, Color.Yellow, Color.Green, Color.Blue, Color.LightPurple });

			while (true)
			{
				Canvas.Clear(Color.CoolGreen);
				
				for (int X = 0; X < G.Width; X++)
				{
					for (int Y = 0; Y < G.Height; Y++)
					{
						Canvas[50 + X, 50 + Y] = Common.Lerp(G[X, Y], Color.White, X / G.Width);
					}
				}

				Canvas.Update();
			}
		}

		/// <summary>
		/// A method to test the realocating uppon changing buffer sizes. No memory should be leaked.
		/// </summary>
		public static void TestResize()
		{
			// Swap width and height evert 1.5 seconds.
			Timer T = new((object? O) => { (Buffer.Width, Buffer.Height) = (Buffer.Height, Buffer.Width); }, null, 1500, 0);
			
			while (true)
			{
				Canvas.Clear(Color.CoolGreen);
				Buffer.Clear();
				Buffer.DrawString(0, 0, $"Width: {Buffer.Width}\nHeight: {Buffer.Height}", default, Color.White);
				Canvas.DrawImage(0, 0, Buffer, false);
				Canvas.Update();
			}
		}

		/// <summary>
		/// A method than renders a 3D scene on screen. Will not return!
		/// </summary>
		public static void Test3D()
		{
			while (true)
			{
				Engine.Objects[^1].TestLogic(0.01f);
				Engine.Render();

				Canvas.DrawImage(0, 0, Engine, false);
				Canvas.DrawString(15, 15, $"{Canvas.GetFPS()} FPS", default, Color.White);
				Canvas.Update();
			}
		}

		#endregion

		#region Fields

		private static readonly SVGAIICanvas Canvas;
		private static readonly Graphics Buffer;
		private static readonly Engine Engine;

		#endregion
	}
}