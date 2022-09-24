namespace PrismBinary.Text.NBT
{
	public class NBTDocument
	{
		public NBTDocument(string Source)
		{
			if (!NBTList.TryParse(Source, out RootList))
			{
				throw new Exception("Error parsing NBT data.");
			}
		}

		#region Methods

		public NBTList GetRoot()
		{
			return RootList;
		}

		#endregion

		#region Fields

		private readonly NBTList RootList;

		#endregion

		public override string ToString()
		{
			string S = "";

			foreach (NBTList L in GetRoot())
			{
				S += L.ToString() + '\n';
			}

			return S;
		}
	}
}