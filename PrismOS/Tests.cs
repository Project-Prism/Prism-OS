using PrismGraphics.UI.Controls;
using PrismGraphics.Extentions;
using PrismGraphics.Rasterizer;
using Cosmos.Core.Memory;
using PrismGraphics;
using Cosmos.System;
using Cosmos.Core;

namespace PrismOS;

/// <summary>
/// The testing class. Used to test prism's features for unwanted bugs.
/// </summary>
public static class Tests
{
	/// <summary>
	/// A method designed to test for memory leaks.
	/// </summary>
	public static void TestGraphics()
	{
		// Swap width and height evert 1.5 seconds.
		//Timer T = new((O) => { (Buffer.Width, Buffer.Height) = (Buffer.Height, Buffer.Width); Buffer.Clear(Color.ClassicBlue); }, null, 1500, 0);

		Graphics GradientBuffer = Gradient.GetGradient(256, 256, new Color[]
		{
			Color.Red,
			Color.DeepOrange,
			Color.Yellow,
			Color.Green,
			Color.Blue,
			Color.UbuntuPurple
		});

		Display Canvas = Display.GetDisplay(800, 600);
		Graphics Buffer = new(128, 64);
		Button Button1 = new(75, 15, 64, 16, 4, "Button1", () => { });
		Engine Engine = new(800, 600, 75);
		int MemoryN = 0;

		Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
		Engine.Camera.Position.Z = 200;
		Button1.Render();

		MouseManager.ScreenHeight = Canvas.Height;
		MouseManager.ScreenWidth = Canvas.Width;

		while (true)
		{
			Engine.Objects[^1].TestLogic(0.01f);
			Engine.Render();

			Canvas.Clear();
			Canvas.DrawString(15, 15, $"{Canvas.GetFPS()} FPS\n{Canvas.GetName()}", default, Color.White);
			Canvas.DrawString(15, 30, GCImplementation.GetUsedRAM() / 1024 + " K", default, Color.White);
			Canvas.DrawImage(15, 50, Buffer, false);
			Canvas.DrawImage(15, 75, Engine, false);
			Canvas.DrawImage(Button1.X, Button1.Y, Button1.MainImage);
			Canvas.DrawImage(200, 15, GradientBuffer, false);
			Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White);
			Canvas.Update();

			if (MemoryN++ == 3)
			{
				Heap.Collect();
				MemoryN = 0;
			}
		}
	}
}