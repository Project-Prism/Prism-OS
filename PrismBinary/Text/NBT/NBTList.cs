namespace PrismBinary.Text.NBT
{
	public class NBTList : List<NBTList>
	{
		public NBTList(string Name, List<NBTNode> Nodes)
		{
			this.Name = Name;
			this.Nodes = Nodes;
		}
		public NBTList(string Name)
		{
			this.Name = Name;
			Nodes = new();
		}
		private NBTList()
		{
			Name = "root";
			Nodes = new();
		}

		#region Fields

		private string Name;
		private List<NBTNode> Nodes;

		#endregion

		#region Methods

		public string GetName()
		{
			return Name;
		}
		public void SetName(string Name)
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				throw new ArgumentNullException(nameof(Name));
			}

			this.Name = Name;
		}

		public List<NBTNode> GetNodes()
		{
			return Nodes;
		}
		public void SetNodes(List<NBTNode> Nodes)
		{
			this.Nodes = Nodes;
		}

		public static bool TryParse(string Source, out NBTList List)
		{
			NBTList TL = new();

			for (int I = 0; I < Source.Length; I++)
			{
				if (Source[I] == '{')
				{
					NBTNode TN = new();

					while (Source[++I] != ':')
					{
						TN.SetName(TN.GetName() + Source[I++]);
					}

					if (Source[I] == '{')
					{
						if (TryParse(Source[I..], out NBTList L))
						{
							TL.Add(new(TN.GetName(), L.GetNodes()));
							continue;
						}
						else
						{
							throw new Exception("Could not read NBT data.");
						}
					}

					while (Source[I] != ',' && Source[I] != '}')
					{
						TN.SetValue(((string)TN.GetValue()) + Source[I++]);
					}

					TL.Nodes.Add(TN);
					continue;
				}
			}

			List = TL;
			return true;
		}

		#endregion
	}
}