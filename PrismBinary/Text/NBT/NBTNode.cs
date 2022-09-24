namespace PrismBinary.Text.NBT
{
	public class NBTNode
	{
		public NBTNode(string Name, object Value)
		{
			this.Value = Value;
			this.Name = Name;
		}
		public NBTNode()
		{
			Value = string.Empty;
			Name = string.Empty;
		}

		#region Fields

		private object Value;
		private string Name;

		#endregion

		#region Methods

		public void SetValue(object Value)
		{
			this.Value = Value ?? throw new("Value is null.");
		}
		public object GetValue()
		{
			return Value ?? throw new("Value is null.");
		}

		public void SetName(string Name)
		{
			this.Name = Name;
		}
		public string GetName()
		{
			return Name;
		}

		#endregion
	}
}