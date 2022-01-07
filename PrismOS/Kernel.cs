using PrismOS.UI;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            SaltCanvas c = new(1280, 720);

            int R = 0;
            int G = 0;
            int B = 0;

            while (true)
            {
                for (; R < 255; R++)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }
                for (; G < 255; G++)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }
                for (; B < 255; B++)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }

                for (; R > 0; R--)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }
                for (; G > 0; G--)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }
                for (; R < 255; R++)
                {
                    c.Clear(new(R, G, B));
                    c.Update();
                }
                for (; B > 0; B--)
                {
                    R--;
                    c.Clear(new(R, G, B));
                    c.Update();
                }
            }
        }
    }
}