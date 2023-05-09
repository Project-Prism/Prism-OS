using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFProgramHeader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFProgramHeader32
{
    public ELFProgramHeader32(ELFProgramHeader32* Original)
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

    #region Fields

    public ELFProgramType Type;
    public uint Offset;
    public uint VAddress;
    public uint PAddress;
    public uint FileSize;
    public uint MemorySize;
    public uint Flags;
    public uint Align;

    #endregion
}