namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT
{
	/// <summary>
	/// The list/enum containing all token types.
	/// Retefence: https://nvidia.github.io/open-gpu-doc/BIOS-Information-Table/BIOS-Information-Table.html
	/// </summary>
	public enum BITTokens : byte
	{
		I2CPointers = 0x32, // I2C Script Pointers
		DACPointers = 0x41, // DAC Data Pointers
		BIOSData = 0x42, // BIOS Data
		ClockPointers = 0x43, // Clock Script Pointers
		DFPPointers = 0x44, // DFP/Panel Data Pointers
		NVInitPointers = 0x49, // Initialization Table Pointers
		LVDSPointers = 0x4C, // LVDS Table Pointers
		MemoryPointers = 0x4D, // Memory Control/Programming Pointers
		NoOperation = 0x4E, // No Operation
		PerfPointers = 0x50, // Performance Table Pointers
		StringPointers = 0x53, // String Pointers
		TMDSPointers = 0x54, // TMDS Table Pointers
		DisplayPointers = 0x55,// Display Control/Programming Pointers
		VirtualPointers = 0x56, // Virtual Field Pointers
		B32Pointers = 0x63, // 32-bit Pointer Data
		DPTPointers = 0x64, // DP Table Pointers
		FalconData = 0x70, // Falcon Ucode Data
		UEFIData = 0x75, // UEFI Driver Data
		MXMData = 0x78, // MXM Configuration Data
		BridgeData = 0x52, // Bridge Firmware Data
	}
}