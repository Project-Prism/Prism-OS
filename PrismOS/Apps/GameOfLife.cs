using Keyboard = Cosmos.System.KeyboardManager;
using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Numerics;
using System.Drawing;

namespace PrismOS.Apps
{
    public class GameOfLife
    {
        public GameOfLife(Canvas Canvas, int SquareSize = 32)
        {
            this.Canvas = Canvas;
            this.SquareSize = SquareSize;
            GameBoard = new(Canvas.Width / SquareSize, Canvas.Height / SquareSize);
        }

        public Canvas Canvas;
        public int SquareSize;
        public bool Paused = true;
        public Matrix<bool> GameBoard;

        public void Update()
        {
            if (!Paused)
            {
                for (int X = 0; X < GameBoard.M.Count; X++)
                {
                    for (int Y = 0; Y < GameBoard.M[X].Count; Y++)
                    {
                        int AliveNeighborsCount = 0;
                        foreach (bool B in new bool[] { GameBoard.M[X - 1][Y], GameBoard.M[X + 1][Y], GameBoard.M[X][Y - 1], GameBoard.M[X][Y + 1] })
                        {
                            if (B)
                                AliveNeighborsCount++;
                        }

                        GameBoard.M[X][Y] = AliveNeighborsCount == 3;
                    }
                }
            }
            for (int X = 0; X < Canvas.Width; X++)
            {
                for (int Y = 0; Y < Canvas.Height; Y++)
                {
                    if (GameBoard.M[X][Y] == true)
                        Canvas.DrawFilledRectangle(X * SquareSize, Y * SquareSize, SquareSize, SquareSize, Color.White);
                    else
                        Canvas.DrawFilledRectangle(X * SquareSize, Y * SquareSize, SquareSize, SquareSize, Color.Black);
                }
            }
            if (Keyboard.ControlPressed)
            {
                Paused = !Paused;
            }
            if (Mouse.MouseState == Cosmos.System.MouseState.Left)
            {
                GameBoard.M[(int)Mouse.X / SquareSize][(int)Mouse.Y / SquareSize] = !GameBoard.M[(int)Mouse.X / SquareSize][(int)Mouse.Y / SquareSize];
            }
        }
    }
}