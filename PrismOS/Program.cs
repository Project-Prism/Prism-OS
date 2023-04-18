using PrismRuntime.SShell;
using PrismFilesystem;
using Cosmos.System;
using PrismRuntime;
using PrismNetwork;
using PrismAudio;
using PrismGraphics.Extentions.VMWare;
using PrismGraphics;
using System.Numerics;

namespace PrismOS
{
	/*
	// TO-DO: raycaster engine.
	// TO-DO: Fix gradient's MaskAlpha method. (?)
	// TO-DO: Move 3D engine to be shader based for all transformations.
	*/
	public unsafe class Program : Kernel
	{
		/// <summary>
		/// A method called once when the kernel boots, Used to initialize the system.
		/// </summary>
		protected override void BeforeRun()
		{
			// Clear console & display ascii art.
			System.Console.Clear();
			System.Console.WriteLine(@"    ____       _                   ____  _____");
			System.Console.WriteLine(@"   / __ \_____(_)________ ___     / __ \/ ___/");
			System.Console.WriteLine(@"  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ ");
			System.Console.WriteLine(@" / ____/ /  / (__  ) / / / / /  / /_/ /___/ / ");
			System.Console.WriteLine(@"/_/   /_/  /_/____/_/ /_/ /_/   \____//____/  ");
			System.Console.WriteLine("CopyLeft PrismProject 2023. Licensed with GPL2.\n");

			// Initialize system services.
			FilesystemManager.Init();
			NetworkManager.Init();
			SystemCalls.Init();

			AudioPlayer.Play(Media.Startup);
		}

		/// <summary>
		/// A method called repeatedly until the kernel stops.
		/// </summary>
		protected override void Run()
		{
			// Hold any key for graphics test.
			if (PrismTools.KeyboardEx.TryReadKey(out _))
			{
				Tests.TestGraphics();
			}

			WADFile R = new(Media.Doom);

			List<Vector2> PointDefs = Map.Remap(R.ReadVertexes("VERTEXES"), 800, 600);
			List<LineDef> LineDefs = R.ReadLines("LINEDEFS");

			SVGAIICanvas C = new(800, 600);

			foreach (var L in LineDefs)
			{
				C.DrawLine((int)PointDefs[L.StartVertex].X, (int)PointDefs[L.StartVertex].Y, (int)PointDefs[L.EndVertex].X, (int)PointDefs[L.EndVertex].Y, Color.White);
				C.Update();
			}

			while (true) { }

			Shell.Main();
		}
	}
}