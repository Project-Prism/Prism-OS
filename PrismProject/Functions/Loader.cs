using static PrismProject.Functions.IO.FileSystem;
using static PrismProject.Functions.Network.Basic;
using static PrismProject.Functions.IO.SystemFiles;
using static PrismProject.Functions.Graphics.Canvas2;
using static Cosmos.HAL.PCSpeaker;
using System.Drawing;
using System;

namespace PrismProject.Functions
{
    class Loader
    {
        public static void StartLoader()
        {
            StartDisk();
            switch(FileExists(@"0:\System2\start.hx"))
            {
                case false:
                    break;
                case true:
                    break;
            }
            Canvas.Clear(Color.White);
            Shapes.DrawRoundRect(Width / 4, Height / 3, Width / 2, Height / (int)1.5, 4, Color.DimGray, new int[] { 1, 1, 1, 1 });
            Advanced.DrawBMP((Width / 2) - ((int)Boot_bmp.Width/2), (Height / 2) - ((int)Boot_bmp.Height / 2), Boot_bmp);
            Console.Beep();
            NetStart(Local, Subnet, Gateway2);
            Canvas.Clear(Color.AliceBlue);
            while (true)
            {

            }
        }
    }
}
