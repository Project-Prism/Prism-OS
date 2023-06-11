using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFHeader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFHeader64
{
    public ELFHeader64(ELFHeader64* Original)
    {
        MagicNumber = Original->MagicNumber;
        ClassType = Original->ClassType;
        EndianType = Original->EndianType;
        Version = Original->Version;
        ABIType = Original->ABIType;
        ABIVersion = Original->ABIVersion;

        Type = Original->Type;
        MachineType = Original->MachineType;
        MachineVersion = Original->MachineVersion;
        EntryPoint = Original->EntryPoint;
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

    #region Properties

    public bool IsValid => MagicNumber == 0x7F454C46;

    #endregion

    #region Fields

    #region Identifier [14 bytes]

    public uint MagicNumber;
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
    public ulong EntryPoint;
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