namespace PrismRuntime.SShell.Structure
{
	/// <summary>
	/// This is the base class all commands must inherit.
	/// </summary>
	public abstract class Script
	{
		public Script(string Name, string Description)
		{
			this.Name = Name;
			this.Description = Description;

			Shell.Scripts.Add(this);
		}

		#region Methods

		public abstract void Invoke(string[] Args);

		#endregion

		#region Fields

		public string Name { get; set; }
		public string Description { get; set; }

		#endregion
	}
}