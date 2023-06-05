namespace PrismAPI.Tools.FluentXS;

public class PlugArgument
{
	#region Constructors

	public PlugArgument(string Name, int Offset, Type? Type)
	{
		if (string.IsNullOrWhiteSpace(Name))
		{
			throw new ArgumentNullException(nameof(Name));
		}

		this.Name = Name;
		this.Offset = Offset;
		this.Type = Type;
	}

	#endregion

	#region Fields

	public string Name { get; init; }
	public Type? Type { get; init; }
	public int Offset { get; init; }

	#endregion
}