namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT
{
	/// <summary>
	/// The BIOS Information Table Structure.
	/// Reference: https://nvidia.github.io/open-gpu-doc/BIOS-Information-Table/BIOS-Information-Table.html
	/// </summary>
	public unsafe struct BITHeader
	{
		#region Methods

		/// <summary>
		/// Checks if the BIT header signature is valid.
		/// </summary>
		/// <returns>True or False.</returns>
		public bool IsValid()
		{
			return
				Signature[0] == 'B' &&
				Signature[1] == 'I' &&
				Signature[2] == 'T' &&
				Signature[3] == '\0' &&
				CheckSum == 0x00;
		}

		#endregion

		#region Fields

		public ushort ID;
		public fixed char Signature[4];
		public ushort BCDVersion;
		public byte HeaderSize;
		public byte TokenSize;
		public byte TokenCount;
		public byte CheckSum;

		#endregion
	}
}