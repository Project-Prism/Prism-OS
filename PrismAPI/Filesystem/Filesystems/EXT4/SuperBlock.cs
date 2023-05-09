namespace PrismAPI.Filesystem.Filesystems.EXT4;

/// <summary>
/// The EXT4 filesystem superblock implementation.
/// Reference: https://wiki.osdev.org/Ext4
/// </summary>
public unsafe struct SuperBlock
{
    public uint INodeCount;
    public uint BlockCount;
    public uint ReservedBlockCount;
    public uint UnallocatedBlockCount;
    public uint UnallocatedINodeCount;
    public uint SuperBlockIndex;
    public uint BlockSizeShifter;
    public uint FragmentSizeShifter;
    public uint BlockPerGroup;
    public uint FragmentPerGroup;
    public uint INodesPerGroup;
    public uint LastMountTime;
    public uint LastWriteTime;
    public ushort TimesMountedSinceFSCK;
    public ushort TimesBeforeFSCK;
    public ushort MagicSignature;
    public ushort Status;
    public ushort OnErrorFunction;
    public ushort MinorVersion;
    public uint LastFSCK;
    public uint ForcedIntervalBetweenFSCK;
    public uint OSCreationID;
    public uint MajorVersion;
    public uint ReservedBlocksAllowedUID;
    public uint ReservedBlocksAllowedGID;
}