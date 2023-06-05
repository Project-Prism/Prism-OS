using PrismAPI.Graphics;

namespace PrismOS.Games.MineTest.Items;

public abstract class Item
{
    #region Properties

    /// <summary>
    /// The item's texture when in the inventory.
    /// </summary>
    public abstract Canvas Texture { get; }

    /// <summary>
    /// The item's name.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// The block ID of the item.
    /// </summary>
    public abstract uint BlockID { get; }

    /// <summary>
    /// The ID of the item.
    /// </summary>
    public abstract uint ID { get; }

    #endregion
}