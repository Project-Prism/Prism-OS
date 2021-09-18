using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using Cosmos.System;
using static PrismProject.Source.Graphics.Drawables;
using static PrismProject.Source.Network.Interface;
using PrismProject.Source.FileSystem;

namespace PrismProject.Source.Sequence.Boot
{
    internal class ShowScreen
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Boot.bmp")] public static Bitmap Boot;
        public static void Main()
        {
            DrawImage(UI_Set[0] - (int)Boot.Width, (UI_Set[1]) - (int)Boot.Height, Boot);
            PCSpeaker.Beep();
            Disk.Start();
            NetStart(Local, Subnet, Gateway2);
            Desktop.ShowScreen.Main();
        }
    }
}