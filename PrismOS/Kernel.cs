using PrismOS.Libraries.Graphics;
using System.Drawing;
using System;
using PrismOS.Libraries.Numerics;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        // TODO
        // fix 3D (wtf weird grub error??) (tutorial is https://www.youtube.com/watch?v=ih20l3pJoeU)
        // fix "game of life" (matrix doesn't initialise properly, also effects 3D)
        // re-do perlin noise generator (screwed up currently :skull:)
        // implement draw filled triangle into canvas _properly_

        protected override void Run()
        {
            Canvas Canvas = new(1024, 768, true);
            Apps.GameOfLife GL = new(Canvas);

            try
            {
                if (!Tests.Tester.MatrixTest())
                    new Apps.Menus().Update1(new("Matrix tests failed! expected values not found."), Canvas);

                while (true)
                {
                    Canvas.Clear();
                    GL.Update();
                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS, Color.White);
                    Canvas.Update();
                }
            }
            catch (Exception EX)
            {
                new Apps.Menus().Update1(EX.Message, Canvas);
            }
        }
    }
}