using PrismOS.UI;
using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Storage.VFS.InitVFS();
            Canvas Canvas = new(1280, 720);

            System.DateTime LT = System.DateTime.Now;
            bool ShowCollon = true;
            string Time = "";

            int W2 = Canvas.Width / 2;
            int H2 = Canvas.Height / 2;

            Cosmos.System.MouseManager.ScreenWidth = (uint)Canvas.Width;
            Cosmos.System.MouseManager.ScreenHeight = (uint)Canvas.Height;

            while (true)
            {
                if (System.DateTime.Now.Second != LT.Second)
                {
                    if(ShowCollon)
                    {
                        Time = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + " PM";
                        ShowCollon = false;
                    }
                    else
                    {
                        Time = System.DateTime.Now.Hour + " " + System.DateTime.Now.Minute + " PM";
                        ShowCollon = true;
                    }
                    LT = System.DateTime.Now;
                }

                Canvas.Clear(Color.Green);
                Canvas.DrawFilledRectangle(0, 0, Canvas.Width, Default.Height + 6, Color.Gray);
                Canvas.DrawString(Canvas.Width - (Default.Width * Time.Length) - 5, 3, Default, Time, Color.White);
                Canvas.DrawString(0, 0, Default, Canvas.FPS + " FPS", Color.White);
                Canvas.DrawFilledRectangle(W2 - 100, H2 - 100, 200, H2 + 100, Color.FromArgb(100, 25, 25, 25));
                Canvas.DrawFilledRectangle(W2 - 100, H2 - 100, 200, H2 - 70, Color.FromArgb(255, 25, 25, 25));
                Canvas.DrawFilledCircle(W2, H2, 65, Color.White);
                Canvas.DrawAngledLine(W2, H2, System.DateTime.Now.Hour, 20, Color.Black);
                Canvas.DrawAngledLine(W2, H2, System.DateTime.Now.Minute, 40, Color.Black);
                Canvas.DrawAngledLine(W2, H2, System.DateTime.Now.Second, 60, Color.Black);
                Canvas.DrawFilledCircle((int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y, 3, Color.Red);
                Canvas.Update();
            }
        }
    }
}