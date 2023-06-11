namespace PrismAPI.Filesystem.Filesystems.XFS.Structure;

/// <summary>
/// The two Free Space B+trees store a sorted array of block offset and block counts in the leaves of the
/// B+tree.The first B+tree is sorted by the offset, the second by the count or size.
/// </summary>
public unsafe struct BTreeBlock
{
    public uint Magic;
    public ushort Level;
    public ushort NumberOfRecs;
    public uint LeftSib;
    public uint RightSib;
}