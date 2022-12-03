using System.Runtime.InteropServices;

namespace PrismRuntime.ELF.Structure.ELFHeader
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct ELFHeader64
	{
		public ELFHeader64(ELFHeader64* Original)
		{
			ClassType = Original->ClassType;
			EndianType = Original->EndianType;
			Version = Original->Version;
			ABIType = Original->ABIType;
			ABIVersion = Original->ABIVersion;

			Type = Original->Type;
			MachineType = Original->MachineType;
			MachineVersion = Original->MachineVersion;
			Entry = Original->Entry;
			PHOffset = Original->PHOffset;
			SHOffset = Original->SHOffset;
			Flags = Original->Flags;
			EHSize = Original->EHSize;
			PHEntrySize = Original->PHEntrySize;
			PHCount = Original->PHCount;
			SHEntrySize = Original->SHEntrySize;
			SHCount = Original->SHCount;
			SHStringIndex = Original->SHStringIndex;
		}

		#region Methods

		public bool IsValid()
		{
			return
				Magic[0] == 0x7f &&
				Magic[1] == 0x45 &&
				Magic[2] == 0x4c &&
				Magic[3] == 0x46;
		}

		#endregion

		#region Fields

		#region Identifier [14 bytes]

		public fixed char Magic[4];
		public ELFClassType ClassType;
		public ELFEndianType EndianType;
		private readonly byte Version;
		public ELFSystemABIType ABIType;
		public byte ABIVersion;
		private fixed char Padding[7];

		#endregion

		public ELFType Type;
		public ELFMachineType MachineType;
		public uint MachineVersion;
		public ulong Entry;
		public ulong PHOffset;
		public ulong SHOffset;
		public uint Flags;
		public ushort EHSize;
		public ushort PHEntrySize;
		public ushort PHCount;
		public ushort SHEntrySize;
		public ushort SHCount;
		public ushort SHStringIndex;

		#endregion
	}
}