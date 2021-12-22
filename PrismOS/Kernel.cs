using Pen = Cosmos.System.Graphics.Pen;
using Font = Cosmos.System.Graphics.Fonts.PCScreenFont;
using static PrismOS.Storage.Storage;
using static PrismOS.Network.Network;
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

                Network.NWeb.NWebClient x = new(new Cosmos.System.Network.IPv4.Address(0, 0, 0, 0), "ID1");
                x.Post("Test.txt", System.Text.Encoding.UTF8.GetBytes("Hello, this is a test using the NWeb client and server!"));
                Console.WriteLine(x.Get("Test.txt"));
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