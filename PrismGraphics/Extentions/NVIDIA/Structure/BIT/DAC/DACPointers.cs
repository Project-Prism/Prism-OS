namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT.DAC
{
	/// <summary>
	/// This data structure contains DAC related data.
	/// </summary>
	public unsafe struct DACPointers
	{
		#region Properties

		/// <summary>
		/// Checks if DAC sleep is supported.
		/// </summary>
		public bool IsSleepSupported => 1 == (((byte)Flags >> 0) & 1);

		#endregion

		#region Fields

		public ushort* Address;
		public DACFlags Flags;

		#endregion
	}
}