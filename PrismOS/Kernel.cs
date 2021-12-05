using Arc;
using System;
using static PrismOS.Storage.Framework;
namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            int X = 200;
            int Y = 200;
            int deg = 0;
            

            while (true)
            {
                if (deg != 360)
                {
                    deg++;
                }
                else
                {
                    deg = 0;
                }
                UI.Extras.Canvas.DrawLine(
                    pen: new Cosmos.System.Graphics.Pen(System.Drawing.Color.White),
                    x1: X,
                    y1: Y,
                    x2: (int)Math.Cos(X + (deg * (Math.PI / 180))),
                    y2: (int)Math.Cos(Y + (deg * (Math.PI / 180))));
                
            }
        }
    }
}