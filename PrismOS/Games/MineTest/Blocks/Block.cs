using Cosmos.System.Graphics;

namespace PrismOS.Games.MineTest.Blocks;

public abstract class Block
{
    #region Properties

    /// <summary>
    /// The block's textures.
    /// </summary>
    public abstract Canvas[] Texture { get; }

    /// <summary>
    /// The block's name.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// The item the block will drop.
    /// </summary>
    public abstract uint ItemID { get; }

    /// <summary>
    /// The block's world ID.
    /// </summary>
    public abstract uint ID { get; }

    #endregion
}