using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject.Functions.Graphics.Fake3D
{
    class Shapes
    {
        public static void RenderBox()
        {
            int aX = 220;
            int aY = 290;
            int bX = 320;
            int bY = 340;
            int cX = 420;
            int cY = 290;
            int dX = 320;
            int dY = 240;
            int eX = aX;
            int eY = aY - 100;
            int fX = bX;
            int fY = bY - 100;
            int gX = cX;
            int gY = cY - 100;
            int hX = dX;
            int hY = dY - 100;
            Pen pen = new Pen(Color.Red);

            for (int i = 0; i < 200; i++)
            {
                if (i < 100)
                {
                    Canvas2.Canvas.Clear(Color.Black);
                    aX++;
                    bX++;
                    cX--;
                    dX--;
                    if (i % 2 == 0)
                    {
                        aY++;
                        bY--;
                        cY--;
                        dY++;
                    }

                    eY = aY - 100;
                    fY = bY - 100;
                    gY = cY - 100;
                    hY = dY - 100;
                    eX = aX;
                    fX = bX;
                    gX = cX;
                    hX = dX;

                    Canvas2.Canvas.DrawLine(pen, eX, eY, aX, aY);
                    Canvas2.Canvas.DrawLine(pen, fX, fY, bX, bY);
                    Canvas2.Canvas.DrawLine(pen, gX, gY, cX, cY);
                    Canvas2.Canvas.DrawLine(pen, hX, hY, dX, dY);

                    Canvas2.Canvas.DrawLine(pen, eX, eY, fX, fY);
                    Canvas2.Canvas.DrawLine(pen, fX, fY, gX, gY);
                    Canvas2.Canvas.DrawLine(pen, gX, gY, hX, hY);
                    Canvas2.Canvas.DrawLine(pen, hX, hY, eX, eY);

                    Canvas2.Canvas.DrawLine(pen, bX, bY, aX, aY);
                    Canvas2.Canvas.DrawLine(pen, cX, cY, bX, bY);
                    Canvas2.Canvas.DrawLine(pen, dX, dY, cX, cY);
                    Canvas2.Canvas.DrawLine(pen, aX, aY, dX, dY);
                }

                else if (i >= 100)
                {
                    Canvas2.Canvas.Clear(Color.Black);
                    aX--;
                    bX--;
                    cX++;
                    dX++;
                    if (i % 2 == 0)
                    {
                        aY--;
                        bY++;
                        cY++;
                        dY--;
                    }

                    eY = aY - 100;
                    fY = bY - 100;
                    gY = cY - 100;
                    hY = dY - 100;
                    eX = aX;
                    fX = bX;
                    gX = cX;
                    hX = dX;

                    Canvas2.Canvas.DrawLine(pen, eX, eY, aX, aY);
                    Canvas2.Canvas.DrawLine(pen, fX, fY, bX, bY);
                    Canvas2.Canvas.DrawLine(pen, gX, gY, cX, cY);
                    Canvas2.Canvas.DrawLine(pen, hX, hY, dX, dY);

                    Canvas2.Canvas.DrawLine(pen, eX, eY, fX, fY);
                    Canvas2.Canvas.DrawLine(pen, fX, fY, gX, gY);
                    Canvas2.Canvas.DrawLine(pen, gX, gY, hX, hY);
                    Canvas2.Canvas.DrawLine(pen, hX, hY, eX, eY);

                    Canvas2.Canvas.DrawLine(pen, bX, bY, aX, aY);
                    Canvas2.Canvas.DrawLine(pen, cX, cY, bX, bY);
                    Canvas2.Canvas.DrawLine(pen, dX, dY, cX, cY);
                    Canvas2.Canvas.DrawLine(pen, aX, aY, dX, dY);
                }
            }


            Console.ReadKey();
            RenderBox();

        }
    }
}
