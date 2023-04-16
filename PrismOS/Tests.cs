using PrismGraphics.Extentions.VMWare;
using PrismGraphics.UI.Controls;
using PrismGraphics.Rasterizer;
using Cosmos.Core.Memory;
using PrismGraphics;
using Cosmos.System;
using Cosmos.Core;

namespace PrismOS
{
    /// <summary>
    /// The testing class. Used to test prism's features for unwanted bugs.
    /// </summary>
    public static class Tests
    {
        /// <summary>
        /// Only run when the class is accessed, memory is not used when no tests are ran.
        /// </summary>
        static Tests()
        {
            Canvas = new(800, 600);
            Gradient = new(256, 256, new Color[] { Color.Red, Color.DeepOrange, Color.Yellow, Color.Green, Color.Blue, Color.UbuntuPurple });
            Buffer = new(128, 64);
            Engine = new(800, 600, 75);
            Button1 = new(75, 15, 64, 16, 4, "Button1", () => { });

            Engine.Objects.Add(Mesh.GetCube(200, 200, 200));
            Engine.Camera.Position.Z = 200;
            Button1.Render();

            MouseManager.ScreenHeight = Canvas.Height;
            MouseManager.ScreenWidth = Canvas.Width;
        }

        #region Methods

        /// <summary>
        /// A method designed to test for memory leaks.
        /// </summary>
        public static void TestGraphics()
        {
            // Swap width and height evert 1.5 seconds.
            //Timer T = new((O) => { (Buffer.Width, Buffer.Height) = (Buffer.Height, Buffer.Width); Buffer.Clear(Color.ClassicBlue); }, null, 1500, 0);

            while (true)
            {
                Engine.Objects[^1].TestLogic(0.01f);
                Engine.Render();

                Canvas.Clear();
                Canvas.DrawString(15, 15, Canvas.GetFPS() + " FPS", default, Color.White);
                Canvas.DrawString(15, 30, GCImplementation.GetUsedRAM() / 1024 + " K", default, Color.White);
                Canvas.DrawImage(15, 50, Buffer, false);
                Canvas.DrawImage(15, 75, Engine, false);
                Canvas.DrawImage(Button1.X, Button1.Y, Button1.MainImage);
                Canvas.DrawImage(200, 15, Gradient, false);
                Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White);
                Canvas.Update();

                if (MemoryN++ == 3)
                {
                    Heap.Collect();
                    MemoryN = 0;
                }
            }
        }

        #endregion

        #region Fields

        private static readonly SVGAIICanvas Canvas;
        private static readonly Gradient Gradient;
        private static readonly Graphics Buffer;
        private static readonly Button Button1;
        private static readonly Engine Engine;
        private static int MemoryN;

        #endregion
    }
}