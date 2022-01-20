using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using PrismOS.UI;
using PrismOS.UI.Elements;
using PrismOS.UI.Elements.Clocks;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Storage.VFS.InitVFS();
            Canvas Canvas = new(1280, 720);

            Window Window1 = new(500, 700, 400, 100, Color.White, Color.DarkSlateGray, Color.Blue, Canvas);
            Window1.Children.Add(new Mixed(100, 100, 20, Window1));

            while (true)
            {
                Canvas.Clear(Color.Green);
                Window1.Draw();
                Canvas.DrawString(0, 0, Default, Canvas.FPS + " FPS", Color.White);
                Canvas.Update();
            }
        }
    }
}