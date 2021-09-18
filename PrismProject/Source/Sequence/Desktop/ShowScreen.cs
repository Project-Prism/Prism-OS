using PrismProject.Source.Graphics;
using System.Drawing;

namespace PrismProject.Source.Sequence.Desktop
{
    internal class ShowScreen
    {
        public static void Main()
        {
            Drawables.Clear(Color.Green);
            Drawables.DrawWindowBase(200, 100, 400, 400, 4, Themes.Window);
        }
    }
}