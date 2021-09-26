using static PrismProject.Prism_Core.Net.Core;
using static PrismProject.Prism_Core.Graphics.Canvas2;
using static PrismProject.Prism_Core.Internal.Files;
using static Cosmos.System.PCSpeaker;
using PrismProject.Prism_Core.IO;

namespace PrismProject.Prism_Core.Sequence.Boot
{
    class Boot
    {
        public static void Main()
        {
            Advanced.DrawBMP(X1: (Width / 2) - ((int)Boot_bmp.Width / 2), Y1: (Height / 2) - ((int)Boot_bmp.Height / 2), BMP: Boot_bmp);
            Work();
            Desktop.Desktop.Main();
        }

        public static void Work()
        {
            Beep();
            Disk.Start();
            NetStart(Local, Subnet, Gateway2);
        }
    }
}
