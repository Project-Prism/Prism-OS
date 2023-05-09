using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFProgramHeader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFProgramHeader64
{
    public ELFProgramHeader64(ELFProgramHeader64* Original)
    {
        Type = Original->Type;
        Offset = Original->Offset;
        VAddress = Original->VAddress;
        PAddress = Original->PAddress;
        FileSize = Original->FileSize;
        MemorySize = Original->MemorySize;
        Flags = Original->Flags;
        Align = Original->Align;
    }

    #region Flags

    public ELFProgramType Type;
    public uint Flags;
    public ulong Offset;
    public ulong VAddress;
    public ulong PAddress;
    public uint FileSize;
    public uint MemorySize;
    public uint Align;

    #endregion
}