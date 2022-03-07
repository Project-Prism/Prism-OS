using Keyboard = Cosmos.System.KeyboardManager;
using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Numerics;
using System.Drawing;

namespace PrismOS.Apps
{
    public class GameOfLife
    {
        public int SquareSize = 32;
        public bool Paused = true;
        public Matrix<bool> GameBoard = new();

        public void Update(Canvas Canvas)
        {
            if (!Paused)
            {
                for (int X = 0; X < Canvas.Width; X *= SquareSize)
                {
                    for (int Y = 0; Y < Canvas.Height; Y *= SquareSize)
                    {
                        if (X >= 0 && Y >= 0 && X <= Canvas.Width && Y <= Canvas.Height)
                        {
                            int AliveNeighborsCount = 0;
                            foreach (bool B in new bool[] { GameBoard.M[X - 1][Y], GameBoard.M[X + 1][Y], GameBoard.M[X][Y - 1], GameBoard.M[X][Y + 1] })
                            {
                                if (B)
                                    AliveNeighborsCount++;
                            }
                            if (AliveNeighborsCount != 3)
                                GameBoard.M[32 * X][32 * Y] = false;
                            else
                                Canvas.DrawFilledRectangle(32 * X, 32 * Y, SquareSize, SquareSize, Color.White);
                        }
                    }
                }
            }
            if (Keyboard.ControlPressed)
            {
                Paused = !Paused;
            }
            if (Mouse.MouseState == Cosmos.System.MouseState.Left)
            {
                GameBoard.M[32 * (int)Mouse.X][32 * (int)Mouse.Y] = true;
            }
        }
    }
}