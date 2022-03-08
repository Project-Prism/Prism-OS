using PrismOS.Libraries.Graphics;
using System.Drawing;
using System;
using PrismOS.Libraries.Numerics;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        // TODO
        // fix 3D (wtf weird grub error??)
        // fix "game of life" (matrix doesn't initialise properly, also effects 3D)
        // re-do perlin noise generator (screwed up currently :skull:)
        // implement draw filled triangle into canvas _properly_

        protected override void Run()
        {
            Mesh Cube = new(new Triangle[] {
                // South
                new (0.0f, 0.0f, 0.0f,    0.0f, 1.0f, 0.0f,    1.0f, 1.0f, 0.0f),
                new (0.0f, 0.0f, 0.0f,    1.0f, 1.0f, 0.0f,    1.0f, 0.0f, 0.0f),

                // East
                new (1.0f, 0.0f, 0.0f,    1.0f, 1.0f, 0.0f,    1.0f, 1.0f, 1.0f),
                new (1.0f, 0.0f, 0.0f,    1.0f, 1.0f, 1.0f,    1.0f, 0.0f, 1.0f),

                // North
                new (1.0f, 0.0f, 1.0f,    1.0f, 1.0f, 1.0f,    0.0f, 1.0f, 1.0f),
                new (1.0f, 0.0f, 1.0f,    0.0f, 1.0f, 1.0f,    0.0f, 0.0f, 1.0f),

                // West
                new (0.0f, 0.0f, 1.0f,    0.0f, 1.0f, 1.0f,    0.0f, 1.0f, 0.0f),
                new (0.0f, 0.0f, 1.0f,    0.0f, 1.0f, 0.0f,    0.0f, 0.0f, 0.0f),

                // Top
                new (0.0f, 1.0f, 0.0f,    0.0f, 1.0f, 1.0f,    1.0f, 1.0f, 1.0f),
                new (0.0f, 1.0f, 0.0f,    1.0f, 1.0f, 1.0f,    1.0f, 1.0f, 0.0f),

                // Bottom
                new (1.0f, 0.0f, 1.0f,    0.0f, 0.0f, 1.0f,    0.0f, 0.0f, 0.0f),
                new (1.0f, 0.0f, 1.0f,    0.0f, 0.0f, 0.0f,    1.0f, 0.0f, 0.0f)});
            Canvas Canvas = new(1024, 768, true);
            Apps.GameOfLife GOL = new(Canvas);

            try
            {
                while (true)
                {
                    Canvas.Clear();
                    //Canvas.DrawObject(Cube, 2);
                    GOL.Update();
                    Canvas.DrawString(15, 15, "FPS: " + Canvas.FPS + (GOL.Paused ? "\nGame paused." : "\nGame running."), Color.White);
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