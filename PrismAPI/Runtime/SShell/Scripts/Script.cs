namespace PrismAPI.Runtime.SShell.Scripts;

/// <summary>
/// This is the base class all commands must inherit.
/// </summary>
public abstract class Script
{
	internal Script(string ScriptName, string BasicDescription)
	{
		AdvancedDescription = string.Empty;
		this.BasicDescription = BasicDescription;
		this.ScriptName = ScriptName;
	}

	#region Methods

	public abstract void Invoke(string[] Args);

	#endregion

	#region Fields

	public string AdvancedDescription { get; set; }
	public string BasicDescription { get; set; }
	public string ScriptName { get; set; }

	#endregion
}