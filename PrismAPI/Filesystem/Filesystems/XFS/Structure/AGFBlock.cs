namespace PrismAPI.Filesystem.Filesystems.XFS.Structure;

/// <summary>
/// The second sector in an AG contains the information about the two free space B+trees and associated
/// free space information for the AG.The "AG Free Space Block", also knows as the AGF, uses the
/// following structure:
/// </summary>
public unsafe struct AGFBlock
{
    #region Methods

    public bool IsValid => MagicNumber == 0x58414746;

    #endregion

    #region Fields

    public uint MagicNumber;
    public uint VersionNumber;
    public uint SequenceNumber;
    public uint Length;
    public fixed uint Roots[2];
    public uint agf_spare0;
    public fixed uint Levels[2];
    public uint Spare1;
    public uint FirstFreeList;
    public uint LastFreeList;
    public uint FileCount;
    public uint FreeBlocks;
    public uint Longest;
    public uint BTreeBlocks;

    #endregion
}