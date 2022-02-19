using System.Drawing;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            while (true)
            {
                Graphics.Canvas Canvas = new(1280, 720);

                while (true)
                {
                    Canvas.Clear();
                    Canvas.DrawCube(1.0f, Color.White);
                    Canvas.Update();
                }
            }
        }
    }
}