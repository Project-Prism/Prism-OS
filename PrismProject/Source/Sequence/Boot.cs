using static PrismProject.Source.Network.Interface;
using static PrismProject.Source.Graphics.ExtendedCanvas;
using static PrismProject.Source.Assets.Converted;
using static Cosmos.System.PCSpeaker;
using PrismProject.Source.FileSystem;
using System.Drawing;

namespace PrismProject.Source.Sequence
{
    class Boot
    {
        public static void Main()
        {
            EXTInit();
            DrawTXT(200, 450, "Please wait...", Color.White);
            DrawBMP((Width / 2) - ((int)BootLogo.Width/2), (Height / 2) - ((int)BootLogo.Height/2), BootLogo);
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
