namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT
{
	/// <summary>
	/// The BIT Token Structure.
	/// Reference: https://nvidia.github.io/open-gpu-doc/BIOS-Information-Table/BIOS-Information-Table.html
	/// </summary>
	public struct BITToken
	{
		public byte ID;
		public byte Version;
		public byte Size;
		public byte Pointer;
	}
}