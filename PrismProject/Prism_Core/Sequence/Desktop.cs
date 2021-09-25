using PrismProject.Prism_Core.Graphics;
using System.Drawing;

namespace PrismProject.Prism_Core.Sequence.Desktop
{
    internal class Desktop
    {
        public static void Main()
        {
            while (true)
            {
                ExtendedCanvas.EXTCanvas.Clear(Color.Green);
                ExtendedCanvas.DrawPage(200, 100, 400, 200, 4);
            }
        }
    }
}