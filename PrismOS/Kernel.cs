using System.IO;
using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using Cosmos.System.Graphics;
using System;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                UI.Framework.Canvas.Display();
                InitVFS();
                InitNet();

                Bitmap Logo = new(File.ReadAllBytes("0:\\Resources\\Icons\\Logo.bmp"));
                UI.Framework.Canvas.DrawImageAlpha(Logo, 400 - ((int)Logo.Width / 2), 300 - ((int)Logo.Height / 2));

                Bitmap Folder = new(File.ReadAllBytes("0:\\Resources\\Folder.bmp"));
                Bitmap Mouse = new(File.ReadAllBytes("0:\\Resources\\Mouse.bmp"));
                Bitmap Warn = new(File.ReadAllBytes("0:\\Resources\\Warning.bmp"));

                UI.Framework.Theme WindowTheme = new(new int[] { -8355712, -16777056, -1 });
                UI.Framework.Theme ButtonTheme = new(new int[] { -32768, -32768, -1 });

                var X = new UI.Forms.Window(50, 50, 300, 300, 0, "Hello, m8!", WindowTheme, true);
                X.Children.Add(new UI.Forms.Button(32, 32, 128, 32, 0, "This is a button", ButtonTheme, X, true));

                while (true)
                {
                    X.Draw();
                }
            }
            catch(Exception EX)
            {
                UI.Framework.Canvas.Clear();
                UI.Framework.Canvas.DrawString(
                    "Critical error! " + EX.Message,
                    Default,
                    new Pen(System.Drawing.Color.Red),
                    (UI.Framework.Width / 2) - (Default.Width * (15 + EX.Message.Length) / 2),
                    (UI.Framework.Height / 2) - (Default.Height * 2 / 2));
            }
        }
    }
}