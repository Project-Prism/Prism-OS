namespace PrismBinary.Text.NBT
{
	public class NBTDocument
	{
		public NBTDocument(string Source)
		{
			Nodes = new();
		}


		#region Methods

		public NBTNode[] GetNodes()
		{
			return Nodes.ToArray();
		}

		#endregion

		#region Fields

		private List<NBTNode> Nodes;

		#endregion
	}
}