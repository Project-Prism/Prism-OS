using PrismProject.Source.Graphics;
using System.Drawing;

namespace PrismProject.Source.Sequence.Desktop
{
    class DisplayScreen
    {
        public static void Main()
        {
            while (true)
            {
                Drawables.Clear(Color.Green);
                Drawables.DrawWindowBase(200, 100, 400, 200, 4, Themes.Window);
                BackgroundWork.Main();
            }
        }
    }
}
