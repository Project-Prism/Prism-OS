using PrismOS.UI;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            SaltCanvas c = new(1280, 720);
            c.Clear(new(118, 109, 49));
            c.SetPixel(0, 0, new(-1));
            c.SetPixel(0, 1, new(-1));
            c.SetPixel(1, 1, new(-1));
            c.SetPixel(1, 0, new(-1));
            //c.DrawFilledRectangle(50, 50, 50, 50, new(-248));
            int X = 0, Y = 0;

            while (true)
            {
                c.SetPixel(X, Y, new(-1));
                X++;
                Y++;
                c.Update();
            }
        }
    }
}