using PrismAPI.Graphics.Rasterizer;
using PrismAPI.Graphics.Animation;
using PrismAPI.Tools.Extentions;
using PrismAPI.Hardware.GPU;
using PrismAPI.UI.Controls;
using PrismAPI.Filesystem;
using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;
using Cosmos.Core;
using PrismAPI.UI;

namespace PrismOS.Services;

public class Screen : Service
{
	#region Constructors

	public Screen() : base()
	{
		EnableTicks = true;
		Canvas = Display.GetDisplay(1920, 1080);

		A = new(25f, 270f, new(0, 0, 0, 0, 750), AnimationMode.Ease);
		B = new(0f, 360f, new(0, 0, 0, 0, 500), AnimationMode.Linear);

		// Get the half size of the target canvas.
		H3 = (ushort)(Canvas.Height / 3);
		W3 = (ushort)(Canvas.Width / 3);
		H2 = (ushort)(Canvas.Height / 2);
		W2 = (ushort)(Canvas.Width / 2);

		Media.Prism = Filters.Scale(H3, H3, Media.Prism);

		Gradient = new(256, 256, new Color[]
		{
			Color.Red,
			Color.DeepOrange,
			Color.Yellow,
			Color.Green,
			Color.Blue,
			Color.UbuntuPurple
		});

		Engine = new(800, 600, 75);

		Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
		Engine.Camera.Position.Z = 200;
		WindowManager.Windows.Add(new(100, 100, 250, 150));
		WindowManager.Windows[^1].Controls.Add(new Button(50, 50, 128, 64, 4, "Button1")
		{
			Theme = ThemeStyle.Material,
		});

		MouseManager.ScreenHeight = Canvas.Height;
		MouseManager.ScreenWidth = Canvas.Width;
	}

	#endregion

	#region Methods

	public override void Tick()
	{
		Canvas.Clear();

		if (EnableTicks)
		{
			Canvas.DrawImage(W2 - (H3 / 2), H2 - (H3 / 2), Media.Prism, false);

			if (A.IsFinished)
			{
				(A.Source, A.Target) = (A.Target, A.Source);
				A.Reset();
			}
			if (B.IsFinished)
			{
				B.Reset();
			}

			int LengthOffset = (int)(B.Current + A.Current);
			int Offset = (int)B.Current;

			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 19, Color.LightGray, Offset, LengthOffset);
			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 20, Color.White, Offset, LengthOffset);
			Canvas.DrawArc(W2, H2 + (H2 / 2) + (H3 / 2), 21, Color.LightGray, Offset, LengthOffset);

			Canvas.Update();
			return;
		}

		Engine.Objects[^1].TestLogic(0.01f);
		Engine.Render();

		string Info = $"{Canvas.GetFPS()} FPS\n{Canvas.GetName()}\n{StringEx.GetMegaBytes(GCImplementation.GetUsedRAM())} MB";

		Canvas.DrawImage(15, 75, Engine, false);
		Canvas.DrawImage(200, 15, Gradient, false);
		Canvas.DrawString(15, 15, Info, default, Color.White);
		WindowManager.Update(Canvas);
		//Heap.Collect();

		//int ClockSize = 40;
		//Canvas.DrawFilledCircle(W2, H2, (ushort)ClockSize, Color.White);
		//Canvas.DrawAngledLine(W2, H2, (short)Coordinates.Scale(Cosmos.HAL.RTC.Second, 360, 60), (ushort)(ClockSize / 1.5), Color.Red);
		//Canvas.DrawAngledLine(W2, H2, (short)Coordinates.Scale(Cosmos.HAL.RTC.Minute, 360, 60), (ushort)(ClockSize / 2), Color.Black);
		//Canvas.DrawAngledLine(W2, H2, (short)Coordinates.Scale(Cosmos.HAL.RTC.Hour, 360, 12), (ushort)(ClockSize / 3), Color.Black);

		Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Media.Cursor);

		Canvas.Update();
	}

	#endregion

	#region Fields

	public AnimationController A;
	public AnimationController B;
	public Gradient Gradient;
	public Display Canvas;
	public Engine Engine;
	public ushort H3;
	public ushort W3;
	public ushort H2;
	public ushort W2;

	#endregion
}