using Pen = Cosmos.System.Graphics.Pen;
using Font = Cosmos.System.Graphics.Fonts.PCScreenFont;
using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
using System;
using static PrismOS.UI.Framework;
using static PrismOS.UI.Forms;
using Cosmos.System.Graphics;

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

                UI.Forms.Canvas.Mode = new Mode(1280, 720, (ColorDepth)32);

                Theme WindowTheme = new(new int[] { -8355712, -16777056, -1 });
                Theme ButtonTheme = new(new int[] { -32768, -32768, -1 });
                Theme TextTheme = new(new int[] { -1, -1, -1 });

                var X = new Window(32, 32, 256, 256, 0, "Stats", WindowTheme, true);
                X.Children.Add(new Label(15, 15, "Installed RAM: " + Cosmos.Core.CPU.GetAmountOfRAM(), true, TextTheme, X));

                while (true)
                {
                    X.Draw();
                }
            }
            catch(Exception EX)
            {
                UI.Forms.Canvas.Clear();
                UI.Forms.Canvas.DrawString(
                    "Error! " + EX.Message,
                    Font.Default,
                    new Pen(System.Drawing.Color.Red),
                    (UI.Forms.Canvas.Mode.Columns / 2) - (Font.Default.Width * (15 + EX.Message.Length) / 2),
                    (UI.Forms.Canvas.Mode.Rows / 2) - (Font.Default.Height * 2 / 2));
            }
        }
    }
}