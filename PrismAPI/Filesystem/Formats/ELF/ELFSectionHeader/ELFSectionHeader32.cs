using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFSectionHeader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFSectionHeader32
{
    public ELFSectionHeader32(ELFSectionHeader32* Original)
    {
        Name = Original->Name;
        Type = Original->Type;
        Flags = Original->Flags;
        Address = Original->Address;
        Offset = Original->Offset;
        Size = Original->Size;
        Link = Original->Link;
        Info = Original->Info;
        AddressAlign = Original->AddressAlign;
        EntrySize = Original->EntrySize;
    }

    #region Flags

    public uint Name;
    public ELFSectionType Type;
    public ELFSectionFlagsType Flags;
    public uint Address;
    public uint Offset;
    public uint Size;
    public uint Link;
    public uint Info;
    public uint AddressAlign;
    public uint EntrySize;

    #endregion
}