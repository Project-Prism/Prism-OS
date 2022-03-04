using System.Drawing;
using Canvas = PrismOS.Graphics.Canvas;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas Canvas = new(1920, 1080);

            try
            {
                while (true)
                {
                    Canvas.Clear(Color.Green);
                    Tests.SineWaveVisualiser.Tick(Canvas);
                    Canvas.DrawString(15, 60, Canvas.Width + "x" + Canvas.Height + " (" + Canvas.FPS + " FPS)", Color.White);
                    Canvas.DrawBitmap((int)Mouse.X, (int)Mouse.Y, Files.Resources.Cursor);
                    Canvas.Update();
                }
            }
            catch { }
        }
    }
}