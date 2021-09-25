using static PrismProject.Prism_Core.Net.Core;
using static PrismProject.Prism_Core.Graphics.ExtendedCanvas;
using static PrismProject.Prism_Core.Internal.Files;
using static Cosmos.System.PCSpeaker;
using PrismProject.Prism_Core.IO;
using System.Drawing;

namespace PrismProject.Prism_Core.Sequence.Boot
{
    class Boot
    {
        public static void Main()
        {
            EXTInit();
            DrawTXT(200, 450, "Please wait...", Color.White);
            DrawBMP((Width / 2) - ((int)Boot_bmp.Width/2), (Height / 2) - ((int)Boot_bmp.Height/2), Boot_bmp);
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
