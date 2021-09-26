using static PrismProject.Prism_Core.Graphics.Canvas2;
using System.Drawing;

namespace PrismProject.Prism_Core.Sequence.Desktop
{
    internal class Desktop
    {
        public static void Main()
        {
            while (true)
            {
                Canvas.Clear(Color.Green);
                Advanced.DrawPage(200, 100, 400, 200, 4);
            }
        }
    }
}