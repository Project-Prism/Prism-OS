using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using static PrismProject.Functions.Graphics.Canvas2;
using static PrismProject.Functions.IO.FileSystem;
using static PrismProject.Functions.IO.SystemFiles;
using static PrismProject.Functions.Network.Basic;
using static System.Console;

namespace PrismProject.Functions
{
    class Loader
    {
        public static void LoadSys()
        {
            Shapes.DrawRoundRect(Width / 4-1, Height / 4-1, Width / 2+2, Height / 2+2, 10, Color.White, new int[] { 1, 1, 1, 1 });
            Shapes.DrawRoundRect(Width / 4, Height / 4, Width / 2, Height / 2, 10, Color.FromArgb(35, 35, 55), new int[] { 1, 1, 1, 1 });
            Advanced.DrawBMP((Width / 2) - ((int)Boot_bmp.Width / 2), (Height / 2) - ((int)Boot_bmp.Height / 2), Boot_bmp);
            Advanced.DrawTXT((Width/2)-(PCScreenFont.Default.Width*18/2), 425, "Prism OS (21.9.28)", Color.White);
            Beep();
            StartDisk();
            switch (FileExists(@"0:\System2\start.hx"))
            {
                case false:
                    break;

                case true:
                    break;
            }
            NetStart(Local, Subnet, Gateway2);
            Graphics.Canvas2.Canvas.Clear(Color.Green);
            Advanced.DrawPage(Width / 4, Height / 4, Width / 2, Height / 2, 4);
            while (true)
            {
            }
        }

        public static void Crash(string err)
        {
            Shapes.DrawRoundRect(Width / 4 - 1, Height / 4 - 1, Width / 2 + 2, Height / 2 + 2, 10, Color.White, new int[] { 1, 1, 1, 1 });
            Shapes.DrawRoundRect(Width / 4, Height / 4, Width / 2, Height / 2, 10, Color.FromArgb(35, 35, 55), new int[] { 1, 1, 1, 1 });
            Advanced.DrawBMP((Width / 2) - ((int)Boot_bmp.Width / 2), (Height / 2) - ((int)Boot_bmp.Height / 2), Boot_bmp);
            Advanced.DrawTXT((Width / 2) - (PCScreenFont.Default.Width * (16 + err.Length) / 2), 425, "Critical error: " + err, Color.Red);
            Beep();
            int x = 0;
            while (x != 500)
            {
                x++;
            }
        }
    }
}