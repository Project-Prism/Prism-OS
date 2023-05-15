using PrismAPI.Graphics.Rasterizer;
using PrismAPI.Hardware.GPU;
using PrismAPI.UI.Controls;
using Cosmos.Core.Memory;
using PrismAPI.Graphics;
using PrismAPI.Tools;
using Cosmos.System;
using Cosmos.Core;
using PrismAPI.UI;

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
		Gradient GradientBuffer = new(256, 256, new Color[]
		{
			Color.Red,
			Color.DeepOrange,
			Color.Yellow,
			Color.Green,
			Color.Blue,
			Color.UbuntuPurple
		});

		Display Canvas = Display.GetDisplay(800, 600);
		Canvas Buffer = new(128, 64);
		Engine Engine = new(800, 600, 75);

		// Swap width and height evert 1.5 seconds.
		Timer T1 = new((O) => { (Buffer.Width, Buffer.Height) = (Buffer.Height, Buffer.Width); Buffer.Clear(Color.ClassicBlue); }, null, 1500, 0);
		
		Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
		Engine.Camera.Position.Z = 200;
		//WindowManager.Windows.Add(new(100, 100, 250, 150));
		//WindowManager.Windows[^1].Controls.Add(new Button(50, 50, 128, 64, 4, "Button1"));

		MouseManager.ScreenHeight = Canvas.Height;
		MouseManager.ScreenWidth = Canvas.Width;

		while (true)
		{
			Engine.Objects[^1].TestLogic(0.01f);
			Engine.Render();

			string Info = $"{Canvas.GetFPS()} FPS\n{Canvas.GetName()}\n{ByteFormatter.GetMegaBytes(GCImplementation.GetUsedRAM())} MB";

			Canvas.Clear();
			Canvas.DrawImage(150, 150, Buffer, false);
			Canvas.DrawImage(15, 75, Engine, false);
			Canvas.DrawImage(200, 15, GradientBuffer, false);
			Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White);
			Canvas.DrawString(15, 15, Info, default, Color.White);
			GCImplementation.Free(Info);
			//WindowManager.Update(Canvas);
			Canvas.Update();
			//Heap.Collect();
		}
	}
}