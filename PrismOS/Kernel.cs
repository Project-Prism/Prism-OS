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
                Canvas.DrawString(Canvas.Width - (Default.Width * Time.Length) - 5, 2, Default, Time, Color.White);
                Canvas.Update();
            }
        }
    }
}