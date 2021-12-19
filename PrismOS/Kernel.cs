using System.IO;
using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using PrismOS.Time;
using System;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                InitVFS();
                InitNet();

                Console.WriteLine("Difference check: " + Math.Calculate.Difference(0, 150) + ", " + Math.Calculate.Difference(150, 152));
                Console.WriteLine("Unix time MS: " + Time.Values.UnixTimestampMils);

                StopWatch Timer = new();
                Timer.Start();
                System.Threading.Thread.Sleep(153);
                Timer.Stop();
                Console.WriteLine("Initilised the system in " + Timer.ElapsedMiliseconds + " Mseconds.");

                //Bitmap Logo = new(File.ReadAllBytes("0:\\Resources\\Icons\\Logo.bmp"));
                //UI.Forms.Canvas.DrawImage(400 - ((int)Logo.Width / 2), 300 - ((int)Logo.Height / 2), Logo);

                //Bitmap Folder = new(File.ReadAllBytes("0:\\Resources\\Folder.bmp"));
                //Bitmap Mouse = new(File.ReadAllBytes("0:\\Resources\\Mouse.bmp"));
                //Bitmap Warn = new(File.ReadAllBytes("0:\\Resources\\Warning.bmp"));

                //UI.Framework.Theme WindowTheme = new(new int[] { -8355712, -16777056, -1 });
                //UI.Framework.Theme ButtonTheme = new(new int[] { -32768, -32768, -1 });

                //var X = new UI.Forms.Window(50, 50, 300, 300, 0, "Hello, m8!", WindowTheme, true);
                //X.Children.Add(new UI.Forms.Button(32, 32, 128, 32, 0, "This is a button", ButtonTheme, X, true));

                while (true)
                {
                    //X.Draw();
                }
            }
            catch(Exception EX)
            {
                UI.Forms.Canvas.Clear();
                UI.Forms.Canvas.DrawString(
                    (UI.Forms.Canvas.Width / 2) - (Default.Width * (15 + EX.Message.Length) / 2),
                    (UI.Forms.Canvas.Height / 2) - (Default.Height * 2 / 2),
                    Default,
                    "Error! " + EX.Message,
                    System.Drawing.Color.Red);
            }
        }
    }
}