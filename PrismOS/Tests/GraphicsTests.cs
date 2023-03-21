using PrismGraphics.Extentions.VMWare;
using PrismGraphics.Rasterizer;
using PrismGraphics.Animation;
using Cosmos.Core.Memory;
using PrismUI.Controls;
using PrismGraphics;
using Cosmos.System;
using Cosmos.Core;

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
			Button1 = new(50, 50, 64, 16, 4, "Button1", () => { });
			Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
			Engine.Camera.Position.Z = 200;
			MouseManager.ScreenHeight = Canvas.Height;
			MouseManager.ScreenWidth = Canvas.Width;
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

		/// <summary>
		/// A method that renders a basic UI on screen.
		/// </summary>
		public static void TestUI()
		{
			Button1.Render();

			while (true)
			{
				string Used = GCImplementation.GetUsedRAM() / 1024 + "K used.";

				Canvas.Clear(Color.CoolGreen);
				Canvas.DrawImage(Button1.X, Button1.Y, Button1.MainImage);
				Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White);
				Canvas.DrawString(15, 15, Used, default, Color.White);
				Canvas.Update();
				Heap.Collect();
				GCImplementation.Free(Used);
			}
		}

		#endregion

		#region Fields

		private static readonly SVGAIICanvas Canvas;
		private static readonly Graphics Buffer;
		private static readonly Button Button1;
		private static readonly Engine Engine;

		#endregion
	}
}