using static PrismProject.Source.Network.Interface;
using static PrismProject.Source.Assets.AssetList;
using static PrismProject.Source.Graphics.ExtendedCanvas;
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
            DrawTXT(TextXCenter(14), 450, "Please wait...", Color.White);
            DrawBMP((Width / 2) - (int)BootLogo.Width, (Height / 2) - (int)BootLogo.Height, BootLogo);
            Work();
            Desktop.Desktop.Main();
        }

        public static void Work()
        {
            Beep();
            Disk.Start();
            NetStart(Local, Subnet, Gateway2);
            InitFont();
        }
    }
}
