namespace PrismFilesystem.XFS.Structure
{
	public unsafe struct SuperBlock
	{
		#region Methods

		public bool IsValid => MagicNumber == 0x58465342;

		#endregion

		#region Fields

		public uint MagicNumber;
		public uint BlockSize;
		public ulong DBlocks;
		public ulong RBlocks;
		public ulong RExtents;
		public string UUID;
		public ulong LogStart;
		public ulong RootInode;
		public ulong RBMInode;
		public ulong RsumInode;
		public uint RExtentSize;
		public uint AGBlocks;
		public uint AGCcount;
		public uint RBMBlocks;
		public uint LogBlocks;
		public ushort VersionNumber;
		public ushort SectorSize;
		public ushort InodeSize;
		public uint Inopblock;
		public fixed char FNname[12];
		public byte BlockLog;
		public byte SectorLog;
		public byte InodeLog;
		public byte InopbLog;
		public byte AgblkLog;
		public byte RExtentsLog;
		public byte InProgress;
		public byte IMaxPCT;
		public ulong ICount;
		public ulong IFree;
		public ulong FDBlocks;
		public ulong FRExtents;
		public ulong Uquotino;
		public ulong Gquotino;
		public SuperBlockQuotaFlags QFlags;
		public SuperBlockFlags Flags;
		public byte SharedVN;
		public uint InodeAlignment;
		public uint Unit;
		public uint Width;
		public byte DirblkLog;
		public byte LogsectorLog;
		public ushort LogSectorSize;
		public uint LogsUnit;
		public uint Features2;

		#endregion
	}
}