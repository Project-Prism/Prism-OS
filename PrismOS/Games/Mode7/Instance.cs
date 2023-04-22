using PrismGraphics;

namespace PrismOS.Games.Mode7;

public class Instance
{
    public Instance()
    {
        Output = new(Config.Width, Config.Height);
        Floor = new(64, 64);
        Floor.DrawRectangleGrid(0, 0, 2, 2, 32, Color.Black, Color.White);
    }

    #region Methods

    public void Render()
    {
        Output.Clear();

        for (int X = 0; X < Output.Width; X++)
        {
            for (int Y = Output.Height / 2; Y < Output.Height; Y++)
            {
                float XX = Config.Width / 2 - X;
                float YY = Config.FocalLength + Y;
                float ZZ = Y - Config.Height / 2 + 0.01f;

                // Projection
                float PX = XX / ZZ * Config.Scale;
                float PY = YY / ZZ * Config.Scale;

                int FloorX = (int)(PX % Floor.Width);
                int FloorY = (int)(PY % Floor.Height);

                Output[X, Y] = Floor[FloorX, FloorY];
            }
        }
    }

    #endregion

    #region Fields

    public Graphics Output;
    public Graphics Floor;

    #endregion
}