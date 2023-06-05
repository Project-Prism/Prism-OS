using Cosmos.System.Graphics;

namespace PrismOS.Games.MineTest.Particles;

public class AmbientEntityEffect : Particle
{
    #region Properties

    public override Canvas Texture => throw new NotImplementedException();

    public override string Name => "ambient_entity_effect";

    public override uint ID => 0;

    #endregion
}