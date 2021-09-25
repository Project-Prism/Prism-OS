using System.Drawing;
using static PrismProject.Prism_Core.Graphics.ExtendedCanvas;

namespace PrismProject.Prism_Core.Sequence.Setup
{
    class Setup
    {
        public static void Main()
        {
            EXTCanvas.Clear(Color.White);
            DrawPage(100, 100, 700, 500, 6);
            Work();
        }

        private static void Work()
        {
            UpdateMouse();
        }
    }
}
