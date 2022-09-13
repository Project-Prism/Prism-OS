namespace PrismBinary.Text.NBT
{
	public class NBTDocument
	{
		public NBTDocument(string Source)
		{
			Tags = new();
		}

		#region Fields

		private List<NBTTag> Tags;

		#endregion
	}
}