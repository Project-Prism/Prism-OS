namespace PrismGraphics.Extentions.NVIDIA.Structure.BIT
{
	public unsafe struct BIOSInfo
	{
		public uint Version; // BIOS Binary Version.
		public byte OEMVersion; // BIOS OEM Version Number.
		public byte Checksum; // BIOS 0 Checksum inserted during the build.
		public ushort Callbacks; // INT15 Callbacks during POST.
		public ushort SYSTEMCallbacks; // General INT15 Callbacks.
		public ushort FrameCount; // Number of frames to display SignOn Message.
		public uint Reserved; // Reserved.
		public byte MaxHeads; // Max Heads at POST
		public byte MSR; // Scheme for computing memory size displayed in Control Panel, Does not affect functionality in any way.
		public byte WidthScale; // NOT WIDTH, only scale.
		public byte HeightScale; // NOT HEIGHT, only scale.
		public ushort* DRTPointer; // Pointer to the table of pointers identifying where all data in the VGA BIOS image is located that the OS or EFI GPU driver need.
		public ushort* ROMPacks; // Pointer to any ROMpacks. A NULL (0) pointer indicates that no run-time ROMpacks are present.
		public ushort* AppliedROMpacks; // Pointer to a list of indexes of applied run-time ROMpacks.
		public byte MaxAppliedROMpack; // Maximum number of stored indexes in the list pointed to by the Applied ROMpacks pointer.
		public byte AppliedROMpackCount; // Number of applied run-time ROMpacks NOTE: Count can be higher than amount stored at the AppliedROMpacksPtr array, if more than the value at AppliedROMpackMax were applied.
		public byte ModuleMapExternal0; // Module Map External 0 byte. Indicates whether modules outside of the BIT and not at fixed addresses are included in the binary
		public uint* CompressionInfo; // Pointer to compression information structure (for use only by stage0 build script and decompression run-time code).
	}
}