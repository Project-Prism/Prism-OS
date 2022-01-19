using PrismOS.UI;
using System.Drawing;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Storage.VFS.InitVFS();
            Canvas Canvas = new(1280, 720, true);

            while (true)
            {
                Canvas.Clear(Color.Black);
                Canvas.DrawFilledRectangle(50, 50, 100, 100, Color.White);
                Canvas.DrawFilledCircle(500, 500, 20, Color.Red);
                Canvas.DrawTriangle(200, 200, 200, 100, 100, 100, Color.Green);
                Canvas.Update();
            }
        }
    }
}