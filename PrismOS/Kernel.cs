using PrismOS.UI;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            CircleSize();
        }

        public static void CircleSize()
        {
            while (true)
            {
                Canvas c = new(1280, 720);

                while (true)
                {
                    for (int Radius = 1; Radius < 50; Radius++)
                    {
                        c.Clear(new(System.Drawing.Color.Black));
                        c.DrawFilledCircle(c.Width / 2, c.Height / 2, Radius, new(-1));
                        c.Update();
                    }
                    for (int Radius = 50; Radius > 1; Radius--)
                    {
                        c.Clear(new(System.Drawing.Color.Black));
                        c.DrawFilledCircle(c.Width / 2, c.Height / 2, Radius, new(-1));
                        c.Update();
                    }
                }
            }
        }

        public static void SizeDemo()
        {
            Canvas c = new(1280, 720);

            int I = 150;
            int A = 0;

            while (true)
            {
                for (; I > 1; I--)
                {
                    c.Clear(new(System.Drawing.Color.Green.ToArgb()));
                    c.DrawFilledRectangle(c.Width / 2, c.Height / 2, I, I, new(A, 25, 25, 25));
                    c.Update();
                }
                for (; I < 150; I++)
                {
                    c.Clear(new(System.Drawing.Color.Green.ToArgb()));
                    c.DrawFilledRectangle(c.Width / 2, c.Height / 2, I, I, new(A, 25, 25, 25));
                    c.Update();
                }
                for (; A < 255; A++)
                {
                    c.Clear(new(System.Drawing.Color.Green.ToArgb()));
                    c.DrawFilledRectangle(c.Width / 2, c.Height / 2, I, I, new(A, 25, 25, 25));
                    c.Update();
                }
                for (; A > 0; A--)
                {
                    c.Clear(new(System.Drawing.Color.Green.ToArgb()));
                    c.DrawFilledRectangle(c.Width / 2, c.Height / 2, I, I, new(A, 25, 25, 25));
                    c.Update();
                }
            }
        }

        public static void FadeDemo()
        {
            int R = 0;
            int G = 0;
            int B = 0;

            Canvas c = new(1280, 720);

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