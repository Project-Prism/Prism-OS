namespace PrismOS.Games.MineTest.Biomes;

public abstract class Biome
{
	#region Properties

	/// <summary>
	/// The biome's name.
	/// </summary>
	public abstract string Name { get; }

	/// <summary>
	/// The biome's ID.
	/// </summary>
	public abstract uint ID { get; }

	#endregion

	#region Methods

	/// <summary>
	/// Gets a new biome from a biome name.
	/// </summary>
	/// <param name="Name">The biome name.</param>
	/// <returns>A new biome object based on input.</returns>
	/// <exception cref="NotImplementedException">Thrown on biome not found.</exception>
	public static Biome FromName(string Name)
	{
		return Name switch
		{
			"the_void" => new Void(),
			"plains" => new Plains(),
			"sunflower_plains" => new SunflowerPlains(),
			"snowy_plains" => new SnowyPlains(),
			"ice_spikes" => new IceSpikes(),
			_ => throw new NotImplementedException(),
		};
	}

	/// <summary>
	/// Gets a new biome from a biome ID.
	/// </summary>
	/// <param name="ID">The biome ID.</param>
	/// <returns>A new biome based on the ID.</returns>
	/// <exception cref="NotImplementedException">Thrown on biome not found.</exception>
	public static Biome FromID(uint ID)
	{
		return ID switch
		{
			0 => new Void(),
			1 => new Plains(),
			2 => new SunflowerPlains(),
			3 => new SnowyPlains(),
			4 => new IceSpikes(),
			_ => throw new NotImplementedException(),
		};
	}

	#endregion
}