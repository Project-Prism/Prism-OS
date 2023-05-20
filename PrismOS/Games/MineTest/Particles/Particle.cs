using Cosmos.System.Graphics;

namespace PrismOS.Games.MineTest.Particles;

public abstract class Particle
{
	#region Properties

	public abstract Canvas Texture { get; }

	public abstract string Name { get; }

	public abstract uint ID { get; }

	#endregion
}