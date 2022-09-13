namespace PrismBinary.Text.NBT
{
	// WIP, not finished!
	public class NBTNode
	{
		public NBTNode(string Name, object Value)
		{
			this.Value = Value;
			this.Name = Name;
		}
		public NBTNode(string Source)
		{
			string T = "";
			NBTNode TT;

			// Start at 1 to skip the first '{'
			// Exclude last to skip the last '}'
			for (int I = 1; I < Source.Length - 1; I++)
			{
				if (Source[I] == '\"')
				{
					TT = new(string.Empty, new());
					while (Source[I] != '\"')
					{
						T += Source[I++];
					}
					TT.SetName(T);
					T = "";
					continue;
				}
				if (Source[I] == ':')
				{
					if (Source[I + 1] == ' ')
					{
						I++;
					} // Skip space if it exists

					//while (Source[I + ] == ' ')
				}
			}
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