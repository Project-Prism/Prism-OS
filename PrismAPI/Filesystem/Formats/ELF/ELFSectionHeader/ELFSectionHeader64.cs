using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFSectionHeader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFSectionHeader64
{
    public ELFSectionHeader64(ELFSectionHeader64* Original)
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

    #region Fields

    public uint Name;
    public ELFSectionType Type;
    public ELFSectionFlagsType Flags;
    public ulong Address;
    public ulong Offset;
    public uint Size;
    public uint Link;
    public uint Info;
    public uint AddressAlign;
    public uint EntrySize;

    #endregion
}