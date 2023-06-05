using Cosmos.System.Graphics;
using PrismOS.Games.MineTest.Types;

namespace PrismOS.Games.MineTest.Particles;

public class Block : Particle
{
    #region Constructors

    public Block()
    {
        State = 0;
    }

    #endregion

    #region Properties

    public override Canvas Texture => throw new NotImplementedException();

    public override string Name => "block";

    public override uint ID => 0;

    #endregion

    #region Fields

    public VarInt State;

    #endregion
}