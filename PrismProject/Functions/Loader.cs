using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using static PrismProject.Functions.Graphics.Canvas2;
using static PrismProject.Functions.IO.Disk;
using static PrismProject.Functions.IO.SystemFiles;
using static PrismProject.Functions.Network.Basic;
using static System.Console;
using Cosmos.System;

namespace PrismProject.Functions
{
    class Loader
    {
        
        public static void InitCore()
        {
            // Cosmos.System.Graphics.Fonts.PCScreenFont.LoadFont(Convert.FromBase64String(""));
            Canvas.Clear();
            int[] All = new int[] { 1, 1, 1, 1 };
            Shapes.DrawRoundRect(Width / 4 - 1, Height / 4 - 1, Width / 2 + 2, Height / 2 + 2, 10, Color.White, All);
            Shapes.DrawRoundRect(Width / 4, Height / 4, Width / 2, Height / 2, 10, Color.FromArgb(35, 35, 55), All);
            Advanced.DrawBMP((Width / 2) - ((int)Boot_bmp.Width / 2), (Height / 2) - ((int)Boot_bmp.Height / 2), Boot_bmp);
            Advanced.DrawTXT((Width / 2) - (PCScreenFont.Default.Width * 18 / 2), 425, "Prism OS (21.9.28)", Color.White);
            Beep();
            StartDisk();
            NetStart(Local, Subnet, Gateway2);
            System2.Threading.Thread.Sleep(3);
            Canvas.Clear(Color.Black);

            Graphics.Fake3D.Shapes.RenderBox();
        }
    }
}