using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using PrismProject.Source.Graphics;
using System;
using System.Drawing;

namespace PrismProject.Source.Sequence.Boot
{
    class DisplayScreen
    {
        [ManifestResourceStream(ResourceName = "PrismProject.Source.Assets.Boot.bmp")] public static byte[] BootImage;

        private static readonly int Width = Drawables.Width, Height = Drawables.Height;
        private static readonly Bitmap Boot = new Bitmap(BootImage);

        public static void Main()
        {
            Drawables.DrawImage(Width/2-((int)Boot.Width), Height/2-((int)Boot.Height), Boot);
            try { BackgroundWork.Main(); }
            catch (Exception ex) { Drawables.Screen.Disable();  Console.WriteLine(ex.Message); }
            while (true)
            {  Desktop.DisplayScreen.Main(); }
        }
    }
}
