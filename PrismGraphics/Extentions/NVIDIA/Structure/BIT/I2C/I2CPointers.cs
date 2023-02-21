namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT.I2C
{
	/// <summary>
	/// This data structure contains I2C scripting data.
	/// </summary>
	public unsafe struct I2CPointers
	{
		public ushort* Address;
		public ushort* ExternalHWMonInit;
	}
}